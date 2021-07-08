using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    private SolarSystem selectedSolarSystem;
    private Vector3 lastPosition;
    [SerializeField]
    private float cameraDepth = -500.0f;
    public SolarSystem SelectedSolarSystem { get => selectedSolarSystem; set { selectedSolarSystem = value; OpenSolarSystem(); } }
    private Camera cameraMain;
    private bool isSolarMapOpened;
    private int lastCameraCulling;

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

    public void OpenSolarSystem()
    {
        if (!isSolarMapOpened)
        {
            if (!selectedSolarSystem) return;
            lastPosition = transform.position;
            transform.position = new Vector3(selectedSolarSystem.transform.position.x, cameraDepth, selectedSolarSystem.transform.position.z);
            selectedSolarSystem.ShowSystem(cameraDepth);
            lastCameraCulling = cameraMain.cullingMask;
            cameraMain.cullingMask = ~0;
            isSolarMapOpened = true;
            PlayerManagerEventHandler.MapChangeEvent?.Invoke(isSolarMapOpened);
            PlayerManagerEventHandler.BoundryChangeEvent?.Invoke(isSolarMapOpened);
        }
        else
        {
            isSolarMapOpened = false;
            transform.position = lastPosition;
            cameraMain.cullingMask = lastCameraCulling;
            selectedSolarSystem.HideSystem();
            PlayerManagerEventHandler.MapChangeEvent?.Invoke(isSolarMapOpened);
            PlayerManagerEventHandler.BoundryChangeEvent?.Invoke(isSolarMapOpened);
        }

    }
}
