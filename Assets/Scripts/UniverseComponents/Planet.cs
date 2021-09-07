using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Planet : MonoBehaviour
{
    public string planetName;
    public PlanetType planetType;
    public Moon[] moons;
    public SolarSystem ownerSolarSystem;
    public Item Item;
}

public enum PlanetType
{
    GasPlanet,
    OrganicPlanet,
    MetalPlanet,
    MineralPlanet
}