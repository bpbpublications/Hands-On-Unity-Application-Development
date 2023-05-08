using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation; //Header file to access the AR Foundation components
using UnityEngine.XR.ARSubsystems; //Used to access the AR Subsystems like raycasting

public class tapToAugment: MonoBehaviour {
  //Defining as public objects allow them to become accessible in other scripts and Inspector.
  public GameObject arObject;
  public GameObject placementIndicator;
  public Camera arCamera;
  public ARRaycastManager arRaycastManager;

  //Defined as private for internal use in this script
  private Pose placementPose; //Contains the location of the point where raycast hits the surface
  private bool isAugentationAllowed = false;
  void Update() {
    //Calculates the point where raycast from the centre of mobile screen hits the detected plane
    GetPlacementPose();

    if (isAugentationAllowed) //Checks if a new augmentation is allowed 
      if (Input.touchCount > 0) //Checks whether the user is giving a touch input of the screen
        if (Input.GetTouch(0).phase == TouchPhase.Began) //Checks the first touch point has began
          SpawnObject(); //Spawns the ARObject into the scene 
  }
  private void GetPlacementPose() {
    //Stores the hit results from the raycast
    var hits = new List < ARRaycastHit > ();
    //Used to find the centre screen point from where the raycast should began
    var screenCenter = arCamera.ViewportToScreenPoint(new Vector2(0.5 f, 0.5 f));
    //Ray is casted from screenCenter, stores hit results in hits and collides with trackable planes
    arRaycastManager.Raycast(screenCenter, hits, TrackableType.Planes);

    if (hits.Count > 0) {
      //Raycast is hitting a surface
      placementPose = hits[0].pose;
      placementIndicator.SetActive(true);
      //Placement Indicator position update
      placementIndicator.transform.position = placementPose.position;
      placementIndicator.transform.rotation = placementPose.rotation;
      isAugentationAllowed = true;
    } else {
      //Raycast is not hitting any surface
      placementIndicator.SetActive(false);
      isAugentationAllowed = false;
    }
  }
  private void SpawnObject() {
    //Spawning ARCube game object in the scene 
    var augmentedObj = Instantiate(arObject);
    //Assigning position to augmented object
    augmentedObj.transform.position = placementPose.position;
    augmentedObj.transform.rotation = placementPose.rotation;
  }
}