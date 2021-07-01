using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZoomCommand : Command
{
    [SerializeField]
    private float zoomSpeed = 100;
    [SerializeField]
    private float minZoom = 0;
    [SerializeField]
    private float maxZoom = 30;

    [SerializeField]
    private CinemachineCameraOffset cameraOffset;

    private float zoomAmount;
    private void OnEnable()
    {
        // SaveLoadHandlers.VirtualCamOffsetLoad.AddListener(VirtualCamOffsetLoad);
    }
    private void OnDisable()
    {
        // SaveLoadHandlers.VirtualCamOffsetLoad.RemoveListener(VirtualCamOffsetLoad);
    }

    private void VirtualCamOffsetLoad(float arg0)
    {
        cameraOffset.m_Offset = new Vector3(0, 0, arg0);
    }

    public override void ExecuteWithFloat(float value)
    {

        zoomAmount = Mathf.Clamp(zoomAmount - (value / zoomSpeed), minZoom, maxZoom);

        cameraOffset.m_Offset = new Vector3(0, 0, -zoomAmount);
        // SaveLoadHandlers.VirtualCamOffset?.Invoke(cameraOffset.m_Offset.z);

    }
}
