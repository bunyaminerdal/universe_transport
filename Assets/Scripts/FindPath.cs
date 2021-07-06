using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FindPath
{
    public List<SolarSystem[]> FindPathBeetwenToSolarSystem(SolarSystem startSolarsystem, SolarSystem endSolarSystem, List<SolarSystem[]> roads)
    {
        List<List<SolarSystem[]>> realRoadss = new List<List<SolarSystem[]>>();
        Dictionary<List<SolarSystem[]>, float> realRoadswithDistance = new Dictionary<List<SolarSystem[]>, float>();
        List<List<SolarSystem[]>> tempRoadss = new List<List<SolarSystem[]>>();
        Dictionary<List<SolarSystem[]>, float> tempRoadswithDistance = new Dictionary<List<SolarSystem[]>, float>();

        List<SolarSystem> visitedSolars = new List<SolarSystem>();
        visitedSolars.Add(startSolarsystem);

        //detected first roads
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
                    realRoadswithDistance.Add(tempRoads, Vector3.Distance(road[0].transform.position, road[1].transform.position));
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
                    realRoadswithDistance.Add(tempRoads, Vector3.Distance(road[1].transform.position, road[0].transform.position));
                    visitedSolars.Add(road[0]);
                }
            }

        }

        while (!visitedSolars.Contains(endSolarSystem))
        {
            visitedSolars.Clear();
            foreach (var temproads in tempRoadss)
            {
                visitedSolars.Add(temproads[temproads.Count - 1][1]);
            }
            tempRoadss.Clear();
            foreach (var realroads in realRoadss)
            {
                tempRoadss.Add(realroads);
            }
            foreach (var realroadswDistance in realRoadswithDistance)
            {
                tempRoadswithDistance.Add(realroadswDistance.Key, realroadswDistance.Value);
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
                            float distance1 = Vector3.Distance(road[0].transform.position, road[1].transform.position);
                            foreach (var temproad1 in temproads)
                            {
                                newtemproads.Add(temproad1);
                                distance1 += Vector3.Distance(temproad1[0].transform.position, temproad1[1].transform.position);
                            }
                            newtemproads.Add(road1);
                            realRoadss.Add(newtemproads);
                            realRoadswithDistance.Add(newtemproads, distance1);
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
                            float distance1 = Vector3.Distance(road[1].transform.position, road[0].transform.position);
                            foreach (var temproad1 in temproads)
                            {
                                newtemproads.Add(temproad1);
                                distance1 += Vector3.Distance(temproad1[0].transform.position, temproad1[1].transform.position);
                            }
                            newtemproads.Add(road1);
                            realRoadss.Add(newtemproads);
                            realRoadswithDistance.Add(newtemproads, distance1);
                        }

                    }
                    realRoadss.Remove(temproads);
                    realRoadswithDistance.Remove(temproads);
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
