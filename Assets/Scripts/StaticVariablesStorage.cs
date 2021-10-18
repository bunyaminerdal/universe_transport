using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class StaticVariablesStorage
{
    //universe randomization parameters
    public static float solarSystemDistance = 1600;
    public static float solarClusterDistance = 5300;
    public static int randomizationRange = 450;
    public static int solarSystemCircleCount = 2;
    public static int solarClusterCircleCount = 4;

    //raw material probability and how many raw material can be exist in a solarsystem
    public static float rawMaterialProbability = 0.04f;
    public static int maxResourceCount = 2;
    public static bool isSameRawMaterialExistInSolarsystem = false;

    //planet randomization in solarsystem
    public static int minPlanetCount = 3;
    public static int maxPlanetCount = 6; // don't use more than 6 planets

    //solarsystem randomization in solarcluster
    public static int minSolarSystemCount = 2; // don't use less than 2 solar
    public static int maxSolarSystemCount = 5; //if you want to have more than 7 system in a cluster, need third circle
    //Intermediate product stations randomization
    public static float intermediateProductStationProbability = 0.01f;
    public static int numOfIntermediateProductStationInCluster = 1;
    public static int numOfIntermediateProductStationInSolarSystem = 1;
    //Final product stations randomization
    public static float finalProductStationProbability = 0.01f;
    public static int numOfFinalProductStationInCluster = 1;
    public static int numOfFinalProductStationInSolar = 1;
    //City randomization
    public static float cityProbability = 0.02f;
    public static int numOfCityInCluster = 1;
    public static int numOfCityInSolar = 1;

}
