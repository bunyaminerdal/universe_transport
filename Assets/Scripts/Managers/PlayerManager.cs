using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    private SolarSystem selectedSolarSystem;
    private Vector3 lastPosition;
    private Camera cameraMain;
    private bool isSolarMapOpened;
    private void Awake()
    {
        cameraMain = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();

    }
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

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
        selectedSolarSystem.gameObject.GetComponent<Collider>().enabled = !isSolarMapOpened;
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
        selectedSolarSystem.gameObject.GetComponent<Collider>().enabled = !isSolarMapOpened;
        PlayerManagerEventHandler.MapChangeEvent?.Invoke(isSolarMapOpened);
        PlayerManagerEventHandler.BoundaryChangeEvent?.Invoke(isSolarMapOpened);
    }

}
