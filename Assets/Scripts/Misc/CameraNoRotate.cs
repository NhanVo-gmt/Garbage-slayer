using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraNoRotate : MonoBehaviour
{

    Transform cam;

    void Awake() 
    {
        cam = GetComponent<Transform>();
    }
    
    void LateUpdate() 
    {
        cam.transform.rotation = Quaternion.Euler(0, 0, 0);
    }
}
