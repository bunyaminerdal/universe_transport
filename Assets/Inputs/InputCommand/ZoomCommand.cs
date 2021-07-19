using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class ZoomCommand : Command
{
    [SerializeField]
    private float zoomSpeed = 1.25f;
    [SerializeField]
    private float minZoom = 0;
    [SerializeField]
    private float maxZoom = 30;

    [SerializeField]
    private float minZoom1 = 100;
    [SerializeField]
    private float maxZoom1 = 200;

    bool isSystemMapOpened;
    private float zoomAmount;
    [SerializeField]
    private CinemachineVirtualCamera cameraBase;
    private float currentMaxZoom;
    private float currentMinZoom;
    private float tempZoom1 = 30f;
    private float tempZoom = 30f;

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
        zoomAmount = cameraBase.m_Lens.OrthographicSize;
        PlayerManagerEventHandler.MovementModifier?.Invoke(zoomAmount);
        currentMaxZoom = maxZoom;
        currentMinZoom = minZoom;

    }
    private void SystemMapChange(bool isOpened)
    {
        if (isOpened)
        {
            tempZoom = zoomAmount;
            zoomAmount = tempZoom1;
            currentMaxZoom = maxZoom1;
            currentMinZoom = minZoom1;

        }
        else
        {
            tempZoom1 = zoomAmount;
            zoomAmount = tempZoom;
            currentMaxZoom = maxZoom;
            currentMinZoom = minZoom;
        }
        PlayerManagerEventHandler.MovementModifier?.Invoke(zoomAmount);
        cameraBase.m_Lens.OrthographicSize = zoomAmount;

    }
    private void VirtualCamOffsetLoad(float arg0)
    {
        zoomAmount = -arg0;
        //cameraOffset.m_Offset = new Vector3(0, 0, arg0);
    }

    public override void ExecuteWithFloat(float value)
    {

        // SaveLoadHandlers.VirtualCamOffset?.Invoke(cameraOffset.m_Offset.z);
        if (value < 0)
        {

            zoomAmount = Mathf.Clamp(zoomAmount *= zoomSpeed, currentMinZoom, currentMaxZoom);
        }
        else
        {
            zoomAmount = Mathf.Clamp(zoomAmount /= zoomSpeed, currentMinZoom, currentMaxZoom);
        }

        cameraBase.m_Lens.OrthographicSize = zoomAmount;
        PlayerManagerEventHandler.MovementModifier?.Invoke(zoomAmount);


    }
}
