using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RouteController : MonoBehaviour
{
    [SerializeField] private Route routePrefab;
    [SerializeField] private RouteListItem routeListItemPrefab;
    [SerializeField] private Transform routeListTransform;
    [SerializeField] private ToggleGroup routeListToggleGroup;
    public Dictionary<Route, RouteListItem> Routes;
    private void Awake()
    {
        Routes = new Dictionary<Route, RouteListItem>();
    }
    private void OnEnable()
    {
    }
    private void OnDisable()
    {
    }

    public void CreateEmptyRoute()
    {
        var route = Instantiate(routePrefab, transform);
        route.RouteColor = Random.ColorHSV(0f, 1f, 1f, 1f, 0.5f, 1f);
        var routeListItem = Instantiate(routeListItemPrefab, routeListTransform);
        routeListItem.transform.GetComponent<Toggle>().group = routeListToggleGroup;
        Routes.Add(route, routeListItem);
        var index = GetComponentsInChildren<Route>().Length;
        route.RouteName = "Solar route " + index.ToString();
        routeListItem.InitializeItem(route);
    }

}
