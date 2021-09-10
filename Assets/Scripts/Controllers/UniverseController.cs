using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class UniverseController : MonoBehaviour
{

    [Header("prefabs")]
    [SerializeField]
    private GameObject sunPrefab;
    [SerializeField]
    private MaterialList starMatList;
    [SerializeField]
    private Planet PlanetPrefab;
    [SerializeField]
    private MaterialList planetMatList;

    [SerializeField]
    private GameObject solarSystemPrefab;
    [SerializeField]
    private SolarCluster solarClusterPrefab;
    [SerializeField]
    private LineRenderer roadRendererPrefab;

    //local variables
    private List<SolarCluster> solarClusters = new List<SolarCluster>();
    private List<SolarSystem[]> roads = new List<SolarSystem[]>();
    private List<Material> tempMaterials;
    private List<Material> tempPlanetMatList;
    private List<Planet> planetList = new List<Planet>();
    private int totalPlanetCount;

    private void OnEnable()
    {

        SolarClusterCreator(Vector3.zero);
        CreateStarMatList();
        CreateStars();
        RoadCreator();
        CreatePortsInSolar();
        CreatePlanetMatList();
        CalculateRawMaterialsCount();
        CreatePlanets();
        //oldPathFinder();

    }
    void Start()
    {
        //bu evente gerek yok gibi
        PlayerManagerEventHandler.BoundaryCreateEvent?.Invoke((StaticVariablesStorage.solarClusterDistance * StaticVariablesStorage.solarClusterCircleCount) + StaticVariablesStorage.solarSystemDistance, StaticVariablesStorage.solarSystemDistance / 30f);

        //PathFinder.pathFindingWithDistance(solarClusters[12].solarSystems[1], solarClusters[0].solarSystems[2]);
    }
    private void CalculateRawMaterialsCount()
    {
        int numberoforganic = (int)(totalPlanetCount * StaticVariablesStorage.rawMaterialProbability);
        int numberofmetal = (int)(totalPlanetCount * StaticVariablesStorage.rawMaterialProbability);
        int numberofmineral = (int)(totalPlanetCount * StaticVariablesStorage.rawMaterialProbability);
        int numberofgas = (int)(totalPlanetCount * StaticVariablesStorage.rawMaterialProbability);
        tempPlanetMatList.Shuffle();
        //kaç tane hangi cinsten gezegen olacağını belirleyip static bir liisteye ekleyip ordan kullandıralım
        for (int i = 0; i < totalPlanetCount; i++)
        {
            Planet newplanet = Instantiate(PlanetPrefab, transform);
            newplanet.GetComponentInChildren<MeshRenderer>().material = tempPlanetMatList[i];
            string[] matname = tempPlanetMatList[i].name.Split('_');

            switch (matname[0])
            {
                case "metal":
                    if (numberofmetal > 0)
                    {
                        newplanet.planetType = PlanetType.MetalPlanet;
                        numberofmetal--;
                    }
                    else
                    {
                        newplanet.planetType = PlanetType.NullPlanet;
                    }
                    break;
                case "ocean":
                    if (numberoforganic > 0)
                    {
                        newplanet.planetType = PlanetType.OrganicPlanet;
                        numberoforganic--;
                    }
                    else
                    {
                        newplanet.planetType = PlanetType.NullPlanet;
                    }
                    break;
                case "gas":
                    if (numberofgas > 0)
                    {
                        newplanet.planetType = PlanetType.GasPlanet;
                        numberofgas--;
                    }
                    else
                    {
                        newplanet.planetType = PlanetType.NullPlanet;
                    }
                    break;
                case "mineral":
                    if (numberofmineral > 0)
                    {
                        newplanet.planetType = PlanetType.MineralPlanet;
                        numberofmineral--;
                    }
                    else
                    {
                        newplanet.planetType = PlanetType.NullPlanet;
                    }
                    break;
                default:
                    Debug.Log("Wrong material name!");
                    break;
            }
            planetList.Add(newplanet);
        }
        planetList.Shuffle();

    }

    private void CreatePortsInSolar()
    {
        foreach (var solarCluster in solarClusters)
        {
            foreach (var solar in solarCluster.solarSystems)
            {
                solar.CreateConnections();
            }
        }
    }
    private void CreatePlanetMatList()
    {
        totalPlanetCount = 0;
        foreach (var cluster in solarClusters)
        {
            foreach (var system in cluster.solarSystems)
            {
                foreach (var planet in system.planets)
                {
                    totalPlanetCount++;
                }
            }
        }

        float matCount = 0;

        for (int i = 0; i < planetMatList.percentages.Length; i++)
        {
            matCount += planetMatList.percentages[i];
        }

        float solarcountdividematcount = totalPlanetCount / matCount;

        for (int i = 0; i < planetMatList.percentages.Length; i++)
        {
            planetMatList.percentages[i] *= solarcountdividematcount;
        }

        tempPlanetMatList = new List<Material>();
        for (int i = 0; i < planetMatList.percentages.Length; i++)
        {
            var tempMat = planetMatList.listOfMaterial[i];

            for (int j = 0; j < Mathf.RoundToInt(planetMatList.percentages[i]); j++)
            {
                tempPlanetMatList.Add(tempMat);
            }
        }

        if (totalPlanetCount > tempPlanetMatList.Count)
        {
            int diff = totalPlanetCount - tempPlanetMatList.Count;
            for (int i = 0; i < diff; i++)
            {
                tempPlanetMatList.Add(planetMatList.listOfMaterial[0]);
            }
        }
    }

    private void CreateStarMatList()
    {
        int totalSolarCount = 0;
        for (int i = 0; i < solarClusters.Count; i++)
        {
            for (int j = 0; j < solarClusters[i].solarSystems.Count; j++)
            {
                totalSolarCount++;
            }
        }
        float matCount = 0;
        for (int i = 0; i < starMatList.percentages.Length; i++)
        {
            matCount += starMatList.percentages[i];
        }

        float solarcountdividematcount = totalSolarCount / matCount;

        for (int i = 0; i < starMatList.percentages.Length; i++)
        {
            starMatList.percentages[i] *= solarcountdividematcount;
        }

        tempMaterials = new List<Material>();
        for (int i = 0; i < starMatList.percentages.Length; i++)
        {
            var tempMat = starMatList.listOfMaterial[i];

            for (int j = 0; j < Mathf.RoundToInt(starMatList.percentages[i]); j++)
            {
                tempMaterials.Add(tempMat);
            }
        }
        if (totalSolarCount > tempMaterials.Count)
        {
            int diff = totalSolarCount - tempMaterials.Count;
            for (int i = 0; i < diff; i++)
            {
                tempMaterials.Add(starMatList.listOfMaterial[0]);
            }
        }

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
                if (Mathf.Abs(distance) < StaticVariablesStorage.solarSystemDistance + StaticVariablesStorage.randomizationRange)
                {
                    SolarSystem[] road = new SolarSystem[] { solarClusters[i].solarSystems[0], solarClusters[i].solarSystems[j] };
                    solarClusters[i].solarSystems[0].connectedSolars.Add(solarClusters[i].solarSystems[j]);
                    solarClusters[i].solarSystems[j].connectedSolars.Add(solarClusters[i].solarSystems[0]);
                    roads.Add(road);
                    //Debug.DrawLine(solarClusters[i].solarSystems[0].transform.position, solarClusters[i].solarSystems[j].transform.position, Color.gray, 100f);
                }
                if (j != 1)
                {
                    SolarSystem[] road = new SolarSystem[] { solarClusters[i].solarSystems[j - 1], solarClusters[i].solarSystems[j] };
                    solarClusters[i].solarSystems[j - 1].connectedSolars.Add(solarClusters[i].solarSystems[j]);
                    solarClusters[i].solarSystems[j].connectedSolars.Add(solarClusters[i].solarSystems[j - 1]);
                    roads.Add(road);
                    //Debug.DrawLine(solarClusters[i].solarSystems[j - 1].transform.position, solarClusters[i].solarSystems[j].transform.position, Color.gray, 100f);
                }

            }
            for (int t = 0; t < solarClusters.Count; t++)
            {
                if (solarClusters[i] != solarClusters[t])
                {
                    float clusterDistance = Vector3.Distance(solarClusters[i].clusterLocation, solarClusters[t].clusterLocation);
                    if (clusterDistance < StaticVariablesStorage.solarClusterDistance + StaticVariablesStorage.randomizationRange)
                    {
                        //clasterlar arasında en yakın olan solar systemleri seciyoruz.
                        SolarSystem[] tempRoad = new SolarSystem[2];
                        // SolarSystem[] tempRoad_transpose = new SolarSystem[2];
                        float distanceClusterCon = StaticVariablesStorage.solarClusterDistance;
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
                                    // tempRoad_transpose[0] = solarClusters[t].solarSystems[x];
                                    // tempRoad_transpose[1] = solarClusters[i].solarSystems[y];
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
                    if (roads[i][0] == roads[j][1] && roads[i][1] == roads[j][0])
                    {
                        roads[j][0].connectedSolars.Remove(roads[j][1]);
                        roads[j][1].connectedSolars.Remove(roads[j][0]);
                        roads.Remove(roads[j]);
                        i--;
                        j--;
                    }

                }
            }

            LineRenderer roadPrefab = Instantiate(roadRendererPrefab, roads[i][0].transform.position, roads[i][0].transform.rotation, transform);
            roadPrefab.SetPosition(0, roads[i][0].transform.position);
            roadPrefab.SetPosition(1, roads[i][1].transform.position);


        }

        ////sadece distancelara bakalım böyle çok saçma oldu sanki hata veriyor
        // foreach (var cluster in solarClusters)
        // {
        //     foreach (var system in cluster.solarSystems)
        //     {
        //         if (solarClusters[0].solarSystems[0] != system)
        //         {
        //             PathFinder.pathFindingWithDistance(solarClusters[0].solarSystems[0], system);
        //         }
        //     }
        // }

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
        List<Vector3> targetPositionList = GetPositionList(destination, distances, circleCounts);
        List<Vector3> ClusterPositionList = new List<Vector3>();
        for (int i = 0; i < solarClusterCount; i++)
        {
            ClusterPositionList.Add(targetPositionList[i]);
        }
        for (int i = 0; i < solarClusterCount; i++)
        {
            SolarCluster cluster = Instantiate(solarClusterPrefab, transform);
            cluster.name = "cluster " + i;
            solarClusters.Add(cluster);
        }

        for (int i = 0; i < solarClusterCount; i++)
        {
            solarClusters[i].clusterLocation = ClusterPositionList[i];
            //add random position to solar systems
            int randomX = Random.Range(-StaticVariablesStorage.randomizationRange, StaticVariablesStorage.randomizationRange);
            int randomZ = Random.Range(-StaticVariablesStorage.randomizationRange, StaticVariablesStorage.randomizationRange);
            Vector3 randomPos = new Vector3(randomX, 0, randomZ);

            solarClusters[i].clusterLocation += randomPos;
            int solarSystemCountInCluster = Random.Range(StaticVariablesStorage.minSolarSystemCount, StaticVariablesStorage.maxSolarSystemCount);
            solarClusters[i].solarSystems = SolarSystemLocationCreator(solarClusters[i].clusterLocation, solarSystemCountInCluster, solarClusters[i].gameObject.transform);
        }
    }
    private void CreateStars()
    {
        for (int i = 0; i < solarClusters.Count; i++)
        {
            foreach (var solar in solarClusters[i].solarSystems)
            {
                CreateStar(solar);
            }
        }
    }
    private void CreatePlanets()
    {
        foreach (SolarCluster cluster in solarClusters)
        {
            foreach (SolarSystem solar in cluster.solarSystems)
            {
                solar.PlanetRandomization(planetList);
                solar.CreateBillboard();
            }
        }
    }
    public void CreateStar(SolarSystem parent)
    {
        var star = Instantiate(sunPrefab, parent.transform);
        int randomStar = Random.Range(0, tempMaterials.Count);
        star.GetComponentInChildren<MeshRenderer>().material = tempMaterials[randomStar];
        tempMaterials.RemoveAt(randomStar);
        parent.star = star.GetComponent<Star>();
        parent.CreateSystem();
    }

    private List<SolarSystem> SolarSystemLocationCreator(Vector3 destination, int localSystemCount, Transform solarCluster)
    {
        float[] distances = new float[StaticVariablesStorage.solarSystemCircleCount];
        int[] circleCounts = new int[StaticVariablesStorage.solarSystemCircleCount];
        for (int i = 0; i < StaticVariablesStorage.solarSystemCircleCount; i++)
        {
            circleCounts[i] = Mathf.RoundToInt((2 * Mathf.PI * StaticVariablesStorage.solarSystemDistance * (i + 1) / StaticVariablesStorage.solarSystemDistance)) + 1;
            distances[i] = (StaticVariablesStorage.solarSystemDistance * i) + StaticVariablesStorage.solarSystemDistance;
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
            solarSystem.name = solarCluster.name + " solar " + i;
            solarSystem.GetComponent<SolarSystem>().solarSystemName = solarCluster.name + " - solar " + i;
            localSolarSystems.Add(solarSystem.GetComponent<SolarSystem>());
            solarSystem.transform.position = localArrangedTargetPositionList[targetPositionListIndex];
            //add random position to solar systems
            int randomX = Random.Range(-StaticVariablesStorage.randomizationRange, StaticVariablesStorage.randomizationRange);
            int randomZ = Random.Range(-StaticVariablesStorage.randomizationRange, StaticVariablesStorage.randomizationRange);
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

}
