using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraColliderLogic : MonoBehaviour
{
    public Camera MainCamera;
    public GameObject Target;
    public GameObject Position;

    private void OnTriggerEnter(Collider other)
    {
        MainCamera.transform.position = Position.transform.position;
        MainCamera.transform.LookAt(Target.transform.position);
    }
}
