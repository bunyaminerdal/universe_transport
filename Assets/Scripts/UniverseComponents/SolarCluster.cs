using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SolarCluster : MonoBehaviour
{
    public static List<SolarCluster> SolarClusterList = new List<SolarCluster>();
    public SolarClusterStruct solarClusterStruct;
    public Vector3 clusterLocation;
    public List<SolarSystem> solarSystems;

    private void Awake()
    {
        SolarClusterList.Add(this);
    }
}
