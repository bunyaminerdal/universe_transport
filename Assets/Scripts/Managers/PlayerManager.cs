using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    private SolarSystem selectedSolarSystem;
    private Vector3 lastPosition;
    private Camera cameraMain;
    private bool isSolarMapOpened;
    // public int targetFrameRate = 60;

    // private void Start()
    // {
    //     QualitySettings.vSyncCount = 0;
    //     Application.targetFrameRate = targetFrameRate;
    // }
    private void Awake()
    {
        cameraMain = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();

    }
    private void OnEnable()
    {
        PlayerManagerEventHandler.SolarSelection.AddListener(OpenSolarSystem);
    }


    // Update is called once per frame
    void Update()
    {

    }
    private void OnDisable()
    {
        PlayerManagerEventHandler.SolarSelection.RemoveListener(OpenSolarSystem);
    }

    public void OpenSolarSystem(SolarSystem solar)
    {
        if (isSolarMapOpened) CloseSolarSystem();
        selectedSolarSystem = solar;
        lastPosition = transform.position;
        transform.position = new Vector3(selectedSolarSystem.transform.position.x, 0, selectedSolarSystem.transform.position.z);
        selectedSolarSystem.ShowSystem();
        cameraMain.cullingMask = 119;
        isSolarMapOpened = true;
        selectedSolarSystem.gameObject.GetComponent<SphereCollider>().enabled = !isSolarMapOpened;
        selectedSolarSystem.gameObject.GetComponent<SphereCollider>().radius = 0;
        PlayerManagerEventHandler.MapChangeEvent?.Invoke(isSolarMapOpened);
        PlayerManagerEventHandler.BoundaryChangeEvent?.Invoke(isSolarMapOpened);

    }
    public void CloseSolarSystem()
    {
        if (!isSolarMapOpened) return;
        isSolarMapOpened = false;
        transform.position = lastPosition;
        cameraMain.cullingMask = 183;
        selectedSolarSystem.HideSystem();
        selectedSolarSystem.gameObject.GetComponent<SphereCollider>().enabled = !isSolarMapOpened;
        selectedSolarSystem.gameObject.GetComponent<SphereCollider>().radius = 150;
        PlayerManagerEventHandler.MapChangeEvent?.Invoke(isSolarMapOpened);
        PlayerManagerEventHandler.BoundaryChangeEvent?.Invoke(isSolarMapOpened);
    }

}
