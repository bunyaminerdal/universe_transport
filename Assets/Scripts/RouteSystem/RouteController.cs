using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RouteController : MonoBehaviour
{
    [SerializeField] private Route routePrefab;
    private SolarClusterStruct[] solarClusters;
    private Queue<SolarSystem> solarsForRoute;
    private List<RoutePart> routeParts;
    private SolarSystem firstSolar = null;
    public List<Route> Routes;
    private bool isRouteCreating;
    private Route CurrentRoute;
    private void Awake()
    {
        Routes = new List<Route>();
    }
    private void OnEnable()
    {
        PlayerManagerEventHandler.CreateRouteEvent.AddListener(CreateRoute);
        PlayerManagerEventHandler.RoutePartInstantiateEvent.AddListener(RoutePartsInstantiate);
        PlayerManagerEventHandler.SolarClustersReadyEvent.AddListener(TakeSolarClusters);
        PlayerManagerEventHandler.RouteCreateInteractionEvent.AddListener(RouteCreateInteraction);
    }



    private void OnDisable()
    {
        PlayerManagerEventHandler.CreateRouteEvent.RemoveListener(CreateRoute);
        PlayerManagerEventHandler.RoutePartInstantiateEvent.RemoveListener(RoutePartsInstantiate);
        PlayerManagerEventHandler.SolarClustersReadyEvent.RemoveListener(TakeSolarClusters);
        PlayerManagerEventHandler.RouteCreateInteractionEvent.RemoveListener(RouteCreateInteraction);

    }
    private void CreateRoute(List<RoutePart> routeParts)
    {
        PrepareRoute();
        CurrentRoute.ClearRoute();
        foreach (var routePart in routeParts)
        {
            CurrentRoute.routeParts.Add(routePart);
        }
        CurrentRoute.InitializeRoute();
    }
    private void PrepareRoute()
    {
        if (CurrentRoute != null) return;
        CurrentRoute = Instantiate(routePrefab, transform);
        CurrentRoute.RouteColor = Random.ColorHSV(0f, 1f, 1f, 1f, 0.5f, 1f);
        CurrentRoute.isOpened = true;
        Routes.Add(CurrentRoute);
    }

    private void RouteCreateInteraction()
    {
        isRouteCreating = !isRouteCreating;
        if (isRouteCreating)
        {
            routeParts = new List<RoutePart>();
            solarsForRoute = new Queue<SolarSystem>();
        }
        else
        {
            if (routeParts != null && routeParts.Count > 0)
            {
                CreateRoute(routeParts);
                firstSolar = null;
                CurrentRoute = null;
            }
        }
    }
    private void RoutePartsInstantiate(SolarSystem solar)
    {
        if (firstSolar == null)
        {
            firstSolar = solar;
            PrepareRoute();
        }
        solarsForRoute.Enqueue(solar);
        List<SolarSystemStruct> solars = new List<SolarSystemStruct>();
        List<SolarSystemStruct> firstSolars = new List<SolarSystemStruct>();

        if (solarsForRoute.Count > 1)
        {
            firstSolars = FindPath(solar.solarSystemStruct, firstSolar.solarSystemStruct);
            RoutePart routePartEnd = new RoutePart(firstSolars);
            if (routeParts.Count < 1)
            {
                routeParts.Add(routePartEnd);
            }
            else
            {
                routeParts[0] = routePartEnd;
            }


            solars = FindPath(solarsForRoute.Dequeue().solarSystemStruct, solarsForRoute.Dequeue().solarSystemStruct);
            RoutePart routePart = new RoutePart(solars);
            routeParts.Add(routePart);
            solarsForRoute.Enqueue(solar);
            CreateRoute(routeParts);
        }

    }
    private List<SolarSystemStruct> FindPath(SolarSystemStruct startSolar, SolarSystemStruct endSolar)
    {
        List<SolarSystemStruct> routePart = PathFinderWithStruct.pathFindingWithDistance(endSolar, startSolar, solarClusters);
        return routePart;
    }
    private void TakeSolarClusters(SolarClusterStruct[] clusters) => solarClusters = clusters;
}
