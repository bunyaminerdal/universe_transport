using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RouteController : MonoBehaviour
{
    [SerializeField] private Route routePrefab;
    private SolarClusterStruct[] solarClusters;
    private Queue<SolarSystem> solarsForRoute;
    private Queue<RoutePart> routeParts;
    private SolarSystem firstSolar = null;
    public List<Route> Routes;
    private bool isRouteCreating;
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
    private void CreateRoute(Queue<RoutePart> routeParts)
    {
        Route route = Instantiate(routePrefab, transform);
        foreach (var routePart in routeParts)
        {
            route.routeParts.Add(routePart);
        }
        route.RouteColor = Random.ColorHSV(0f, 1f, 1f, 1f, 0.5f, 1f);
        route.isOpened = true;
        route.InitializeRoute();
        Routes.Add(route);
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
                CreateRoute(routeParts);
                firstSolar = null;
            }
        }
    }
    private void RoutePartsInstantiate(SolarSystem solar)
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
    private List<SolarSystemStruct> FindPath(SolarSystemStruct startSolar, SolarSystemStruct endSolar)
    {
        List<SolarSystemStruct> routePart = PathFinderWithStruct.pathFindingWithDistance(endSolar, startSolar, solarClusters);
        return routePart;
    }
    private void TakeSolarClusters(SolarClusterStruct[] clusters) => solarClusters = clusters;
}
