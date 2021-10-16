using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Route : MonoBehaviour
{
    [SerializeField] LineRenderer lineRenderer;
    [SerializeField] RouteNodeClass stationCircle;
    public List<RoutePart> routeParts;
    public List<CargoStation> CargoStations;
    public List<LineRenderer> lineRenderers;
    public Color RouteColor;
    public string RouteName;
    public List<TransportVehicle> TransportVehicles;
    public bool isEditing = false;
    public List<SolarSystem> Solars;
    private List<RouteNodeClass> solarNodes;
    private void Awake()
    {
        routeParts = new List<RoutePart>();
        CargoStations = new List<CargoStation>();
        TransportVehicles = new List<TransportVehicle>();
        solarNodes = new List<RouteNodeClass>();
    }

    public void InitializeRoute()
    {
        for (int i = 0; i < Solars.Count; i++)
        {
            int nextIndex = Solars.NextIndex(i);

            SolarSystem currentSolar = Solars[i];
            SolarSystem nextSolar = Solars[nextIndex];


            List<SolarSystem> solars = new List<SolarSystem>();
            solars = FindPath(currentSolar.solarSystemStruct, nextSolar.solarSystemStruct);
            RoutePart routePart = new RoutePart(solars);
            routeParts.Add(routePart);

        }

        foreach (var routePart in routeParts)
        {
            RouteNodeClass oldOne = null;
            RouteNodeClass solarNode = Instantiate(stationCircle, transform);
            solarNode.nodePosition = routePart.solars[0].transform.position;
            foreach (var nodeClass in solarNodes)
            {
                if (nodeClass.nodePosition == solarNode.nodePosition)
                {
                    oldOne = nodeClass;
                }
            }
            if (oldOne == null)
            {
                solarNodes.Add(solarNode);
                solarNode.GetComponentInChildren<TMP_Text>().text = (routeParts.IndexOf(routePart) + 1).ToString();
                solarNode.transform.position = routePart.solars[0].transform.position;
            }
            else
            {
                Destroy(solarNode.gameObject);
                oldOne.GetComponentInChildren<TMP_Text>().text += (" - " + (routeParts.IndexOf(routePart) + 1).ToString());
            }

            for (int i = 0; i < routePart.solars.Count - 1; i++)
            {
                LineRenderer line = Instantiate(lineRenderer, transform);
                line.startColor = RouteColor;
                line.endColor = RouteColor;
                line.SetPosition(0, routePart.solars[i].transform.position + Vector3.up);
                line.SetPosition(1, routePart.solars[i + 1].transform.position + Vector3.up);
                lineRenderers.Add(line);
            }

        }
    }

    public void ClearRoute()
    {
        routeParts = new List<RoutePart>();
        solarNodes = new List<RouteNodeClass>();
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

    private List<SolarSystem> FindPath(SolarSystemStruct startSolar, SolarSystemStruct endSolar)
    {
        if (startSolar == endSolar) return null;
        List<SolarSystem> routePart = PathFinderWithStruct.pathFindingWithDistance(endSolar, startSolar, SolarClusterStruct.SolarClusterStructList);
        return routePart;
    }

    public void TempSolar(SolarSystem solar)
    {
        RouteNodeClass tempCircle = Instantiate(stationCircle, transform);
        tempCircle.transform.position = solar.solarSystemStruct.solarLocation;
    }
}
