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

    //raw material probability and how many raw material can be exist in solarsystem
    public static float rawMaterialProbability = 0.03f;
    public static int maxResourceCount = 2;
    public static bool isSameRawMaterialExistInSolarsystem = false;

    //planet randomization in solarsystem
    public static int minPlanetCount = 3;
    public static int maxPlanetCount = 8;

    //solarsystem ransomization in solarcluster
    public static int minSolarSystemCount = 2;
    public static int maxSolarSystemCount = 5;
}
