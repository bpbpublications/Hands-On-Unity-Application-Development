using UnityEngine;
using System.Collections;
public class ActivateAllDisplays : MonoBehaviour{
    // We call our methods in start as they only need to executed once
    void Start (){
      Debug.Log ("No of Displays detected: " + Display.displays.Length);
      // Display.displays[0] is the primary display
      // and is always ON, so we start at the index 1.
      for (int i = 1; i < Display.displays.Length; i++)
      {
         // Check if additional displays are available and activate each.
         Display.displays[i].Activate();
      }
   }
}
