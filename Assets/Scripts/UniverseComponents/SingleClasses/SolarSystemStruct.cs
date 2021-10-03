using System.Collections.Generic;
using UnityEngine;

public class SolarSystemStruct
{

    public float solarDistance { get; protected set; }
    public Vector3 solarLocation;
    public List<SolarSystemStruct> connectedSolars;

    public SolarSystemStruct(Vector3 _solarLocation)
    {
        this.solarLocation = _solarLocation;
        this.connectedSolars = new List<SolarSystemStruct>();
    }

    public SolarSystem solarSystem { get; protected set; }

    public void setSolarSystem(SolarSystem solar)
    {
        solarSystem = solar;
    }
    public void solarDistanceChange(float distance)
    {
        solarDistance = distance;
    }
}
