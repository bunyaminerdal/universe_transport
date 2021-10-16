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
            station.UpdateDisplay(route.routeParts[i].solars[0]);
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
            SolarSystem firstSolar = route.Solars[0];
            SolarSystem lastSolar = route.Solars[route.Solars.Count - 1];
            //failure checks 
            if (lastSolar == newSolar) return;
            if (firstSolar == newSolar) return;

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

        SolarSystem beforeSolar = route.Solars[beforeIndex];
        SolarSystem afterSolar = route.Solars[afterIndex];

        if (beforeSolar == afterSolar)
        {
            route.Solars.RemoveAt(beforeIndex);
        }
        route.Solars.RemoveAt(index);
        CreateRoute();
    }

    private void StationUp(int index)
    {
        int beforeIndex = route.Solars.PreviousIndex(index);
        int moreBeforeIndex = route.Solars.PreviousIndex(beforeIndex);
        int afterIndex = route.Solars.NextIndex(index);
        SolarSystem moreBeforeSolar = route.Solars[moreBeforeIndex];
        SolarSystem beforeSolar = route.Solars[beforeIndex];
        SolarSystem afterSolar = route.Solars[afterIndex];
        SolarSystem currentSolar = route.Solars[index];

        route.Solars[beforeIndex] = currentSolar;
        route.Solars[index] = beforeSolar;

        if (route.routeParts.Count > 2)
        {
            if (currentSolar == moreBeforeSolar)
            {
                route.Solars.RemoveAt(beforeIndex);
            }
            if (afterSolar == beforeSolar)
            {
                route.Solars.RemoveAt(index);
            }
        }

        CreateRoute();
    }
    private void StationDown(int index)
    {
        int afterIndex = route.Solars.NextIndex(index);
        int moreAfterIndex = route.Solars.NextIndex(afterIndex);
        int beforeIndex = route.Solars.PreviousIndex(index);
        SolarSystem moreAfterSolar = route.Solars[moreAfterIndex];
        SolarSystem beforeSolar = route.Solars[beforeIndex];
        SolarSystem afterSolar = route.Solars[afterIndex];
        SolarSystem currentSolar = route.Solars[index];

        route.Solars[afterIndex] = currentSolar;
        route.Solars[index] = afterSolar;

        if (route.routeParts.Count > 2)
        {
            if (currentSolar == moreAfterSolar)
            {
                route.Solars.RemoveAt(afterIndex);
            }
            if (afterSolar == beforeSolar)
            {
                route.Solars.RemoveAt(index);
            }
        }
        CreateRoute();
    }
    private List<SolarSystem> FindPath(SolarSystemStruct startSolar, SolarSystemStruct endSolar)
    {
        if (startSolar == endSolar) return null;
        List<SolarSystem> routePart = PathFinderWithStruct.pathFindingWithDistance(endSolar, startSolar, SolarClusterStruct.SolarClusterStructList);
        return routePart;
    }

}
