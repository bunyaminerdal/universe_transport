using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FindPath
{
    public List<SolarSystem[]> FindPathBeetwenToSolarSystem(SolarSystem startSolarsystem, SolarSystem endSolarSystem, List<SolarSystem[]> roads)
    {
        List<List<SolarSystem[]>> realRoadss = new List<List<SolarSystem[]>>();
        List<List<SolarSystem[]>> tempRoadss = new List<List<SolarSystem[]>>();

        List<SolarSystem> visitedSolars = new List<SolarSystem>();
        visitedSolars.Add(startSolarsystem);

        //detected first roads
        //List<SolarSystem> tempCurrentSolarSystem = new List<SolarSystem>();
        foreach (var road in roads)
        {
            SolarSystem[] road1 = new SolarSystem[2];
            if (road[0] == startSolarsystem)
            {
                if (!visitedSolars.Contains(road[1]))
                {
                    List<SolarSystem[]> tempRoads = new List<SolarSystem[]>();
                    // Debug.DrawLine(road[0].transform.position, road[1].transform.position, Color.green, 360.0f);
                    road1[0] = road[0];
                    road1[1] = road[1];
                    tempRoads.Add(road1);
                    realRoadss.Add(tempRoads);
                    visitedSolars.Add(road[1]);
                }

            }
            else if (road[1] == startSolarsystem)
            {
                if (!visitedSolars.Contains(road[0]))
                {
                    List<SolarSystem[]> tempRoads = new List<SolarSystem[]>();

                    // Debug.DrawLine(road[1].transform.position, road[0].transform.position, Color.green, 360.0f);
                    road1[0] = road[1];
                    road1[1] = road[0];
                    tempRoads.Add(road1);
                    realRoadss.Add(tempRoads);
                    tempRoads.Clear();
                    visitedSolars.Add(road[0]);
                }
            }

        }

        while (!visitedSolars.Contains(endSolarSystem))
        {
            tempRoadss.Clear();
            foreach (var realroads in realRoadss)
            {
                tempRoadss.Add(realroads);
            }

            foreach (var temproads in tempRoadss)
            {
                foreach (var road in roads)
                {
                    if (temproads[temproads.Count - 1][1] == road[0])
                    {
                        if (!visitedSolars.Contains(road[1]))
                        {
                            //Debug.DrawLine(road[0].transform.position, road[1].transform.position, Color.green, 360.0f);
                            visitedSolars.Add(road[1]);
                            SolarSystem[] road1 = new SolarSystem[2];
                            road1[0] = road[0];
                            road1[1] = road[1];
                            List<SolarSystem[]> newtemproads = new List<SolarSystem[]>();
                            foreach (var temproad1 in temproads)
                            {
                                newtemproads.Add(temproad1);
                            }
                            newtemproads.Add(road1);
                            realRoadss.Add(newtemproads);
                        }

                    }
                    else if (temproads[temproads.Count - 1][1] == road[1])
                    {
                        if (!visitedSolars.Contains(road[0]))
                        {
                            //Debug.DrawLine(road[1].transform.position, road[0].transform.position, Color.green, 360.0f);
                            visitedSolars.Add(road[0]);
                            SolarSystem[] road1 = new SolarSystem[2];
                            road1[0] = road[1];
                            road1[1] = road[0];
                            List<SolarSystem[]> newtemproads = new List<SolarSystem[]>();
                            foreach (var temproad1 in temproads)
                            {
                                newtemproads.Add(temproad1);
                            }
                            newtemproads.Add(road1);
                            realRoadss.Add(newtemproads);
                        }

                    }
                    realRoadss.Remove(temproads);
                }
            }
        }
        foreach (var realroads in realRoadss)
        {
            foreach (var realroad in realroads)
            {
                if (realroad[1] == endSolarSystem)
                {
                    return realroads;
                }
            }
        }

        return null;

    }


}
