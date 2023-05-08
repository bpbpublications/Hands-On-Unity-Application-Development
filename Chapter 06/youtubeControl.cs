using UnityEngine;
[RequireComponent(typeof (Camera))]
public class youtubeControl: MonoBehaviour {
  public float speed = 3 f;
  private GameObject cameraParent;
  private float xRotation;
  private float yRotation;
  private Vector3 point1; //variable for saving the initial touch point
  private Vector3 point2;
  private float xAngle = 0.0 f; //Rotation angle for x axis
  private float yAngle = 0.0 f;
  private float xAngTemp = 0.0 f; //temporary variable for angle
  private float yAngTemp = 0.0 f;
  void Start() {
    //Assigning a private variable the reference to the parent gameobject        
    cameraParent = this.transform.parent.gameObject;
    //Initialization the angle of our camera
    xAngle = 0.0 f;
    yAngle = 0.0 f;
    this.transform.rotation = Quaternion.Euler(yAngle, xAngle, 0.0 f);
  }
  void Update() {
    //Touch Control
    //If touch points are greater than 0
    if (Input.touchCount > 0) {
      //Touch began, save position
      if (Input.GetTouch(0).phase == TouchPhase.Began) {
        point1 = Input.GetTouch(0).position;
        xAngTemp = xAngle;
        yAngTemp = yAngle;
      }
      //If finger is moved on screen
      if (Input.GetTouch(0).phase == TouchPhase.Moved) {
        point2 = Input.GetTouch(0).position;
        //Calculating the angle to rotate
        //Rotate a max of 180 degree on swiping the screen in x axis
        xAngle = xAngTemp + (point2.x - point1.x) * 180 f / Screen.width;
        //Rotate a max of 90 degree on swiping the screen in y axis
        yAngle = yAngTemp - (point2.y - point1.y) * 90 f / Screen.height;
        //Rotate camera
        this.transform.rotation = Quaternion.Euler(yAngle, xAngle, 0.0 f);
      }
    } else {
      //Mouse Controls
      //If left mouse button is down
      if (Input.GetMouseButton(0)) {
        //Handling the left-right rotation in the CameraParent gameobject
        yRotation = -Input.GetAxis("Mouse X") * speed;
        cameraParent.transform.Rotate(0, yRotation, 0);
        //Handling the up-down rotation in the MainCamera gameobject
        xRotation = Input.GetAxis("Mouse Y") * speed;
        this.transform.Rotate(xRotation, 0, 0);
      }
    }
  }
}