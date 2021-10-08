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
        route.solarsForRoute.Enqueue(solar);
        List<SolarSystemStruct> solars = new List<SolarSystemStruct>();
        List<SolarSystemStruct> firstSolars = new List<SolarSystemStruct>();

        if (route.solarsForRoute.Count > 1)
        {
            firstSolars = FindPath(route.firstSolar.solarSystemStruct, solar.solarSystemStruct);
            RoutePart routePartEnd = new RoutePart(firstSolars);
            if (route.routeParts.Count < 1)
            {
                route.routeParts.Add(routePartEnd);
            }
            else
            {
                route.routeParts[0] = routePartEnd;
            }
            SolarSystemStruct firstOne = route.solarsForRoute.Dequeue().solarSystemStruct;
            SolarSystemStruct secondOne = route.solarsForRoute.Dequeue().solarSystemStruct;

            solars = FindPath(secondOne, firstOne);
            RoutePart routePart = new RoutePart(solars);
            route.routeParts.Add(routePart);
            route.solarsForRoute.Enqueue(solar);
            CreateRoute();
        }

    }
    private void CreateRoute()
    {
        route.ClearRoute();
        route.InitializeRoute();
        StationListInitializer();
    }
    public void DeleteRoute()
    {
        UIEventHandler.RouteDeleteEvent?.Invoke(route);
    }
    private List<SolarSystemStruct> FindPath(SolarSystemStruct startSolar, SolarSystemStruct endSolar)
    {
        List<SolarSystemStruct> routePart = PathFinderWithStruct.pathFindingWithDistance(endSolar, startSolar, solarClusters);
        return routePart;
    }

}
