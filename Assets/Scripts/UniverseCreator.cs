using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UniverseCreator : MonoBehaviour
{
    [Header("variables")]
    [SerializeField]
    private float solarSystemDistance = 55;
    [SerializeField]
    private float solarClusterDistance = 300;
    [SerializeField]
    private int solarSystemCircleCount = 2;
    [SerializeField]
    private int solarClusterCircleCount = 6;

    [Header("prefabs")]
    [SerializeField]
    private GameObject sunPrefab;
    [SerializeField]
    private GameObject solarSystemPrefab;
    [SerializeField]
    private SolarCluster solarClusterPrefab;

    //local veriables
    private List<SolarCluster> solarClusters = new List<SolarCluster>();
    private List<SolarSystem[]> roads = new List<SolarSystem[]>();
    private int randomizationRange = 20;

    private void OnEnable()
    {
        SolarClusterLocationCreator(Vector3.zero);
        RoadCreator();
    }

    void Start()
    {

    }
    // Update is called once per frame
    void Update()
    {

    }

    private void RoadCreator()
    {
        for (int i = 0; i < solarClusters.Count; i++)
        {
            //solarClusters[i].solarSystems[0].GetComponent<PathSpawner>().CreatePaths();
            for (int j = 1; j < solarClusters[i].solarSystems.Count; j++)
            {
                //solarClusters[i].solarSystems[j].GetComponent<PathSpawner>().CreatePaths();
                float distance = Vector3.Distance(solarClusters[i].solarSystems[0].transform.position, solarClusters[i].solarSystems[j].transform.position);
                if (Mathf.Abs(distance) < solarSystemDistance + randomizationRange)
                {
                    SolarSystem[] road = new SolarSystem[] { solarClusters[i].solarSystems[0], solarClusters[i].solarSystems[j] };
                    roads.Add(road);
                    Debug.DrawLine(solarClusters[i].solarSystems[0].transform.position, solarClusters[i].solarSystems[j].transform.position, Color.gray, 100f);
                }
                if (j != 1)
                {
                    SolarSystem[] road = new SolarSystem[] { solarClusters[i].solarSystems[j - 1], solarClusters[i].solarSystems[j] };
                    roads.Add(road);
                    Debug.DrawLine(solarClusters[i].solarSystems[j - 1].transform.position, solarClusters[i].solarSystems[j].transform.position, Color.gray, 100f);
                }

            }
            for (int t = 0; t < solarClusters.Count; t++)
            {
                if (solarClusters[i] != solarClusters[t])
                {
                    float clusterDistance = Vector3.Distance(solarClusters[i].clusterLocation, solarClusters[t].clusterLocation);
                    if (clusterDistance < solarClusterDistance + randomizationRange)
                    {
                        //clsterlar arasında en yakın olan solar systemleri seciyoruz.
                        SolarSystem[] tempRoad = new SolarSystem[2];
                        float distanceClusterCon = solarClusterDistance;
                        for (int y = 0; y < solarClusters[i].solarSystems.Count; y++)
                        {

                            for (int x = 0; x < solarClusters[t].solarSystems.Count; x++)
                            {
                                float distanceClusterConnection = Vector3.Distance(solarClusters[i].solarSystems[y].transform.position, solarClusters[t].solarSystems[x].transform.position);

                                if (distanceClusterConnection < distanceClusterCon)
                                {
                                    distanceClusterCon = distanceClusterConnection;
                                    tempRoad[0] = solarClusters[i].solarSystems[y];
                                    tempRoad[1] = solarClusters[t].solarSystems[x];
                                }
                            }
                        }
                        roads.Add(tempRoad);
                        Debug.DrawLine(tempRoad[0].transform.position, tempRoad[1].transform.position, Color.gray, 100f);
                    }
                }

            }

        }
    }
    private void SolarClusterLocationCreator(Vector3 destination)
    {
        float[] distances = new float[solarClusterCircleCount];
        int[] circleCounts = new int[solarClusterCircleCount];
        int solarClusterCount = 0;
        for (int i = 0; i < solarClusterCircleCount; i++)
        {
            circleCounts[i] = Mathf.RoundToInt((2 * Mathf.PI * solarClusterDistance * (i + 1) / solarClusterDistance)) + 1;
            solarClusterCount += circleCounts[i];
            distances[i] = (solarClusterDistance * i) + solarClusterDistance;
        }
        List<Vector3> targetPositionList = GetPositionList(destination, distances, circleCounts);
        List<Vector3> ClusterPositionList = new List<Vector3>();
        for (int i = 0; i < solarClusterCount; i++)
        {
            ClusterPositionList.Add(targetPositionList[i]);
        }
        for (int i = 0; i < solarClusterCount; i++)
        {
            SolarCluster cluster = Instantiate(solarClusterPrefab, transform);
            solarClusters.Add(cluster);
        }

        for (int i = 0; i < solarClusterCount; i++)
        {
            solarClusters[i].clusterLocation = ClusterPositionList[i];
            //add random position to solar systems
            int randomX = Random.Range(-randomizationRange, randomizationRange);
            int randomZ = Random.Range(-randomizationRange, randomizationRange);
            Vector3 randomPos = new Vector3(randomX, 0, randomZ);

            solarClusters[i].clusterLocation += randomPos;
            int solarSystemCountInCluster = Random.Range(4, 8);
            solarClusters[i].solarSystems = SolarSystemLocationCreator(solarClusters[i].clusterLocation, solarSystemCountInCluster, solarClusters[i].gameObject.transform);
            foreach (var solar in solarClusters[i].solarSystems)
            {
                CreateStar(solar);
            }
        }

    }
    private List<SolarSystem> SolarSystemLocationCreator(Vector3 destination, int localSystemCount, Transform solarCluster)
    {
        float[] distances = new float[solarSystemCircleCount];
        int[] circleCounts = new int[solarSystemCircleCount];
        for (int i = 0; i < solarSystemCircleCount; i++)
        {
            circleCounts[i] = Mathf.RoundToInt((2 * Mathf.PI * solarSystemDistance * (i + 1) / solarSystemDistance)) + 1;
            distances[i] = (solarSystemDistance * i) + solarSystemDistance;
        }
        List<Vector3> targetPositionList = GetPositionList(destination, distances, circleCounts);
        List<Vector3> localArrangedTargetPositionList = new List<Vector3>();
        localArrangedTargetPositionList.Add(destination);
        for (int i = 0; i < localSystemCount; i++)
        {
            localArrangedTargetPositionList.Add(targetPositionList[i]);
        }
        var targetPositionListIndex = 0;
        List<SolarSystem> localSolarSystems = new List<SolarSystem>();
        for (int i = 0; i < localArrangedTargetPositionList.Count; i++)
        {
            var solarSystem = Instantiate(solarSystemPrefab, solarCluster);
            localSolarSystems.Add(solarSystem.GetComponent<SolarSystem>());
            solarSystem.transform.position = localArrangedTargetPositionList[targetPositionListIndex];
            //add random position to solar systems
            int randomX = Random.Range(-randomizationRange, randomizationRange);
            int randomZ = Random.Range(-randomizationRange, randomizationRange);
            Vector3 randomPos = new Vector3(randomX, 0, randomZ);
            solarSystem.transform.position += randomPos;

            targetPositionListIndex = (targetPositionListIndex + 1) % localArrangedTargetPositionList.Count;
        }

        return localSolarSystems;
    }
    private List<Vector3> GetPositionList(Vector3 startPosition, float[] ringDistanceArray, int[] ringPositionCountArray)
    {
        List<Vector3> positionList = new List<Vector3>();
        //positionList.Add(startPosition);
        for (int i = 0; i < ringDistanceArray.Length; i++)
        {

            positionList.AddRange(GetPositionListCircle(startPosition, ringDistanceArray[i], ringPositionCountArray[i]));
        }
        return positionList;
    }
    private List<Vector3> GetPositionListCircle(Vector3 startPosition, float distance, int positionCount)
    {
        List<Vector3> positionList = new List<Vector3>();

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
    public void CreateStar(SolarSystem parent)
    {
        var star = Instantiate(sunPrefab, parent.transform);
        parent.star = star.GetComponent<Star>();
    }
}
