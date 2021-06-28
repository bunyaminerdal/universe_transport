using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Planet : MonoBehaviour
{
    public string planetName;
    public PlanetType planetType;
    public int planetCapacity;
    public Moon[] moons;
    public SolarSystem ownerSolarSystem;
}

public enum PlanetType
{
    GasPlanet,
    OceanPlanet,
    DesertPlanet,
    RockPlanet,
    GreenPlanet

}