using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class accessNativeFeatures : MonoBehaviour
{
    Text txt;
    // Start is called before the first frame update
    void Start(){
        // Find the 2D Text component in the gameObject
        txt=this.GetComponent<Text>();
        // The custom function we want to call from inside Unity
        string methodName="sayHi";
        // If anything goes wrong, this will be the response
        string defaultResponse="Invalid Response";
        // The package name we defined in Figure 3.4 for our custom plugin
        string customPluginPackageName="com.unity.nativefeatures";
        // Name of the new Java Class we created in Figure 3.11
        string customPluginClassName="CustomPluginForUnity";
        // Accessing com.unity.nativefeatures.CustomPluginForUnity class
        string androidJavaClass = customPluginPackageName + "." +
        customPluginClassName;       
        // Calling the generic function and displaying the result in 2D Text
        txt.text=StaticCall<string>(methodName, defaultResponse,
        androidJavaClass);
    }
    // Defining our generic function to call methods inside our custom plugin
    public T StaticCall<T>(string methodName, T defaultResponse, string androidJavaClass){
        T result;
        // This function will work work with Android platform
        if (Application.platform != RuntimePlatform.Android)
            return defaultResponse; 
        try{
            using (AndroidJavaClass androidClass = new  AndroidJavaClass(androidJavaClass)){
                if (null != androidClass)
                    // Calling our custom plugin method 
                    result = androidClass.CallStatic<T>(methodName);
                else
                    result = defaultResponse;
            }
        }
        catch (System.Exception ex){
            // If there is an issue you can check it inside android logcat
            Debug.Log(string.Format("{0}.{1} Exception:{2}", androidJavaClass, methodName, ex.ToString() ));
            return defaultResponse;
        }
        return result;
    }
}
