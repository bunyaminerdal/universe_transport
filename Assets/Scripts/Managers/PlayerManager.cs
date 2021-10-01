using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    private SolarSystem selectedSolarSystem;
    private Vector3 lastPosition;
    private Camera cameraMain;
    private PlayType playType = PlayType.Map;

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
        PlayerManagerEventHandler.SolarSelectionEvent.AddListener(ClickSolarSystem);
        PlayerManagerEventHandler.RouteCreateInteractionEvent.AddListener(RouteCreateInteraction);
    }


    // Update is called once per frame
    void Update()
    {

    }
    private void OnDisable()
    {
        PlayerManagerEventHandler.SolarSelectionEvent.RemoveListener(ClickSolarSystem);
        PlayerManagerEventHandler.RouteCreateInteractionEvent.RemoveListener(RouteCreateInteraction);

    }

    public void ClickSolarSystem(SolarSystem solar)
    {
        switch (playType)
        {
            case PlayType.Map:
                OpenSolarSystem(solar);
                break;
            case PlayType.InSolar:
                CloseSolarSystem();
                OpenSolarSystem(solar);
                break;
            case PlayType.Route:
                PlayerManagerEventHandler.RoutePartInstantiateEvent?.Invoke(solar);
                break;
            case PlayType.Menu:
                break;
            default:
                Debug.Log("Wrong PlayType!");
                break;
        }
    }

    private void OpenSolarSystem(SolarSystem solar)
    {
        selectedSolarSystem = solar;
        lastPosition = transform.position;
        transform.position = new Vector3(selectedSolarSystem.transform.position.x, 0, selectedSolarSystem.transform.position.z);
        selectedSolarSystem.ShowSystem();
        cameraMain.cullingMask = 119;
        selectedSolarSystem.gameObject.GetComponent<SphereCollider>().enabled = false;
        PlayerManagerEventHandler.MapChangeEvent?.Invoke(true);
        PlayerManagerEventHandler.BoundaryChangeEvent?.Invoke(true);
        playType = PlayType.InSolar;
    }
    public void CloseSolarSystem()
    {
        transform.position = lastPosition;
        cameraMain.cullingMask = 183;
        selectedSolarSystem.HideSystem();
        selectedSolarSystem.gameObject.GetComponent<SphereCollider>().enabled = true;
        PlayerManagerEventHandler.MapChangeEvent?.Invoke(false);
        PlayerManagerEventHandler.BoundaryChangeEvent?.Invoke(false);
        playType = PlayType.Map;
    }


    private void RouteCreateInteraction()
    {
        switch (playType)
        {
            case PlayType.Map:
                playType = PlayType.Route;
                break;
            case PlayType.InSolar:
                CloseSolarSystem();
                playType = PlayType.Route;
                break;
            case PlayType.Route:
                playType = PlayType.Map;
                break;
            case PlayType.Menu:
                break;
            default:
                break;
        }
    }
}
public enum PlayType
{
    Map,
    InSolar,
    Route,
    Menu
}

