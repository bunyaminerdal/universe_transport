using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Route : MonoBehaviour
{
    public List<RoutePart> routeParts;
    public List<CargoStation> CargoStations;
    public Color RouteColor;
    public string RouteName;
    public List<TransportVehicle> TransportVehicles;
    public bool isOpened;
    private void Awake()
    {
        routeParts = new List<RoutePart>();
        CargoStations = new List<CargoStation>();
        TransportVehicles = new List<TransportVehicle>();
    }
    public void InitializeRoute()
    {
        foreach (var routePart in routeParts)
        {
            for (int i = 0; i < routePart.solars.Count - 1; i++)
            {
                Debug.DrawLine(routePart.solars[i].solarLocation, routePart.solars[i + 1].solarLocation, RouteColor, 360.0f);
            }
        }
    }
}

public class RoutePart
{
    public List<SolarSystemStruct> solars;

    public RoutePart(List<SolarSystemStruct> solars)
    {
        this.solars = solars;
    }
}
