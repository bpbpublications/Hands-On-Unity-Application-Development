using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
public class SearchTweetAndSpawnBird: MonoBehaviour
{
  public GameObject birdPrefab;
  // Hashtag for which we will be fetching the tweets
  private string hashtag = "madewithunity";
  // Endpoint for SearchTweets API
  private string SEARCH_TWEETS_ENDPOINT = "https://api.twitter.com/2/tweets/search/recent?query=";
  // TweetID that is currently being displayed
  private string currentTweetID = "";
  // Reference to the bird that is currently being displayed
  private GameObject _bird;
  // Start is called before the first frame update
  void Start()
  {
    // Indefinitely loop on the function, starts after 1 sec and runs again every 10 sec
    InvokeRepeating("_SearchTweets", 1.0f, 10f);
  }
  // Internal function to call the coroutine
  private void _SearchTweets(){
    StartCoroutine(SearchTweets(hashtag));
  }
  // Searching for Tweets using the Hashtag 
  IEnumerator SearchTweets(string hashtag)
  {
    using (UnityWebRequest webRequest = UnityWebRequest.Get(SEARCH_TWEETS_ENDPOINT + hashtag))
    {
      // Adding BEARER_TOKEN to Header for Authorization
      webRequest.SetRequestHeader("Authorization", "Bearer"+AccessKeys.BEARER_TOKEN);
      // Request and wait for the desired page.
      yield return webRequest.SendWebRequest();
      switch (webRequest.result)
      {
        case UnityWebRequest.Result.ConnectionError:
        case UnityWebRequest.Result.DataProcessingError:
        case UnityWebRequest.Result.ProtocolError:
          Debug.LogError("Error: " + webRequest.error);
          break;
        case UnityWebRequest.Result.Success:
          Debug.Log("Received: " + webRequest.downloadHandler.text);
          foundTweets(webRequest.downloadHandler.text);
          break;
      }
    }
  }
  // Parsing the result into JSON and spawning the new bird
  void foundTweets(string result){
    //Convert the string result in the text to JSON
    SearchTweetResult json=JsonUtility.FromJson<SearchTweetResult>(result);
    //If a new Tweet is detected
    if(currentTweetID!=json.data[0].id.ToString())
    {
       //If a bird exists in the scene, make it fly away
       if(_bird) _bird.GetComponent<BirdControl>().flyAway();
       //Set the new Tweet ID
       currentTweetID=json.data[0].id.ToString();
       //Spawn the new bird into the scene
       _bird= GameObject.Instantiate(birdPrefab);
       _bird.GetComponent<BirdControl>().flyIn(currentTweetID);
    }
  }
}