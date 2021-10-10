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

    private Route route;
    private Dictionary<int, StationListItem> stations;

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
        stations = new Dictionary<int, StationListItem>();
        for (int i = 0; i < route.routeParts.Count; i++)
        {
            var station = Instantiate(stationListItemPrefab, stationListTransform);
            station.index = i;
            station.transform.GetComponent<Toggle>().group = stationListToggleGroup;
            stations.Add(i, station);
            station.UpdateDisplay(route.routeParts[i].solars[0].solarSystem);
        }
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
    private void RoutePartsInstantiate(SolarSystem newSolar)
    {
        if (route.Solars.Count <= 0)
        {
            route.Solars.Add(newSolar);
            route.TempSolar(newSolar);
        }
        else
        {
            SolarSystemStruct firstSolar = route.Solars[0].solarSystemStruct;
            SolarSystemStruct lastSolar = route.Solars[route.Solars.Count - 1].solarSystemStruct;
            //failure checks 
            if (lastSolar == newSolar.solarSystemStruct) return;
            if (firstSolar == newSolar.solarSystemStruct) return;

            route.Solars.Add(newSolar);
            CreateRoute();
        }
    }
    private void CreateRoute()
    {
        route.ClearRoute();
        route.InitializeRoute();
        StationListInitializer();
    }

    private void DeleteRouteStation(int index)
    {
        if (route.routeParts.Count < 3) return;
        int beforeIndex = route.Solars.PreviousIndex(index);
        int afterIndex = route.Solars.NextIndex(index);

        SolarSystemStruct beforeSolar = route.Solars[beforeIndex].solarSystemStruct;
        SolarSystemStruct afterSolar = route.Solars[afterIndex].solarSystemStruct;

        if (beforeSolar == afterSolar)
        {
            route.Solars.RemoveAt(beforeIndex);
        }
        route.Solars.RemoveAt(index);
        CreateRoute();
    }

    //TODO: Up ve Down için de denetim yapmam lazım. aşağıda sıkıntı yok yukarı sıkıntılı.
    private void StationUp(int index)
    {
        //if (route.routeParts.Count < 3) return;
        int beforeIndex = route.Solars.PreviousIndex(index);
        int moreBeforeIndex = route.Solars.PreviousIndex(beforeIndex);
        int afterIndex = route.Solars.NextIndex(index);
        SolarSystemStruct moreBeforeSolar = route.Solars[moreBeforeIndex].solarSystemStruct;
        SolarSystemStruct beforeSolar = route.Solars[beforeIndex].solarSystemStruct;
        SolarSystemStruct afterSolar = route.Solars[afterIndex].solarSystemStruct;
        SolarSystemStruct currentSolar = route.Solars[index].solarSystemStruct;

        if (currentSolar == moreBeforeSolar)
        {
            Debug.Log("morebefore ile current aynı");
        }


        //current ile before arasındaki route un yönünü değişiyoruz.
        List<SolarSystemStruct> solars1 = new List<SolarSystemStruct>();
        solars1 = FindPath(currentSolar, beforeSolar);
        RoutePart routePart1 = new RoutePart(solars1);
        route.routeParts[beforeIndex] = routePart1;



        // if (route.routeParts.Count < 3) return;
        // SolarSystem lastSolar = route.solarsForRoute.Pop();

        // SolarSystemStruct beforeSolar = null;
        // SolarSystemStruct afterSolar = null;
        // SolarSystemStruct nextBeforeSolar = null;

        // for (int i = 0; i < route.routeParts.Count; i++)
        // {
        //     if (route.routeParts[i].solars[route.routeParts[i].solars.Count - 1] == solar.solarSystemStruct)
        //     {
        //         beforeSolar = route.routeParts[i].solars[0];
        //         List<SolarSystemStruct> solars1 = new List<SolarSystemStruct>();
        //         solars1 = FindPath(solar.solarSystemStruct, beforeSolar);
        //         RoutePart routePart1 = new RoutePart(solars1);
        //         route.routeParts[i] = routePart1;
        //     }
        // }

        // for (int i = 0; i < route.routeParts.Count; i++)
        // {
        //     if (route.routeParts[i].solars[route.routeParts[i].solars.Count - 1] == beforeSolar &&
        //       route.routeParts[i].solars[0] != solar.solarSystemStruct)
        //     {
        //         nextBeforeSolar = route.routeParts[i].solars[0];
        //         List<SolarSystemStruct> solars2 = new List<SolarSystemStruct>();
        //         solars2 = FindPath(nextBeforeSolar, solar.solarSystemStruct);
        //         RoutePart routePart2 = new RoutePart(solars2);
        //         route.routeParts[i] = routePart2;
        //     }
        // }


        // for (int i = 0; i < route.routeParts.Count; i++)
        // {

        //     if (route.routeParts[i].solars[0] == solar.solarSystemStruct)
        //     {
        //         if (route.routeParts[i].solars[route.routeParts[i].solars.Count - 1] != beforeSolar)
        //         {
        //             afterSolar = route.routeParts[i].solars[route.routeParts[i].solars.Count - 1];
        //             List<SolarSystemStruct> solars3 = new List<SolarSystemStruct>();
        //             solars3 = FindPath(beforeSolar, afterSolar);
        //             RoutePart routePart3 = new RoutePart(solars3);
        //             route.routeParts[i] = routePart3;
        //         }
        //     }

        // }


        // //eğer first soları değiştiriyorsak
        // if (solar == route.firstSolar)
        // {
        //     route.firstSolar = beforeSolar.solarSystem;
        //     route.solarsForRoute.Push(solar);
        // }
        // else if (beforeSolar.solarSystem == route.firstSolar)
        // {
        //     route.solarsForRoute.Push(lastSolar);
        //     route.firstSolar = solar;
        // }
        // else if (solar == lastSolar)
        // {
        //     route.solarsForRoute.Push(beforeSolar.solarSystem);
        // }
        // else
        // {
        //     route.solarsForRoute.Push(lastSolar);
        // }
        // CreateRoute();
    }
    private void StationDown(int index)
    {
        // if (route.routeParts.Count < 3) return;
        // SolarSystem lastSolar = route.solarsForRoute.Pop();

        // SolarSystemStruct beforeSolar = null;
        // SolarSystemStruct afterSolar = null;
        // SolarSystemStruct nextAfterSolar = null;

        // for (int i = 0; i < route.routeParts.Count; i++)
        // {
        //     if (route.routeParts[i].solars[0] == solar.solarSystemStruct)
        //     {
        //         afterSolar = route.routeParts[i].solars[route.routeParts[i].solars.Count - 1];
        //         //bir sonraki ile arasındaki routePart ın yönünü değiştirdik.
        //         List<SolarSystemStruct> solars1 = new List<SolarSystemStruct>();
        //         solars1 = FindPath(afterSolar, solar.solarSystemStruct);
        //         RoutePart routePart1 = new RoutePart(solars1);
        //         route.routeParts[i] = routePart1;

        //     }
        // }

        // //after solar ile yer değiştikten sonra current solar after soların yerine geçti ve current solar ile aftersolardan sonraki arasındaki bağlantıyı yeniliyoruz.
        // for (int i = 0; i < route.routeParts.Count; i++)
        // {
        //     if (route.routeParts[i].solars[0] == afterSolar &&
        //      route.routeParts[i].solars[route.routeParts[i].solars.Count - 1] != solar.solarSystemStruct)
        //     {
        //         nextAfterSolar = route.routeParts[i].solars[route.routeParts[i].solars.Count - 1];
        //         //yeni ile afterdan sonraki arasındaki routePart ı yeniledik.
        //         List<SolarSystemStruct> solar2 = new List<SolarSystemStruct>();
        //         solar2 = FindPath(solar.solarSystemStruct, nextAfterSolar);
        //         RoutePart routePart2 = new RoutePart(solar2);
        //         route.routeParts[i] = routePart2;
        //     }
        // }


        // for (int i = 0; i < route.routeParts.Count; i++)
        // {

        //     if (route.routeParts[i].solars[route.routeParts[i].solars.Count - 1] == solar.solarSystemStruct &&
        //      route.routeParts[i].solars[0] != afterSolar)
        //     {
        //         beforeSolar = route.routeParts[i].solars[0];
        //         //befor ile after arasındaki routePart ı yeniledik.
        //         List<SolarSystemStruct> solar3 = new List<SolarSystemStruct>();
        //         solar3 = FindPath(beforeSolar, afterSolar);
        //         RoutePart routePart3 = new RoutePart(solar3);
        //         route.routeParts[i] = routePart3;
        //     }
        // }
        // route.solarsForRoute.Push(lastSolar);
        // //eğer after solar last solar ise last soları after solar yapıyoruz.
        // if (afterSolar.solarSystem == lastSolar)
        // {
        //     route.solarsForRoute.Pop();
        //     route.solarsForRoute.Push(solar);
        // }
        // else if (solar == route.firstSolar)
        // {
        //     route.firstSolar = afterSolar.solarSystem;
        // }
        // else if (solar == lastSolar)
        // {
        //     route.solarsForRoute.Pop();
        //     route.solarsForRoute.Push(afterSolar.solarSystem);
        //     route.firstSolar = solar;
        // }
        // CreateRoute();
    }
    private List<SolarSystemStruct> FindPath(SolarSystemStruct startSolar, SolarSystemStruct endSolar)
    {
        if (startSolar == endSolar) return null;
        List<SolarSystemStruct> routePart = PathFinderWithStruct.pathFindingWithDistance(endSolar, startSolar, SolarClusterStruct.SolarClusterStructList);
        return routePart;
    }

}
