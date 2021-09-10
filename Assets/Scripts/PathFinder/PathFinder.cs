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
            // var tempstartsolar = startsolar.connectedSolars.First();
            var tempstartsolar = startsolar.connectedSolars.Find(solar => solar.solarDistance == startsolar.connectedSolars.Min(solar => solar.solarDistance));

            //Debug.DrawLine(startsolar.transform.position, tempstartsolar.transform.position, Color.red, 360.0f);
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
            var filterdSolars = nextSolars.Where(solar => !visitedSolars.Contains(solar)).ToList();


            //enqueue them
            foreach (var solar in filterdSolars)
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

    public static void oldPathFinder(SolarSystem startSolar, SolarSystem targetSolar, List<SolarSystem[]> roads)
    {
        FindPath path = new FindPath();
        List<SolarSystem[]> selectedRoads = new List<SolarSystem[]>();
        selectedRoads = path.FindPathBeetwenToSolarSystem(startSolar, targetSolar, roads);
        if (selectedRoads == null)
            return;
        foreach (var selectedroad in selectedRoads)
        {
            Debug.DrawLine(selectedroad[0].transform.position, selectedroad[1].transform.position, Color.green, 360.0f);
        }
    }
}
