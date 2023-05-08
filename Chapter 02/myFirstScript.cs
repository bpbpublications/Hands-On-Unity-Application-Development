using UnityEngine;

public class myFirstScript: MonoBehaviour {
  //Awake is called when the script instance is being loaded
  void Awake(){
    Debug.Log(this.name + “ is awake”);
  }

  //This function is called whenever the object becomes enabled and active.
  void OnEnable(){
    Debug.Log(this.name + “ is enabled”);
  }

  public Vector3 RotateAmount;
  //Start is called on the frame when a script is enabled just before any of the Update methods are called the first time.
  void Start() {
    Debug.Log(this.name + “ has started”);
    StartCoroutine(delayedResponse(2f));
  }
  
  //Update is called every frame, if the MonoBehaviour is enabled.
  void Update(){
    Debug.Log(this.name + “ is getting updated”);
    this.transform.Rotate(RotateAmount * Time.deltaTime);
  }

  IEnumerator delayedResponse(float delay){
  yield return new WaitForSeconds(delay);
    Debug.Log("This message is delayed by " + delay + "secs");
    yield return new WaitForEndOfFrame();
    Debug.Log("This message is delayed by a frame");
    yield return new WaitForSeconds(delay);
    Debug.Log("This message is again delayed by " + delay + "secs");
    yield return null;
  }

  //This function is called when the user has pressed the mouse button while over the Collider.
  void OnMouseDown() {
    Debug.Log("Mouse click is down on " + this.name);
  }

  //OnMouseDrag is called when the user has clicked on a Collider and is still holding down the mouse.
  void OnMouseDrag() {
    Debug.Log("Mouse is dragging on " + this.name);
  }

  //Called once when the mouse enters the Collider.
  void OnMouseEnter(){
    Debug.Log("Mouse has entered on "+ this.name);
  }

  //Called once when the mouse is not any longer over the Collider.
  void OnMouseExit(){
    Debug.Log("Mouse has exited from "+ this.name);
  }

  //Called every frame while the mouse is over the Collider.
  void OnMouseOver(){
    Debug.Log("Mouse is over "+ this.name);
  }

  //OnMouseUp is called when the user has released the mouse button.
  void OnMouseUp(){
    Debug.Log("Mouse click is now up from "+ this.name);
  }

  //OnMouseUpAsButton is only called when the mouse is released over the same Collider as it was pressed.
  void OnMouseUpAsButton(){
    Debug.Log("Mouse has exited from "+ this.name);
  }

  //This function is called when the renderer became visible by any camera.
  void OnBecameVisible(){
    Debug.Log(this.name+" is visible");
  }

  //This function is called when the renderer is no longer visible by any camera.
  void OnBecameInvisible(){
    Debug.Log(this.name+" is invisible");
  }
  
  //This function is sent to all game objects before the application quits.
  void OnApplicationQuit(){
    Debug.Log(“Application is closed”);
  }

  //This function is called when the behaviour becomes disabled.
  void OnDisable(){
    Debug.Log(this.name + “ is disabled”);
  }

  //Destroying the attached Behaviour will result in the game or Scene receiving OnDestroy.
  void OnDestroy(){
    Debug.Log(this.name + “ is destroyed”);
  }


}