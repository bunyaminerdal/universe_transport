using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Collections;

public class NewUniverseCreator : MonoBehaviour
{
    private SolarClusterStruct[] solarClusters;
    private List<SolarSystemStruct[]> roads = new List<SolarSystemStruct[]>();

    private void OnEnable()
    {
        PlayerManagerEventHandler.InteractionEvent.AddListener(() => StartCoroutine(CreateUniverse()));
        PlayerManagerEventHandler.Interaction2Event.AddListener(FindPath);
    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    private void OnDisable()
    {
        PlayerManagerEventHandler.InteractionEvent.RemoveListener(() => StartCoroutine(CreateUniverse()));
        PlayerManagerEventHandler.Interaction2Event.RemoveListener(FindPath);
    }

    private IEnumerator CreateUniverse()
    {
        UIEventHandler.CreatingUniverse?.Invoke(true);
        yield return new WaitForSeconds(0.1f);
        var startTime = Time.realtimeSinceStartup;
        SolarClusterCreator(Vector3.zero);
        RoadCreator();
        CheckConnection();
        UIEventHandler.CreatingUniverse?.Invoke(false);
        Debug.Log("Universe create time: " + ((Time.realtimeSinceStartup - startTime) * 1000f));
        yield return null;
    }

    private void FindPath()
    {
        PathFinderWithStruct.pathFindingWithDistance(solarClusters[35].solarSystems[0], solarClusters[75].solarSystems[0]);
    }
    private void CheckConnection()
    {
        PathFinderWithStruct.pathFindingWithDistance(solarClusters[0].solarSystems[0], solarClusters[0].solarSystems[1]);
        List<SolarSystemStruct> solarsystemsWithoutConnection = new List<SolarSystemStruct>();
        foreach (var cluster in solarClusters)
        {
            foreach (var system in cluster.solarSystems)
            {
                if (solarClusters[0].solarSystems[0].solarLocation != system.solarLocation)
                {
                    if (system.solarDistance == float.MaxValue)
                    {
                        solarsystemsWithoutConnection.Add(system);
                    }
                }
            }
        }
        if (solarsystemsWithoutConnection.Count > 0)
        {
            Debug.Log("Recalculate begun!");
            StartCoroutine(CreateUniverse());
        }
        else
        {
            Debug.Log("All systems are connected!");
        }
    }
    private void RoadCreator()
    {
        for (int i = 0; i < solarClusters.Length; i++)
        {
            for (int j = 1; j < solarClusters[i].solarSystems.Length; j++)
            {
                float distance = Vector3.Distance(solarClusters[i].solarSystems[0].solarLocation, solarClusters[i].solarSystems[j].solarLocation);
                if (Mathf.Abs(distance) < StaticVariablesStorage.solarSystemDistance + StaticVariablesStorage.randomizationRange)
                {
                    SolarSystemStruct[] road = new SolarSystemStruct[] { solarClusters[i].solarSystems[0], solarClusters[i].solarSystems[j] };
                    solarClusters[i].solarSystems[0].connectedSolars.Add(solarClusters[i].solarSystems[j]);
                    solarClusters[i].solarSystems[j].connectedSolars.Add(solarClusters[i].solarSystems[0]);
                    roads.Add(road);
                }
                if (j != 1)
                {
                    SolarSystemStruct[] road = new SolarSystemStruct[] { solarClusters[i].solarSystems[j - 1], solarClusters[i].solarSystems[j] };
                    solarClusters[i].solarSystems[j - 1].connectedSolars.Add(solarClusters[i].solarSystems[j]);
                    solarClusters[i].solarSystems[j].connectedSolars.Add(solarClusters[i].solarSystems[j - 1]);
                    roads.Add(road);
                }

            }
            for (int t = 0; t < solarClusters.Length; t++)
            {
                if (solarClusters[i].clusterLocation != solarClusters[t].clusterLocation)
                {
                    float clusterDistance = Vector3.Distance(solarClusters[i].clusterLocation, solarClusters[t].clusterLocation);
                    if (clusterDistance < StaticVariablesStorage.solarClusterDistance + StaticVariablesStorage.randomizationRange)
                    {
                        //clusterlar arasında en yakın olan solar systemleri seciyoruz.
                        SolarSystemStruct[] tempRoad = new SolarSystemStruct[2];
                        float distanceClusterCon = StaticVariablesStorage.solarClusterDistance;
                        for (int y = 0; y < solarClusters[i].solarSystems.Length; y++)
                        {

                            for (int x = 0; x < solarClusters[t].solarSystems.Length; x++)
                            {
                                float distanceClusterConnection = Vector3.Distance(solarClusters[i].solarSystems[y].solarLocation, solarClusters[t].solarSystems[x].solarLocation);

                                if (distanceClusterConnection < distanceClusterCon)
                                {
                                    distanceClusterCon = distanceClusterConnection;
                                    tempRoad[0] = solarClusters[i].solarSystems[y];
                                    tempRoad[1] = solarClusters[t].solarSystems[x];
                                }
                            }
                        }
                        tempRoad[0].connectedSolars.Add(tempRoad[1]);
                        tempRoad[1].connectedSolars.Add(tempRoad[0]);
                        roads.Add(tempRoad);
                        //Debug.DrawLine(tempRoad[0].transform.position, tempRoad[1].transform.position, Color.gray, 100f);
                    }
                }

            }

        }


        for (int i = 0; i < roads.Count; i++)
        {
            for (int j = 0; j < roads.Count; j++)
            {
                if (roads[i] != roads[j])
                {
                    if (roads[i][0].solarLocation == roads[j][1].solarLocation && roads[i][1].solarLocation == roads[j][0].solarLocation)
                    {
                        roads[j][0].connectedSolars.Remove(roads[j][1]);
                        roads[j][1].connectedSolars.Remove(roads[j][0]);
                        roads.Remove(roads[j]);
                        i--;
                        j--;
                    }
                }
            }
            // TODO:prefabları başka zaman oluşturcaz
            // LineRenderer roadPrefab = Instantiate(roadRendererPrefab, roads[i][0].transform.position, roads[i][0].transform.rotation, transform);
            // roadPrefab.SetPosition(0, roads[i][0].transform.position);
            // roadPrefab.SetPosition(1, roads[i][1].transform.position);
        }


    }
    private void SolarClusterCreator(Vector3 destination)
    {
        float[] distances = new float[StaticVariablesStorage.solarClusterCircleCount];
        int[] circleCounts = new int[StaticVariablesStorage.solarClusterCircleCount];
        int solarClusterCount = 0;
        for (int i = 0; i < StaticVariablesStorage.solarClusterCircleCount; i++)
        {
            circleCounts[i] = Mathf.RoundToInt((2 * Mathf.PI * StaticVariablesStorage.solarClusterDistance * (i + 1) / StaticVariablesStorage.solarClusterDistance)) + 1;
            solarClusterCount += circleCounts[i];
            distances[i] = (StaticVariablesStorage.solarClusterDistance * i) + StaticVariablesStorage.solarClusterDistance;
        }
        NativeList<Vector3> targetPositionList = GetPositionList(destination, distances, circleCounts);
        NativeList<Vector3> ClusterPositionList = new NativeList<Vector3>(Allocator.Temp);
        for (int i = 0; i < solarClusterCount; i++)
        {
            ClusterPositionList.Add(targetPositionList[i]);
        }
        solarClusters = new SolarClusterStruct[solarClusterCount];
        for (int i = 0; i < solarClusterCount; i++)
        {
            int randomX = Random.Range(-StaticVariablesStorage.randomizationRange, StaticVariablesStorage.randomizationRange);
            int randomZ = Random.Range(-StaticVariablesStorage.randomizationRange, StaticVariablesStorage.randomizationRange);
            Vector3 randomPos = new Vector3(randomX, 0, randomZ);
            int solarSystemCountInCluster = Random.Range(StaticVariablesStorage.minSolarSystemCount, StaticVariablesStorage.maxSolarSystemCount);

            var solarClusterStruct = new SolarClusterStruct
            {
                clusterLocation = ClusterPositionList[i] + randomPos,
                solarSystems = SolarSystemLocationCreator(ClusterPositionList[i] + randomPos, solarSystemCountInCluster),
            };
            solarClusters[i] = solarClusterStruct;
        }
    }
    private SolarSystemStruct[] SolarSystemLocationCreator(Vector3 destination, int localSystemCount)
    {
        float[] distances = new float[StaticVariablesStorage.solarSystemCircleCount];
        int[] circleCounts = new int[StaticVariablesStorage.solarSystemCircleCount];
        for (int i = 0; i < StaticVariablesStorage.solarSystemCircleCount; i++)
        {
            circleCounts[i] = Mathf.RoundToInt((2 * Mathf.PI * StaticVariablesStorage.solarSystemDistance * (i + 1) / StaticVariablesStorage.solarSystemDistance)) + 1;
            distances[i] = (StaticVariablesStorage.solarSystemDistance * i) + StaticVariablesStorage.solarSystemDistance;
        }
        NativeList<Vector3> targetPositionList = GetPositionList(destination, distances, circleCounts);
        NativeList<Vector3> localArrangedTargetPositionList = new NativeList<Vector3>(Allocator.Temp);
        localArrangedTargetPositionList.Add(destination);
        for (int i = 0; i < localSystemCount; i++)
        {
            localArrangedTargetPositionList.Add(targetPositionList[i]);
        }
        var targetPositionListIndex = 0;
        SolarSystemStruct[] alibaba = new SolarSystemStruct[localArrangedTargetPositionList.Length];
        for (int i = 0; i < localArrangedTargetPositionList.Length; i++)
        {
            int randomX = Random.Range(-StaticVariablesStorage.randomizationRange, StaticVariablesStorage.randomizationRange);
            int randomZ = Random.Range(-StaticVariablesStorage.randomizationRange, StaticVariablesStorage.randomizationRange);
            Vector3 randomPos = new Vector3(randomX, 0, randomZ);
            var solarSystemStruct = new SolarSystemStruct
            {
                solarLocation = localArrangedTargetPositionList[targetPositionListIndex] + randomPos,
                connectedSolars = new List<SolarSystemStruct>(),
            };
            solarSystemStruct.solarDistanceChange(float.MaxValue);
            alibaba[i] = solarSystemStruct;
            targetPositionListIndex = (targetPositionListIndex + 1) % localArrangedTargetPositionList.Length;
        }
        return alibaba;
    }
    private NativeList<Vector3> GetPositionList(Vector3 startPosition, float[] ringDistanceArray, int[] ringPositionCountArray)
    {
        NativeList<Vector3> positionList = new NativeList<Vector3>(Allocator.Temp);
        //positionList.Add(startPosition);
        for (int i = 0; i < ringDistanceArray.Length; i++)
        {
            positionList.AddRange(GetPositionListCircle(startPosition, ringDistanceArray[i], ringPositionCountArray[i]));
        }
        return positionList;
    }
    private NativeList<Vector3> GetPositionListCircle(Vector3 startPosition, float distance, int positionCount)
    {
        NativeList<Vector3> positionList = new NativeList<Vector3>(Allocator.Temp);

        for (int i = 0; i < positionCount; i++)
        {
            float angle = i * (360.0f / positionCount);
            Vector3 dir = ApplyRotationToVector(new Vector3(1, 0), angle);
            Vector3 position1 = startPosition + dir * distance;
            positionList.Add(position1);
        }
        return positionList;
    }
    private Vector3 ApplyRotationToVector(Vector3 vec, float angle)
    {
        return Quaternion.Euler(0, angle, 0) * vec;
    }
}
public class SolarClusterStruct
{
    public Vector3 clusterLocation;
    public SolarSystemStruct[] solarSystems;
}
public class SolarSystemStruct
{
    public float solarDistance { get; protected set; }
    public Vector3 solarLocation;
    public List<SolarSystemStruct> connectedSolars;

    public void solarDistanceChange(float distance)
    {
        solarDistance = distance;
    }
}
