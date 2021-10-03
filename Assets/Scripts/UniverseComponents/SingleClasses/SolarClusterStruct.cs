using UnityEngine;

public class SolarClusterStruct
{
    public Vector3 clusterLocation;
    public SolarSystemStruct[] solarSystemsStruct;

    public SolarClusterStruct(Vector3 _clusterLocation, SolarSystemStruct[] _solarSystemStruct)
    {
        this.clusterLocation = _clusterLocation;
        this.solarSystemsStruct = _solarSystemStruct;
    }
}
