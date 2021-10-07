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
    [Header("Prefabs")]
    [SerializeField] private StationListItem stationListItemPrefab;


    public Route route;

    private List<StationListItem> stations;
    private void OnEnable()
    {
        UIEventHandler.StationListItemCreateEvent.AddListener(StationListInitializer);
    }
    private void OnDisable()
    {
        UIEventHandler.StationListItemCreateEvent.RemoveListener(StationListInitializer);
    }

    public void UpdateDisplay(Route _route, bool isOn)
    {
        if (route != null && route.isEditing)
        {
            PlayerManagerEventHandler.RouteCreateInteractionEvent?.Invoke();
            UIEventHandler.RouteMenuCloseEvent?.Invoke();
        }
        route = _route;
        routeName.text = _route.RouteName;
        colorTexture.color = _route.RouteColor;
    }

    public void StationListInitializer(Route _route)
    {
        route = _route;
        stationListTransform.Clear();
        stations = new List<StationListItem>();
        for (int i = 0; i < route.routeParts.Count; i++)
        {
            var station = Instantiate(stationListItemPrefab, stationListTransform);
            stations.Add(station);
            station.UpdateDisplay(route.routeParts[i].solars[0].solarSystem);
        }

    }


}
