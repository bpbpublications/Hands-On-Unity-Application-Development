using UnityEngine;
// This script will require a Camera component to function
[RequireComponent(typeof (Camera))]
public class mouseControl: MonoBehaviour {
  // Horizontal rotation speed
  public float horizontalSpeed = 2 f;
  // Vertical rotation speed
  public float verticalSpeed = 1 f;
  // Lock cursor
  public bool lockCursor = true;

  private Camera mainCamera;
  private float xRotation = 0.0 f;
  private float yRotation = 0.0 f;

  void Start() {
    // Finds the reference to the Camera component in this object
    mainCamera = this.GetComponent < Camera > ();
  }
  void Update() {
    //Determine whether mouse is locked to the game window by a ternary operator
    Cursor.lockState = lockCursor ? CursorLockMode.Locked : CursorLockMode.None;
    // The value is in the range -1 to 1 multiplied by fixed variable
    float mouseX = Input.GetAxis("Mouse X") * horizontalSpeed;
    float mouseY = Input.GetAxis("Mouse Y") * verticalSpeed;
    yRotation += mouseX;
    xRotation -= mouseY;
    mainCamera.transform.eulerAngles = new Vector3(xRotation, yRotation, 0 f);
  }
}