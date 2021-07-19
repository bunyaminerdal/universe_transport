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
    private float maxZoom1 = 200;

    [SerializeField]
    private CinemachineCameraOffset cameraOffset;
    bool isSystemMapOpened;
    private float zoomAmount;

    private float currentMaxZoom;
    private float tempZoom1 = 100f;
    private float tempZoom = 100f;

    private void OnEnable()
    {
        // SaveLoadHandlers.VirtualCamOffsetLoad.AddListener(VirtualCamOffsetLoad);
        PlayerManagerEventHandler.MapChangeEvent.AddListener(SystemMapChange);
    }
    private void OnDisable()
    {
        // SaveLoadHandlers.VirtualCamOffsetLoad.RemoveListener(VirtualCamOffsetLoad);
        PlayerManagerEventHandler.MapChangeEvent.RemoveListener(SystemMapChange);
    }
    private void Start()
    {
        zoomAmount = -cameraOffset.m_Offset.z;
        PlayerManagerEventHandler.MovementModifier?.Invoke(zoomAmount);
        currentMaxZoom = maxZoom;

    }
    private void SystemMapChange(bool isopened)
    {
        if (isopened)
        {
            tempZoom = zoomAmount;
            zoomAmount = tempZoom1;
            currentMaxZoom = maxZoom1;
        }
        else
        {
            tempZoom1 = zoomAmount;
            zoomAmount = tempZoom;
            currentMaxZoom = maxZoom;
        }
        PlayerManagerEventHandler.MovementModifier?.Invoke(zoomAmount);
        cameraOffset.m_Offset = new Vector3(0, 0, -zoomAmount);

    }
    private void VirtualCamOffsetLoad(float arg0)
    {
        zoomAmount = -arg0;
        cameraOffset.m_Offset = new Vector3(0, 0, arg0);
    }

    public override void ExecuteWithFloat(float value)
    {

        zoomAmount = Mathf.Clamp(zoomAmount - (value / zoomSpeed), minZoom, currentMaxZoom);
        PlayerManagerEventHandler.MovementModifier?.Invoke(zoomAmount);
        cameraOffset.m_Offset = new Vector3(0, 0, -zoomAmount);
        // SaveLoadHandlers.VirtualCamOffset?.Invoke(cameraOffset.m_Offset.z);

    }
}
