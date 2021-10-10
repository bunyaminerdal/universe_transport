using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Route : MonoBehaviour
{
    [SerializeField] LineRenderer lineRenderer;
    [SerializeField] GameObject stationCircle;
    public Dictionary<int, RoutePart> routeParts;
    public List<CargoStation> CargoStations;
    public List<LineRenderer> lineRenderers;
    public Color RouteColor;
    public string RouteName;
    public List<TransportVehicle> TransportVehicles;
    public bool isEditing = false;
    public List<SolarSystem> Solars;
    private void Awake()
    {
        routeParts = new Dictionary<int, RoutePart>();
        CargoStations = new List<CargoStation>();
        TransportVehicles = new List<TransportVehicle>();
    }

    public void InitializeRoute()
    {


        foreach (var routePart in routeParts)
        {
            GameObject solarNode = Instantiate(stationCircle, transform);
            solarNode.transform.position = routePart.Value.solars[0].solarLocation;
            for (int i = 0; i < routePart.Value.solars.Count - 1; i++)
            {
                LineRenderer line = Instantiate(lineRenderer, transform);
                line.startColor = RouteColor;
                line.endColor = RouteColor;
                line.SetPosition(0, routePart.Value.solars[i].solarLocation + Vector3.up);
                line.SetPosition(1, routePart.Value.solars[i + 1].solarLocation + Vector3.up);
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
