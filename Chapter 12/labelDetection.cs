using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class labelDetection : MonoBehaviour{
  // Vision API HTTP Request URL
  public string endpoint= "https://vision.googleapis.com/v1/images:annotate?key=";
  // GOOGLE CLOUD API KEY
  public string apiKey = "";
  // Max number of results required from Vision API
  public int maxResults = 5;
  // Deserialized Results received from API
  public AnnotateImageResponses result;
  // Type of Detection
  string featureType = "LABEL_DETECTION";
  // Indicates the format of the http request and allowed response
  Dictionary<string, string> headers;
  [System.Serializable]
  public class AnnotateImageRequests{
    public List<AnnotateImageRequest> requests;
  }
  [System.Serializable]
  public class Feature{
    public string type;
    public int maxResults;
  }
  [System.Serializable]
  public class AnnotateImageRequest{
    public Image image;
    public List<Feature> features;
  }
  [System.Serializable]
  public class Image{
    public string content;
  }
  [System.Serializable]
  public class AnnotateImageResponses{
    public List<AnnotateImageResponse> responses;
  }
  [System.Serializable]
  public class AnnotateImageResponse{
    public List<EntityAnnotation> labelAnnotations;
  }
  [System.Serializable]
  public class EntityAnnotation{
    public string mid;
    public string description;
    public float score;
    public float topicality;
  }
  // Start is called before the first frame update
  void Start(){
    headers = new Dictionary<string, string>();
    headers.Add("Content-Type", "application/json; charset=UTF-8");
    if (apiKey == null || apiKey == "")
      Debug.LogError("No API key. Please set your API key.");
  }
  public IEnumerator requestAPI(string base64){
    // Building new List for API requests
    AnnotateImageRequests requests = new AnnotateImageRequests();
    requests.requests = new List<AnnotateImageRequest>();
    // Building new Feature request
    Feature feature = new Feature();
    feature.type = featureType;
    feature.maxResults = this.maxResults;
    // Building an API request
    AnnotateImageRequest request = new AnnotateImageRequest();
    request.image = new Image();
    request.image.content = base64;
    request.features = new List<Feature>();
    request.features.Add(feature);
    // Adding the API request to the List
    requests.requests.Add(request);
    // Convert the request into JSON format
    string jsonData = JsonUtility.ToJson(requests, false);
    if (jsonData != string.Empty){
      string url = this.endpoint + this.apiKey;    
      // Converting the json data into bytes
      byte[] postData = System.Text.Encoding.Default.GetBytes(jsonData);
      // Making the request to the Vision API
      using (WWW www = new WWW(url, postData, headers)){
        yield return www;
        if (string.IsNullOrEmpty(www.error)){
          // Success, the result has been received
          Debug.Log(www.text.Replace("\n", "").Replace(" ", ""));
          // Result is deserialized and stored
          result = JsonUtility.FromJson<AnnotateImageResponses>(www.text);
        }
        else{
          // Some error has occurred
          Debug.Log("Error: " + www.error);
        }
      }
    }
  }
}
