using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Route : MonoBehaviour
{
    public static List<Route> RouteList = new List<Route>();
    public List<SolarSystem[]> Roads;
    public List<CargoStation> CargoStations;
    public Color RouteColor;
    public string RouteName;
    public List<TransportVehicle> TransportVehicles;
    private void Awake()
    {
        RouteList.Add(this);
    }

}
