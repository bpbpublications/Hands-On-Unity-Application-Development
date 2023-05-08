using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fetchWebcamFeed : MonoBehaviour {
  // Dimensions of Webcam feed
  public int requested_width = 640;
  public int requested_height = 480;
  // Web Camera ID
  public int camId = 0;
  // Holds the webcam feed
  private WebCamTexture cam_texture;
  // Stores the still frame that will be sent to cloud
  private Texture2D texture2D;
  // Stores the reference of the labelDetection script
  private labelDetection lDetection;
  // Start is called before the first frame update
  void Start(){
    // Get the reference of the script
    lDetection = this.GetComponent<labelDetection>();
    WebCamDevice[] devices = cam_texture.devices;
    // Logging all the webcam devices in console
    for (var i = 0; i < devices.Length; i++)
      Debug.Log(i + ": " + devices[i].name);
    if (devices.Length > 0){
      cam_texture = new WebCamTexture(devices[camId].name, 
                           requested_width, requested_height);
      Renderer r = GetComponent<Renderer>();
      if (r != null){
        Material m = r.material;
        if (m != null){
          // Assigning webcam feed to plane’s material
          m.mainTexture = cam_texture;
        }
      }
      // Start playing the webcam
      cam_texture.Play();
    }
  }
}

// Update is called once per frame
void Update(){
  // Capturing the frame and triggering the API call on pressing Space
  if (Input.GetKeyDown(KeyCode.Space))
    StartCoroutine("Capture");
}
private IEnumerator Capture(){
  // Frame from webcam is converted to pixels and stored
  Color[] pixels = cam_texture.GetPixels();
  if (pixels.Length == 0)
    yield return null;
  if (texture2D == null || cam_texture.width != texture2D.width ||
      cam_texture.height != texture2D.height){
    texture2D = new Texture2D(cam_texture.width, cam_texture.height,
                              TextureFormat.RGBA32, false);
  }
  texture2D.SetPixels(pixels);
  // Still Frame is converted into bytes
  byte[] jpg = texture2D.EncodeToJPG();
  // Bytes are converted into string of base64
  string base64 = System.Convert.ToBase64String(jpg);
  // Make the API request
  StartCoroutine(lDetection.requestAPI(base64));
}
