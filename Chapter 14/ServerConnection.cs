using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

[System.Serializable]
public class Message
{
  public string message;
}

public class ServerConnection : MonoBehaviour
{
  string serverUrl = "https://<your-project-name>.glitch.me/";
  public Text nameText;
  public Button sendButton;
  public InputField inputField;

  void Start()
  {
    StartCoroutine(GetRequest(serverUrl));
    sendButton.onClick.AddListener(() =>
    {
      string message = inputField.text;
      StartCoroutine(PutRequest(serverUrl, message));
      inputField.text = "";
    });
  }

  IEnumerator GetRequest(string url)
  {
      UnityWebRequest request = UnityWebRequest.Get(url);
      yield return request.SendWebRequest();

      if (request.isNetworkError || request.isHttpError)
      {
        Debug.LogError(request.error);
      }
      else
      {
          Debug.Log(request.downloadHandler.text);
          string responseJson = request.downloadHandler.text;
          Message response = JsonUtility.FromJson<Message>(responseJson);
          nameText.text = "Name: " + response.name;
      }
  }
  IEnumerator PutRequest(string url, string name)
  {
      Message message=new Message();
      message.name=name;
      string jsonString = JsonUtility.ToJson(message);
      UnityWebRequest request = UnityWebRequest.Put(url + ”name”, jsonString);
      request.SetRequestHeader("Content-Type", "application/json");
      yield return request.SendWebRequest();

      if (request.isNetworkError || request.isHttpError)
      {
          Debug.LogError(request.error);
      }
      else
      {
          Debug.Log("Message sent");
          string responseJson = request.downloadHandler.text;
          Message response = JsonUtility.FromJson<Message>(responseJson);
          nameText.text = "Name: " + response.name;
      }
  }

}
