using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraRecenter : MonoBehaviour
{
    private CinemachineFreeLook camera;
    void Start()
    {
        camera = GetComponent<CinemachineFreeLook>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetButton("CameraRecenter"))
        {
            Debug.Log("Center");
            camera.m_RecenterToTargetHeading.m_enabled = true;
        }
        else
        {
            camera.m_RecenterToTargetHeading.m_enabled = false;
        }
    }
}
