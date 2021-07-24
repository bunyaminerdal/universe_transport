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
        PlayerManagerEventHandler.RotationBillboard.AddListener(BillboardRotation);
    }
    private void OnDisable()
    {
        PlayerManagerEventHandler.RotationBillboard.RemoveListener(BillboardRotation);
    }


    private void BillboardRotation()
    {
        transform.forward = cameraMain.transform.forward;
    }
}
