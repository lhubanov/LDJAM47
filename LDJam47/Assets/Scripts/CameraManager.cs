using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public Camera MainCamera;
    public List<GameObject> CameraPositions;
    public List<GameObject> CameraTargets;
    private int CamPosNum = -1;

    void Start()
    {
        MainCamera = Camera.main;
        SelectNextCamera();
    }

    void SelectNextCamera()
    {
        CamPosNum = CamPosNum + 1;
        MainCamera.transform.position = CameraPositions[CamPosNum].transform.position;
        MainCamera.transform.LookAt(CameraTargets[CamPosNum].transform.position);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("space"))
        {
            SelectNextCamera();
        }
    }
}
