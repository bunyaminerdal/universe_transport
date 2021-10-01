using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BillBoardEffect : MonoBehaviour
{
    private Camera cameraMain;


    private void Awake()
    {
        cameraMain = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
    }
    private void OnEnable()
    {
        PlayerManagerEventHandler.RotationBillboardEvent.AddListener(BillboardRotation);
    }
    private void OnDisable()
    {
        PlayerManagerEventHandler.RotationBillboardEvent.RemoveListener(BillboardRotation);
    }


    private void BillboardRotation()
    {
        transform.forward = cameraMain.transform.forward;
    }
}
