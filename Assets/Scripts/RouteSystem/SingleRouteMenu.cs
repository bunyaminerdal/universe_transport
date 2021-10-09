using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class SingleRouteMenu : MonoBehaviour
{
    [SerializeField] private StationListItem stationListItemPrefab;
    [SerializeField] private Transform stationListTransform;
    [SerializeField] private ToggleGroup stationListToggleGroup;
    [SerializeField] private TMP_Text routeName;
    [SerializeField] private Image colorTexture;
    [SerializeField] private Button addButton;
    [SerializeField] private Button doneButton;

    private SolarClusterStruct[] solarClusters;
    private Route route;
    private List<StationListItem> stations;

    private void OnEnable()
    {
        PlayerManagerEventHandler.RoutePartInstantiateEvent.AddListener(RoutePartsInstantiate);
        UIEventHandler.RouteStationDeleteEvent.AddListener(DeleteRouteStation);
        UIEventHandler.RouteStationUpEvent.AddListener(StationUp);
        UIEventHandler.RouteStationDownEvent.AddListener(StationDown);
    }
    private void OnDisable()
    {
        PlayerManagerEventHandler.RoutePartInstantiateEvent.RemoveListener(RoutePartsInstantiate);
        UIEventHandler.RouteStationDeleteEvent.RemoveListener(DeleteRouteStation);
        UIEventHandler.RouteStationUpEvent.RemoveListener(StationUp);
        UIEventHandler.RouteStationDownEvent.RemoveListener(StationDown);
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
            station.transform.GetComponent<Toggle>().group = stationListToggleGroup;
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
    public void DeleteRoute()
    {
        UIEventHandler.RouteDeleteEvent?.Invoke(route);
    }
    private void RoutePartsInstantiate(SolarSystem solar)
    {
        if (route.firstSolar == null)
        {
            route.firstSolar = solar;
        }
        route.solarsForRoute.Push(solar);
        List<SolarSystemStruct> solars = new List<SolarSystemStruct>();
        List<SolarSystemStruct> lastSolars = new List<SolarSystemStruct>();

        if (route.solarsForRoute.Count > 1)
        {
            if (route.routeParts.Count > 1)
            {
                route.routeParts.RemoveAt(route.routeParts.Count - 1);
            }
            SolarSystemStruct newOne = route.solarsForRoute.Pop().solarSystemStruct;
            SolarSystemStruct lastOne = route.solarsForRoute.Pop().solarSystemStruct;
            //sonuncudan yeni eklenene routepart üretiyoruz.
            solars = FindPath(lastOne, newOne);
            RoutePart routePart = new RoutePart(solars);
            route.routeParts.Add(routePart);

            //yeni eklenenden ilk noktaya routepart üretiyoruz.
            lastSolars = FindPath(newOne, route.firstSolar.solarSystemStruct);
            RoutePart routePartEnd = new RoutePart(lastSolars);
            route.routeParts.Add(routePartEnd);

            //artık son nokta yeni eklenen oldu
            route.solarsForRoute.Push(solar);
            CreateRoute();
        }
    }
    private void CreateRoute()
    {
        route.ClearRoute();
        route.InitializeRoute();
        StationListInitializer();
    }

    private void DeleteRouteStation(SolarSystem solar)
    {
        if (route.routeParts.Count < 3) return;
        SolarSystemStruct beforeSolar = null;
        SolarSystemStruct afterSolar = null;
        SolarSystem lastSolar = route.solarsForRoute.Pop();
        for (int i = 0; i < route.routeParts.Count; i++)
        {
            if (route.routeParts[i].solars[0] == solar.solarSystemStruct)
            {
                afterSolar = route.routeParts[i].solars[route.routeParts[i].solars.Count - 1];
                route.routeParts.RemoveAt(i);
                i--;
            }
        }
        for (int i = 0; i < route.routeParts.Count; i++)
        {

            if (route.routeParts[i].solars[route.routeParts[i].solars.Count - 1] == solar.solarSystemStruct)
            {
                beforeSolar = route.routeParts[i].solars[0];

                List<SolarSystemStruct> solars = new List<SolarSystemStruct>();
                solars = FindPath(beforeSolar, afterSolar);
                RoutePart routePart = new RoutePart(solars);
                route.routeParts[i] = routePart;
            }
        }
        if (solar == lastSolar)
        {
            route.solarsForRoute.Push(beforeSolar.solarSystem);
        }
        else
        {
            route.solarsForRoute.Push(lastSolar);
        }
        if (route.firstSolar == solar)
        {
            route.firstSolar = afterSolar.solarSystem;
        }
        for (int i = 0; i < stations.Count; i++)
        {
            if (stations[i].solarSystem == solar)
            {
                stations.RemoveAt(i);
                i--;
            }
        }
        CreateRoute();
    }
    private void StationUp(SolarSystem solar)
    {

    }
    private void StationDown(SolarSystem solar)
    {
        // SolarSystem lastSolar = route.solarsForRoute.Dequeue();
        // if (solar == route.firstSolar)
        // {
        //     Debug.Log("first solar changing!");
        // }

        // if (solar == lastSolar)
        // {
        //     // route.solarsForRoute.Enqueue(solar);
        //     Debug.Log("last solar changing");
        // }
        // SolarSystemStruct firstSolar = null;
        // SolarSystemStruct secondSolar = null;

        // for (int i = 0; i < route.routeParts.Count; i++)
        // {
        //     if (route.routeParts[i].solars[0] == solar.solarSystemStruct)
        //     {
        //         firstSolar = route.routeParts[i].solars[route.routeParts[i].solars.Count - 1];
        //     }
        // }
        // for (int i = 0; i < route.routeParts.Count; i++)
        // {

        //     if (route.routeParts[i].solars[route.routeParts[i].solars.Count - 1] == solar.solarSystemStruct)
        //     {
        //         secondSolar = route.routeParts[i].solars[0];
        //         //bir alttaki ile arasındaki routePart ın yönünü değiştirdik.
        //         List<SolarSystemStruct> solars1 = new List<SolarSystemStruct>();
        //         solars1 = FindPath(secondSolar, solar.solarSystemStruct);
        //         RoutePart routePart1 = new RoutePart(solars1);
        //         route.routeParts[i] = routePart1;

        //     }
        //     if (route.routeParts[i].solars[0].solarSystem == lastSolar)
        //     {
        //         Debug.Log("going to lastsolar!");
        //     }

        //     Debug.Log(route.routeParts[i].solars[0].solarSystem.solarSystemName);
        // }
        // route.solarsForRoute.Enqueue(lastSolar);

    }
    private List<SolarSystemStruct> FindPath(SolarSystemStruct startSolar, SolarSystemStruct endSolar)
    {
        List<SolarSystemStruct> routePart = PathFinderWithStruct.pathFindingWithDistance(endSolar, startSolar, solarClusters);
        return routePart;
    }

}
