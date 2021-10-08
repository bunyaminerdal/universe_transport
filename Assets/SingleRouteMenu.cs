using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class SingleRouteMenu : MonoBehaviour
{
    [SerializeField] private Transform stationListTransform;
    [SerializeField] private TMP_Text routeName;
    [SerializeField] private Image colorTexture;

    [SerializeField] private Button addButton;
    [SerializeField] private Button doneButton;

    [Header("Prefabs")]
    [SerializeField] private StationListItem stationListItemPrefab;

    private SolarClusterStruct[] solarClusters;

    public Route route;

    private List<StationListItem> stations;
    private Queue<SolarSystem> solarsForRoute;
    private List<RoutePart> routeParts;

    private void OnEnable()
    {
        PlayerManagerEventHandler.RoutePartInstantiateEvent.AddListener(RoutePartsInstantiate);
    }
    private void OnDisable()
    {
        PlayerManagerEventHandler.RoutePartInstantiateEvent.RemoveListener(RoutePartsInstantiate);
    }

    public void UpdateDisplay(Route _route, bool isOn)
    {
        route = _route;
        routeName.text = _route.RouteName;
        colorTexture.color = _route.RouteColor;
        StationListInitializer();
    }

    public void StationListInitializer()
    {
        stationListTransform.Clear();
        stations = new List<StationListItem>();
        for (int i = 0; i < route.routeParts.Count; i++)
        {
            var station = Instantiate(stationListItemPrefab, stationListTransform);
            stations.Add(station);
            station.UpdateDisplay(route.routeParts[i].solars[0].solarSystem);
        }

    }
    public void TakeClusters(SolarClusterStruct[] _solarClusters)
    {
        solarClusters = _solarClusters;
    }
    public void RouteCreatingBegun()
    {
        solarsForRoute = new Queue<SolarSystem>();
        routeParts = new List<RoutePart>();
        if (route.routeParts.Count > 0)
        {
            Debug.Log(route.firstSolar.name);

            for (int i = route.routeParts.Count; i < 0; i--)
            {
                solarsForRoute.Enqueue(route.routeParts[i].solars[0].solarSystem);
            }
            solarsForRoute.Enqueue(route.firstSolar);

            routeParts = route.routeParts;
        }

        ButtonChanger(true);
    }

    private void ButtonChanger(bool isActive)
    {
        addButton.gameObject.SetActive(!isActive);
        doneButton.gameObject.SetActive(isActive);
    }

    public void RouteCreatingEnded()
    {
        ButtonChanger(false);
    }

    private void RoutePartsInstantiate(SolarSystem solar)
    {
        if (route.firstSolar == null)
        {
            route.firstSolar = solar;
        }
        solarsForRoute.Enqueue(solar);
        List<SolarSystemStruct> solars = new List<SolarSystemStruct>();
        List<SolarSystemStruct> firstSolars = new List<SolarSystemStruct>();

        if (solarsForRoute.Count > 1)
        {
            firstSolars = FindPath(route.firstSolar.solarSystemStruct, solar.solarSystemStruct);
            RoutePart routePartEnd = new RoutePart(firstSolars);
            if (routeParts.Count < 1)
            {
                routeParts.Add(routePartEnd);
            }
            else
            {
                routeParts[0] = routePartEnd;
            }
            SolarSystemStruct firstOne = solarsForRoute.Dequeue().solarSystemStruct;
            SolarSystemStruct secondOne = solarsForRoute.Dequeue().solarSystemStruct;

            solars = FindPath(secondOne, firstOne);
            RoutePart routePart = new RoutePart(solars);
            routeParts.Add(routePart);
            solarsForRoute.Enqueue(solar);
            CreateRoute(routeParts);
        }

    }
    private void CreateRoute(List<RoutePart> routeParts)
    {
        route.ClearRoute();
        foreach (var routePart in routeParts)
        {
            route.routeParts.Add(routePart);
        }
        route.InitializeRoute();
        StationListInitializer();
    }
    private List<SolarSystemStruct> FindPath(SolarSystemStruct startSolar, SolarSystemStruct endSolar)
    {
        List<SolarSystemStruct> routePart = PathFinderWithStruct.pathFindingWithDistance(endSolar, startSolar, solarClusters);
        return routePart;
    }


}
