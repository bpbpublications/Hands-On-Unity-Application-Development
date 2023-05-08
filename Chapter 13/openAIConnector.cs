using UnityEngine;
using UnityEngine.UI;
using System.Collections; //Used for Coroutine and IEnumerator
using System.Text; //Used for Encoding
using UnityEngine.Networking; // Used for UnityWebRequest

// Data class for the response from the DALL-E API
[System.Serializable] // This attribute is required for the JsonUtility to work
public class DALLEData
{
    public string url;
}

// Response class for the response from the DALL-E API
[System.Serializable]
public class DALLEResponse
{
    public DALLEData[] data;
    public string[] created;
}

public class openAIConnector : MonoBehaviour
{
    [SerializeField]
    RawImage outputImage;

    [SerializeField]
    InputField inputField;

    [SerializeField]
    Button generateButton;

    // DALL-E API endpoint
    private string DALLE_API_ENDPOINT= "https://api.openai.com/v1/images/generations";
    // DALL-E API key
    private string API_KEY= "ENTER_YOUR_API_KEY";

    // Start is called before the first frame update
    void Start()
    {
        // Added new listener to the generate button
        generateButton.onClick.AddListener(()=>{
            StartCoroutine(SendDALLERequest(inputField.text));
        });
    }

    // Function for sending a post request to the DALL-E API endpoint with [prompt]
    IEnumerator SendDALLERequest(string _prompt) {
      // Create a new UnityWebRequest
      UnityWebRequest request = new UnityWebRequest(DALLE_API_ENDPOINT, "POST");
      // Add the API key to the headers
      request.SetRequestHeader("Authorization", "Bearer " + API_KEY);
      // Construct the json with the prompt
      var json="{\"prompt\": \""+_prompt+"\", \"model\": \"image-alpha-001\",
                \"num_images\": 1, \"size\": \"512x512\"}";
      // Add the json to the request body
      byte[] body = Encoding.UTF8.GetBytes(json);
      // Set the request body
      request.uploadHandler = new UploadHandlerRaw(body);
      // Set the content type
      request.uploadHandler.contentType = "application/json";
      // Set the download handler
      request.downloadHandler = new DownloadHandlerTexture();
      // Send the request
      yield return request.SendWebRequest();
      if (request.isNetworkError || request.isHttpError) {
          // Handle error
          Debug.LogError(request.error);
      } else {
          // Get the response from the API
          string response = request.downloadHandler.text;
          //Handle Response
          handleResponse(response);
      }
    }

    // Handle the response from the API
    private void handleResponse(string response){
        // Parse the response
        DALLEResponse dalleResponse = JsonUtility.FromJson<DALLEResponse>(response);
        // Print the first image url in console
        Debug.Log(dalleResponse.data[0].url);
        // Download the image and display it
        StartCoroutine(DownloadImage(dalleResponse.data[0].url));
    }

    // Download the image and display it
    private IEnumerator DownloadImage(string imageURL)
    {
      UnityWebRequest request = UnityWebRequestTexture.GetTexture(imageURL);
      yield return request.SendWebRequest();
      if (request.isNetworkError || request.isHttpError)
      {
        //Handle the error
        Debug.LogError(request.error);
      }
      else
      {
        // Get the downloaded texture and assign it to the RawImage texture
        outputImage.texture=((DownloadHandlerTexture)request.downloadHandler).texture;
      }
    }

}
