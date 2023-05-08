using UnityEngine;
[RequireComponent(typeof(Camera))] 
public class gyroControl : MonoBehaviour
{
    private GameObject cameraParent;
    void Start()
    {
        //Assigning a private variable the reference to the parent gameobject
        cameraParent = this.transform.parent.gameObject;
        //Enabling gyroscope sensor in our application
        Input.gyro.enabled = true;
    }
    void Update()
    {
        //Handling the left-right rotation in the CameraParent gameobject
        //the negative is because the gyroscope values are inverted
        cameraParent.transform.Rotate(0, -Input.gyro.rotationRateUnbiased.y, 0);
        //Handling the up-down rotation in the MainCamera gameobject
        this.transform.Rotate(-Input.gyro.rotationRateUnbiased.x, 0, 0);
    }
}
