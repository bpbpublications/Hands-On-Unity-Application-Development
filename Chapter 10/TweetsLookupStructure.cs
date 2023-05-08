[System.Serializable]
public class TweetsLookupResult
{
    public TweetsLookupResult_Data data;
}
[System.Serializable]
public class TweetsLookupResult_Data
{
    public string author_id;
    public string created_at;
    public string id;
    public string lang;
    public bool possibly_sensitive;
    public string source;
    public string text;
}
