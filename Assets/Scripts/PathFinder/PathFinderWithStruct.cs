using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Unity.Burst;
using Unity.Jobs;
using Unity.Mathematics;
using Unity.Collections;

public static class PathFinderWithStruct
{


    [BurstCompile]
    public static void pathFindingWithDistance(SolarSystemStruct _targetSolar, SolarSystemStruct _startSolar)
    {
        var startTime = Time.realtimeSinceStartup;
        CalculateDistances(_targetSolar);
        Debug.Log("distance calc: " + ((Time.realtimeSinceStartup - startTime) * 1000f));

        SolarSystemStruct startsolar = _startSolar;
        while (startsolar.solarLocation != _targetSolar.solarLocation)
        {
            var tempstartsolar = startsolar.connectedSolars.Find(solar => solar.solarDistance == startsolar.connectedSolars.Min(solar => solar.solarDistance));
            Debug.DrawLine(startsolar.solarLocation, tempstartsolar.solarLocation, Color.red, 360.0f);
            startsolar = tempstartsolar;
        }
    }
    [BurstCompile]
    private static void CalculateDistances(SolarSystemStruct _targetSolar)
    {
        var visitedSolars = new List<SolarSystemStruct>();
        var solarToVisitQueue = new Queue<SolarSystemStruct>();
        solarToVisitQueue.Enqueue(_targetSolar);
        while (solarToVisitQueue.Count > 0)
        {
            var currentSolar = solarToVisitQueue.Dequeue();
            //calculate the solar distances
            if (currentSolar.solarLocation == _targetSolar.solarLocation)
            {
                currentSolar.solarDistanceChange(0);
            }

            //find available next solar
            var nextSolars = currentSolar.connectedSolars;
            var filteredSolars = nextSolars.Where(solar => !visitedSolars.Contains(solar)).ToList();
            foreach (var solar in filteredSolars)
            {
                solar.solarDistanceChange(float.MaxValue);
            }
            //enqueue them
            foreach (var solar in filteredSolars)
            {
                solarToVisitQueue.Enqueue(solar);

                var distance = (currentSolar.solarLocation - solar.solarLocation).magnitude;

                solar.solarDistanceChange(math.min(solar.solarDistance, currentSolar.solarDistance + distance));

            }

            //add to queue
            visitedSolars.Add(currentSolar);
        }
    }
    private static float CalculateSolarDistance(SolarSystemStruct currentSolar, SolarSystemStruct solar)
    {
        return (currentSolar.solarLocation - solar.solarLocation).magnitude;
    }


}
