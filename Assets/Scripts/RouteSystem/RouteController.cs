using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RouteController : MonoBehaviour
{
    [SerializeField] private Route routePrefab;
    public List<Route> Routes;

    public Roads roads;
    private void Awake()
    {
        Routes = new List<Route>();
    }
    private void OnEnable()
    {
        PlayerManagerEventHandler.CreateRoute.AddListener(CreateRoute);
        PlayerManagerEventHandler.RoadsCreated.AddListener(TakeRoads);
        for (int i = 0; i < 10; i++)
        {
            Instantiate(routePrefab, transform);
        }
    }

    private void OnDisable()
    {
        PlayerManagerEventHandler.CreateRoute.RemoveListener(CreateRoute);
        PlayerManagerEventHandler.RoadsCreated.RemoveListener(TakeRoads);
    }
    private void CreateRoute(List<SolarSystemStruct> _solars)
    {
        Route route = Instantiate(routePrefab, transform);

        RoutePart routePart = new RoutePart(_solars);
        route.routeParts.Add(routePart);
        List<SolarSystemStruct> solarReverse = new List<SolarSystemStruct>();
        solarReverse.CopyTo(_solars.ToArray());
        solarReverse.Reverse();
        RoutePart routePartOpossite = new RoutePart(solarReverse);
        route.routeParts.Add(routePartOpossite);
        route.RouteColor = Random.ColorHSV(0f, 1f, 1f, 1f, 0.5f, 1f);
        route.isOpened = true;
        route.InitializeRoute();
        Routes.Add(route);
    }

    private void TakeRoads(Roads _roads)
    {
        roads = new Roads();
        roads = _roads;
    }
}
