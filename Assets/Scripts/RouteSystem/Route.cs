using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Route : MonoBehaviour
{
    [SerializeField] LineRenderer lineRenderer;
    [SerializeField] GameObject stationCircle;
    public List<RoutePart> routeParts;
    public List<CargoStation> CargoStations;
    public List<LineRenderer> lineRenderers;
    public Color RouteColor;
    public string RouteName;
    public List<TransportVehicle> TransportVehicles;
    public bool isEditing = false;
    public SolarSystem firstSolar;
    public Stack<SolarSystem> solarsForRoute;
    private void Awake()
    {
        routeParts = new List<RoutePart>();
        CargoStations = new List<CargoStation>();
        TransportVehicles = new List<TransportVehicle>();
        solarsForRoute = new Stack<SolarSystem>();
    }

    public void InitializeRoute()
    {
        foreach (var routePart in routeParts)
        {
            GameObject solarNode = Instantiate(stationCircle, transform);
            solarNode.transform.position = routePart.solars[0].solarLocation;
            for (int i = 0; i < routePart.solars.Count - 1; i++)
            {
                LineRenderer line = Instantiate(lineRenderer, transform);
                line.startColor = RouteColor;
                line.endColor = RouteColor;
                line.SetPosition(0, routePart.solars[i].solarLocation + Vector3.up);
                line.SetPosition(1, routePart.solars[i + 1].solarLocation + Vector3.up);
                lineRenderers.Add(line);

            }

        }
    }

    public void ClearRoute()
    {
        transform.Clear();
    }


    public void ShowRoute(bool isActive)
    {
        if (isActive)
        {
            transform.ShowAll();
        }
        else
        {
            transform.HideAll();
        }
    }


}
