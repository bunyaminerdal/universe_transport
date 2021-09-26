using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public static class PathFinder
{
    public static void pathFindingWithDistance(SolarSystem targetSolar, SolarSystem startSolar)
    {
        CalculateDistances(targetSolar);

        SolarSystem startsolar = startSolar;
        while (startsolar != targetSolar)
        {
            var tempstartsolar = startsolar.connectedSolars.Find(solar => solar.solarDistance == startsolar.connectedSolars.Min(solar => solar.solarDistance));
            Debug.DrawLine(startsolar.transform.position, tempstartsolar.transform.position, Color.red, 360.0f);
            startsolar = tempstartsolar;
        }

    }
    private static void CalculateDistances(SolarSystem _targetSolar)
    {
        var visitedSolars = new List<SolarSystem>();
        var solarToVisitQueue = new Queue<SolarSystem>();
        solarToVisitQueue.Enqueue(_targetSolar);

        while (solarToVisitQueue.Count > 0)
        {
            var currentSolar = solarToVisitQueue.Dequeue();
            //calculate the solar distances
            if (currentSolar == _targetSolar)
            {
                currentSolar.solarDistance = 0;
            }

            //find available next solar
            var nextSolars = currentSolar.connectedSolars;
            var filteredSolars = nextSolars.Where(solar => !visitedSolars.Contains(solar)).ToList();


            //enqueue them
            foreach (var solar in filteredSolars)
            {
                solarToVisitQueue.Enqueue(solar);
                var distance = CalculateSolarDistance(currentSolar, solar);
                var newDistance = currentSolar.solarDistance + distance;
                solar.solarDistance = Mathf.Min(solar.solarDistance, newDistance);
            }

            //add to queue
            visitedSolars.Add(currentSolar);

        }

    }
    private static float CalculateSolarDistance(SolarSystem currentSolar, SolarSystem solar)
    {
        return (currentSolar.transform.position - solar.transform.position).magnitude;
    }


}
