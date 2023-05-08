using UnityEngine;
using System.Collections;
/**
 * Sample for reading using polling by yourself, and writing too.
 */
public class SampleUserPolling_ReadWrite: MonoBehaviour {
  public SerialController serialController;
  // Initialization
  void Start() {
    // Finding the main Searial Controller script in the scene
    serialController=GameObject.Find("SerialController").GetComponent<SerialController>();
    Debug.Log("Press A or Z to execute some actions");
  }
  // Executed each frame
  void Update() {
    //---------------------------------------------------------------------
    // Send data
    //---------------------------------------------------------------------
    // If you press one of these keys send it to the serial device.
    if (Input.GetKeyDown(KeyCode.A)) {
      Debug.Log("Sending A");
      serialController.SendSerialMessage("A");
    }
    if (Input.GetKeyDown(KeyCode.Z)) {
      Debug.Log("Sending Z");
      serialController.SendSerialMessage("Z");
    }
    //---------------------------------------------------------------------
    // Receive data
    //---------------------------------------------------------------------
    string message = serialController.ReadSerialMessage();
    if (message == null) return; 
    // Check if the message is plain data or a connect/disconnect event.
    if (ReferenceEquals(message, SerialController.SERIAL_DEVICE_CONNECTED))                  
        Debug.Log("Connection established");
    else if(ReferenceEquals(message, SerialController.SERIAL_DEVICE_DISCONNECTED)) 
        Debug.Log("Connection attempt failed or disconnection detected");
    else 
        Debug.Log("Message arrived: " + message);
  }
}
