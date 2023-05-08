[System.Serializable]
public class SearchTweetResult
{
    public SearchTweetResult_Data[] data;
    public SearchTweetResult_Meta meta;
}
[System.Serializable]
public class SearchTweetResult_Meta
{
    public string newest_id;
    public string oldest_id;
    public int result_count;
    public string next_token;
}
[System.Serializable]
public class SearchTweetResult_Data
{
    public string[] edit_history_tweet_ids;
    public string id;
    public string text;
}