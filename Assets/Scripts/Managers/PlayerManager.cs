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
        UIEventHandler.RouteCreatingBegunEvent.AddListener(RouteCreatingEnded);
        UIEventHandler.ConstructionBegunEvent.AddListener(ConstructionBegun);
        UIEventHandler.ConstructionEndedEvent.AddListener(ConstructionEnd);

    }


    // Update is called once per frame
    void Update()
    {

    }
    private void OnDisable()
    {
        PlayerManagerEventHandler.SolarSelectionEvent.RemoveListener(ClickSolarSystem);
        UIEventHandler.RouteCreatingBegunEvent.RemoveListener(RouteCreatingEnded);
        UIEventHandler.ConstructionBegunEvent.RemoveListener(ConstructionBegun);
        UIEventHandler.ConstructionEndedEvent.RemoveListener(ConstructionEnd);
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
            case PlayType.Construction:
                break;
            default:
                Debug.Log("Wrong PlayType!");
                break;
        }
    }

    private void OpenSolarSystem(SolarSystem solar)
    {
        if (playType != PlayType.Map) return;
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
        //TODO: bunu neden koyduğumu unuttum
        // if (playType != PlayType.InSolar) return;
        transform.position = lastPosition;
        cameraMain.cullingMask = 183;
        selectedSolarSystem.HideSystem();
        selectedSolarSystem.gameObject.GetComponent<SphereCollider>().enabled = true;
        PlayerManagerEventHandler.MapChangeEvent?.Invoke(false);
        PlayerManagerEventHandler.BoundaryChangeEvent?.Invoke(false);
        if (playType == PlayType.Construction) ConstructionEnd();
        playType = PlayType.Map;
    }

    public void RouteCreatingBegun()
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
                break;
            case PlayType.Menu:
                break;
            case PlayType.Construction:
                CloseSolarSystem();
                playType = PlayType.Route;
                break;
            default:
                break;
        }
    }
    public void RouteCreatingEnded()
    {
        switch (playType)
        {
            case PlayType.Map:
                break;
            case PlayType.InSolar:
                break;
            case PlayType.Route:
                playType = PlayType.Map;
                break;
            case PlayType.Menu:
                break;
            case PlayType.Construction:
                break;
            default:
                break;
        }
    }
    public void ConstructionBegun(GameObject prefab)
    {
        switch (playType)
        {
            case PlayType.Map:
                break;
            case PlayType.InSolar:
                playType = PlayType.Construction;
                selectedSolarSystem.ShowConstructionNodes(true, prefab);
                break;
            case PlayType.Route:
                break;
            case PlayType.Menu:
                break;
            case PlayType.Construction:
                selectedSolarSystem.ShowConstructionNodes(true, prefab);
                break;
            default:
                break;
        }
    }

    public void ConstructionEnd()
    {
        switch (playType)
        {
            case PlayType.Map:
                break;
            case PlayType.InSolar:
                break;
            case PlayType.Route:
                break;
            case PlayType.Menu:
                break;
            case PlayType.Construction:
                selectedSolarSystem.ShowConstructionNodes(false, null);
                playType = PlayType.InSolar;
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
    Menu,
    Construction
}

