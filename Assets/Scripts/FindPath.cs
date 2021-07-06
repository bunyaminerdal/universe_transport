using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FindPath
{
    public void FindPathBeetwenToSolarSystem(SolarSystem startSolarsystem, SolarSystem endSolarSystem, List<SolarSystem[]> roads)
    {
        List<SolarSystem> currentSolarSystem = new List<SolarSystem>();
        List<List<SolarSystem[]>> tempRoadss = new List<List<SolarSystem[]>>();
        List<SolarSystem[]> selectedRoads = new List<SolarSystem[]>();
        List<SolarSystem> visitedSolars = new List<SolarSystem>();
        visitedSolars.Add(startSolarsystem);
        currentSolarSystem.Add(startSolarsystem);
        while (!currentSolarSystem.Contains(endSolarSystem))
        {
            List<SolarSystem[]> tempRoads = new List<SolarSystem[]>();
            List<SolarSystem> tempCurrentSolarSystem = new List<SolarSystem>();
            foreach (var road in roads)
            {
                foreach (var current in currentSolarSystem)
                {

                    SolarSystem[] road1 = new SolarSystem[2];
                    if (road[0] == current)
                    {
                        bool varmi = false;
                        foreach (var solar in visitedSolars)
                        {
                            if (road[1] == solar)
                            {
                                varmi = true;
                            }
                        }
                        if (!varmi)
                        {
                            tempCurrentSolarSystem.Add(road[1]);
                            // Debug.DrawLine(road[0].transform.position, road[1].transform.position, Color.green, 360.0f);
                            road1[0] = road[0];
                            road1[1] = road[1];
                            tempRoads.Add(road1);
                        }

                    }
                    if (road[1] == current)
                    {
                        bool varmi = false;
                        foreach (var solar in visitedSolars)
                        {
                            if (road[0] == solar)
                            {
                                varmi = true;
                            }
                        }
                        if (!varmi)
                        {
                            tempCurrentSolarSystem.Add(road[0]);
                            // Debug.DrawLine(road[1].transform.position, road[0].transform.position, Color.green, 360.0f);
                            road1[0] = road[1];
                            road1[1] = road[0];
                            tempRoads.Add(road1);
                        }
                    }


                }
            }
            tempRoadss.Add(tempRoads);
            currentSolarSystem.Clear();
            currentSolarSystem = tempCurrentSolarSystem;
            foreach (var solar in currentSolarSystem)
            {
                visitedSolars.Add(solar);
            }
        }
        foreach (var roadss in tempRoadss)
        {
            foreach (var roads1 in roadss)
            {
                if (roads1[0] == endSolarSystem || roads1[1] == endSolarSystem)
                {
                    selectedRoads = roadss;
                }
            }
        }
        //Debug.Log(selectedRoads.Count);
        // if (selectedRoads != null)
        foreach (var selectedroad in selectedRoads)
        {
            Debug.DrawLine(selectedroad[0].transform.position, selectedroad[1].transform.position, Color.green, 360.0f);
            Debug.Log(selectedroad);
        }
        Debug.Log("done");
    }
}
