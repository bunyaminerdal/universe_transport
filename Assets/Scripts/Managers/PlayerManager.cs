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
    private Queue<SolarSystem> solarsForRoute;
    private Queue<RoutePart> routeParts;
    private SolarSystem firstSolar = null;
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
            if (firstSolar == null) firstSolar = solar;
            solarsForRoute.Enqueue(solar);
            List<SolarSystemStruct> solars = new List<SolarSystemStruct>();
            List<SolarSystemStruct> firstSolars = new List<SolarSystemStruct>();

            if (solarsForRoute.Count > 1)
            {
                if (routeParts.Count > 0) routeParts.Dequeue();
                solars = FindPath(solarsForRoute.Dequeue().solarSystemStruct, solarsForRoute.Dequeue().solarSystemStruct);
                RoutePart routePart = new RoutePart(solars);
                routeParts.Enqueue(routePart);
                firstSolars = FindPath(solar.solarSystemStruct, firstSolar.solarSystemStruct);
                RoutePart routePartEnd = new RoutePart(firstSolars);
                routeParts.Enqueue(routePartEnd);
                solarsForRoute.Enqueue(solar);
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
            routeParts = new Queue<RoutePart>();
            solarsForRoute = new Queue<SolarSystem>();
        }
        else
        {
            if (routeParts != null && routeParts.Count > 0)
            {
                PlayerManagerEventHandler.CreateRoute?.Invoke(routeParts);
                firstSolar = null;
            }
        }

    }
    private List<SolarSystemStruct> FindPath(SolarSystemStruct startSolar, SolarSystemStruct endSolar)
    {
        List<SolarSystemStruct> routePart = PathFinderWithStruct.pathFindingWithDistance(endSolar, startSolar, solarClusters);
        return routePart;
    }

}
