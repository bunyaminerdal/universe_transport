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
    public static int solarClusterCircleCount = 5;

    //raw material probability and how many raw material can be exist in solarsystem
    public static float rawMaterialProbability = 0.04f;
    public static int maxResourceCount = 2;
    public static bool isSameRawMaterialExistInSolarsystem = false;

    //planet randomization in solarsystem
    public static int minPlanetCount = 3;
    public static int maxPlanetCount = 5;

    //solarsystem ransomization in solarcluster
    public static int minSolarSystemCount = 2; // 2den az olursa sıkıntı çıkıyor
    public static int maxSolarSystemCount = 6; //8 den fazla olursa 3. sıra olması lazım
    //Intermediate product stations randomization
    public static float intermediateProductStationProbability = 0.01f;
    public static int numOfIntermediateProductStationInCluster = 2;
    public static int numOfIntermediateProductStationInSolarSystem = 1;
}
