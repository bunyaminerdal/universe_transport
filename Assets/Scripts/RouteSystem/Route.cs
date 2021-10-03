using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Route : MonoBehaviour
{
    [SerializeField] LineRenderer lineRenderer;
    public List<RoutePart> routeParts;
    public List<CargoStation> CargoStations;
    public List<LineRenderer> lineRenderers;
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
        routeParts = new List<RoutePart>();
        CargoStations = new List<CargoStation>();
        TransportVehicles = new List<TransportVehicle>();
        transform.Clear();
    }
}
