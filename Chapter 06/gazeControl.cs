using UnityEngine;
public class gazeControl: MonoBehaviour {
  private RaycastHit hit;
  private Transform lastHit;

  void Update() {
    //Raycast from the center of the camera and on hit the output is stored in hit
    if (Physics.Raycast(this.transform.position, this.transform.forward, out hit)) {
      //Checking if the object that was hit has our assigned tag
      if (hit.transform.tag == "infoBubble") {
        //Enable the first child of the hit object that is BG
        if (hit.transform.childCount > 0)
          hit.transform.GetChild(0).gameObject.SetActive(true);
        //Save the currently hit transform
        lastHit = hit.transform;
      }
    } else if (lastHit != null) //If raycast was hitting any collider previously
    {
      if (hit.transform.childCount > 0)
        lastHit.GetChild(0).gameObject.SetActive(false);
      lastHit = null;
    }
  }
}