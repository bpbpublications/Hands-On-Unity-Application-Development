using UnityEngine; // Used for MonoBehaviour
using UnityEngine.Networking; // Used for UnityWebRequest
using System.Collections; // Used for IEnumerator
public class BirdControl: MonoBehaviour
{
    public TextMesh text3d;        
    // Endpoint for TweetsLookUp API
    private string TWEETS_LOOKUP_ENDPOINT="https://api.twitter.com/2/tweets/";
    // Additional attributes required to be fetched from within the Tweet
    private string TWEETS_FIELD = "?tweet.fields=attachments,author_id,created_at,entities,geo,id,in_reply_to_user_id,lang,possibly_sensitive,referenced_tweets,source,text,withheld";
    // Function will be called when a new Tweet is detected
    public void flyIn(string TWEET_ID){
        StartCoroutine(TweetsLookup(TWEET_ID));
    }
    // Fetching the content of the Tweet using the TWEET ID 
    IEnumerator TweetsLookup(string TWEET_ID)
    {
      using (UnityWebRequest webRequest = UnityWebRequest.Get(TWEETS_LOOKUP_ENDPOINT + TWEET_ID + TWEETS_FIELD)){
        // Adding BEARER_TOKEN to Header for Authorization
        webRequest.SetRequestHeader("Authorization", "Bearer " + AccessKeys.BEARER_TOKEN);
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
                lookedupTweet(webRequest.downloadHandler.text);
                break;
        }
      }
    } 
    // Parsing the result into JSON and triggering the animation
    void lookedupTweet(string result){
        // Convert the string result text to JSON
        TweetsLookupResult json=JsonUtility.FromJson<TweetsLookupResult>(result);
        // Assign the text to the component
        text3d.text=json.data.text;
        // Trigger the Animation to flyIn
        this.GetComponent<Animator>().SetTrigger("flyIn");  
    }    
    // Triggering the flyAway animation
    public void flyAway(){
        this.GetComponent<Animator>().SetTrigger("flyAway");
    }
    // Destroying the game object(itself)
    public void destroy(){
        Destroy(this.gameObject);
    }
}