using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    private SolarSystem selectedSolarSystem;
    private Vector3 lastPosition;
    private Camera cameraMain;
    private bool isSolarMapOpened;
    private bool isRouteCreating;
    private SolarClusterStruct[] solarClusters;
    private List<SolarSystem> solarsForRoute;
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
        PlayerManagerEventHandler.RouteCreateInteraction.AddListener(RouteCreateInteraction);
        PlayerManagerEventHandler.SolarClustersReadyEvent.AddListener(TakeSolarClusters);
    }


    // Update is called once per frame
    void Update()
    {

    }
    private void OnDisable()
    {
        PlayerManagerEventHandler.SolarSelection.RemoveListener(OpenSolarSystem);
        PlayerManagerEventHandler.RouteCreateInteraction.RemoveListener(RouteCreateInteraction);
        PlayerManagerEventHandler.SolarClustersReadyEvent.RemoveListener(TakeSolarClusters);

    }

    public void OpenSolarSystem(SolarSystem solar)
    {
        if (isSolarMapOpened) CloseSolarSystem();
        if (!isRouteCreating)
        {
            selectedSolarSystem = solar;
            lastPosition = transform.position;
            transform.position = new Vector3(selectedSolarSystem.transform.position.x, 0, selectedSolarSystem.transform.position.z);
            selectedSolarSystem.ShowSystem();
            cameraMain.cullingMask = 119;
            isSolarMapOpened = true;
            selectedSolarSystem.gameObject.GetComponent<SphereCollider>().enabled = !isSolarMapOpened;
            PlayerManagerEventHandler.MapChangeEvent?.Invoke(isSolarMapOpened);
            PlayerManagerEventHandler.BoundaryChangeEvent?.Invoke(isSolarMapOpened);
        }
        else
        {
            solarsForRoute.Add(solar);
            if (solarsForRoute.Count > 1)
            {
                for (int i = 0; i < solarsForRoute.Count - 1; i++)
                {
                    FindPath(solarsForRoute[i].solarSystemStruct, solarsForRoute[i + 1].solarSystemStruct);
                }
            }
        }


    }
    private void TakeSolarClusters(SolarClusterStruct[] clusters)
    {
        solarClusters = clusters;
    }
    public void CloseSolarSystem()
    {
        if (!isSolarMapOpened) return;
        isSolarMapOpened = false;
        transform.position = lastPosition;
        cameraMain.cullingMask = 183;
        selectedSolarSystem.HideSystem();
        selectedSolarSystem.gameObject.GetComponent<SphereCollider>().enabled = !isSolarMapOpened;
        PlayerManagerEventHandler.MapChangeEvent?.Invoke(isSolarMapOpened);
        PlayerManagerEventHandler.BoundaryChangeEvent?.Invoke(isSolarMapOpened);
    }
    private void RouteCreateInteraction()
    {
        isRouteCreating = !isRouteCreating;
        if (isRouteCreating)
        {
            solarsForRoute = new List<SolarSystem>();
        }

    }
    private void FindPath(SolarSystemStruct startSolar, SolarSystemStruct endSolar)
    {
        List<SolarSystemStruct> route = new List<SolarSystemStruct>();
        route = PathFinderWithStruct.pathFindingWithDistance(endSolar, startSolar, solarClusters);
        PlayerManagerEventHandler.CreateRoute?.Invoke(route);
    }

}
