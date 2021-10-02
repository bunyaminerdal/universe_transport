using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Collections;
using System.Linq;
using Unity.Burst;

public class NewUniverseCreator : MonoBehaviour
{
    [Header("prefabs")]
    [SerializeField] private LineRenderer roadRendererPrefab;
    [SerializeField] private MaterialList starMatList;
    [SerializeField] private MaterialList planetMatList;
    [SerializeField] private GameObject starPrefab;
    [SerializeField] private SolarSystem solarSystemPrefab;
    [SerializeField] private SolarCluster solarClusterPrefab;
    [SerializeField] private Planet PlanetPrefab;
    [SerializeField] private IntermediateProductStation intermediateProductStationPrefab;
    [SerializeField] private FinalProductStation finalProductStationPrefab;

    [Header("Intermediate Products")]
    [SerializeField] private Item consumableItem;
    [SerializeField] private Item plasticItem;
    [SerializeField] private Item plateItem;
    [SerializeField] private Item electronicItem;
    [SerializeField] private Item partItem;
    [SerializeField] private Item fuelItem;

    [Header("Final Products")]
    [SerializeField] private Item shelterItem;
    [SerializeField] private Item lifeSuitItem;
    [SerializeField] private Item toolItem;
    [SerializeField] private Item landVehicleItem;
    [SerializeField] private Item droidItem;
    [SerializeField] private Item machineItem;

    //Local variables
    private SolarClusterStruct[] solarClustersStruct;
    private SolarCluster[] solarClusters;
    private List<SolarSystemStruct[]> roads;
    private List<Material> tempStarMaterials;
    private List<Material> tempPlanetMatList;
    private List<Planet> planetList;
    private List<Planet> emptyPlanetList;
    private Roads newRoads;
    private int totalPlanetCount;


    private void OnEnable()
    {
        PlayerManagerEventHandler.InteractionEvent.AddListener(() => StartCoroutine(GenerateUniverse()));
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
        PlayerManagerEventHandler.InteractionEvent.RemoveListener(() => StartCoroutine(GenerateUniverse()));

    }

    private IEnumerator GenerateUniverse()
    {
        UIEventHandler.CreatingUniverse?.Invoke(true);
        yield return null;
        var startTime = Time.realtimeSinceStartup;
        SolarClusterCreator(Vector3.zero);
        CalculateRoads();
        CheckConnection();
        Debug.Log("Universe create time: " + ((Time.realtimeSinceStartup - startTime) * 1000f));
        yield return null;
    }

    private IEnumerator CreateUniverse()
    {
        transform.Clear();
        CreateRoads();
        CreateStarMatList();
        CreateClustersAndSystems();
        InitializeSolarSystems();
        CreatePlanetMatList();
        CalculateRawMaterialsCount();
        CreatePlanets();
        CreateIntermediateProduct();
        CreateFinalProduct();
        yield return null;
        UIEventHandler.CreatingUniverse?.Invoke(false);
        PlayerManagerEventHandler.SolarClustersReadyEvent?.Invoke(solarClustersStruct);
    }

    private void CreateIntermediateProduct()
    {
        if (emptyPlanetList.Count <= 0) return;
        int numOfStation = (int)(emptyPlanetList.Count * StaticVariablesStorage.intermediateProductStationProbability);

        IntermediateProductStationsCreate(numOfStation, consumableItem);
        IntermediateProductStationsCreate(numOfStation, partItem);
        IntermediateProductStationsCreate(numOfStation, plasticItem);
        IntermediateProductStationsCreate(numOfStation, electronicItem);
        IntermediateProductStationsCreate(numOfStation, plateItem);
        IntermediateProductStationsCreate(numOfStation, fuelItem);

        foreach (var solarCluster in solarClusters)
        {
            foreach (var solar in solarCluster.solarSystems)
            {
                solar.CreateIntermediateProductStationBillboard();
            }
        }
    }
    private void CreateFinalProduct()
    {
        if (emptyPlanetList.Count <= 0) return;
        int numOfStation = (int)(emptyPlanetList.Count * StaticVariablesStorage.finalProductStationProbability);
        FinalProductStationsCreate(numOfStation, shelterItem);
        FinalProductStationsCreate(numOfStation, lifeSuitItem);
        FinalProductStationsCreate(numOfStation, toolItem);
        FinalProductStationsCreate(numOfStation, landVehicleItem);
        FinalProductStationsCreate(numOfStation, droidItem);
        FinalProductStationsCreate(numOfStation, machineItem);
        foreach (var solarCluster in solarClusters)
        {
            foreach (var solar in solarCluster.solarSystems)
            {
                solar.CreateFinalProductStationBillboard();
            }
        }
    }
    private void IntermediateProductStationsCreate(int numOfStation, Item product)
    {
        for (int i = 0; i < numOfStation; i++)
        {
            int randomEmptyPlanet = Random.Range(0, emptyPlanetList.Count);
            Planet selectedPlanet = emptyPlanetList[randomEmptyPlanet];
            if (selectedPlanet.ownerSolarSystem.IntermediateProductStations.Count >= StaticVariablesStorage.numOfIntermediateProductStationInSolarSystem)
            {
                emptyPlanetList.Remove(selectedPlanet);
                numOfStation++;
            }
            else
            {
                int stationInCluster = 0;
                foreach (var solar in selectedPlanet.ownerSolarSystem.ownerCluster.solarSystems)
                {
                    stationInCluster += solar.IntermediateProductStations.Count;
                }
                if (stationInCluster >= StaticVariablesStorage.numOfIntermediateProductStationInCluster)
                {
                    emptyPlanetList.Remove(selectedPlanet);
                    numOfStation++;
                }
                else
                {
                    emptyPlanetList.Remove(selectedPlanet);
                    IntermediateProductStation newStation = Instantiate(intermediateProductStationPrefab, selectedPlanet.ownerSolarSystem.gameObject.transform);
                    newStation.transform.position = selectedPlanet.transform.position;
                    newStation.Product = product;
                    newStation.stationName = "Station";
                    newStation.ownerSolarSystem = selectedPlanet.ownerSolarSystem;
                    selectedPlanet.ownerSolarSystem.planets = selectedPlanet.ownerSolarSystem.planets.Where((source) => source != selectedPlanet).ToArray();
                    selectedPlanet.ownerSolarSystem.PlanetCount--;
                    selectedPlanet.ownerSolarSystem.IntermediateProductStations.Add(newStation);
                    Destroy(selectedPlanet.orbit.gameObject);//destroy orbit which is belong to selected planet
                    Destroy(selectedPlanet.gameObject); //destroy selected planet
                }
            }
        }
    }
    private void FinalProductStationsCreate(int numOfStation, Item product)
    {
        for (int i = 0; i < numOfStation; i++)
        {
            int randomEmptyPlanet = Random.Range(0, emptyPlanetList.Count);
            Planet selectedPlanet = emptyPlanetList[randomEmptyPlanet];
            if (selectedPlanet.ownerSolarSystem.FinalProductStations.Count >= StaticVariablesStorage.numOfFinalProductStationInSolar)
            {
                emptyPlanetList.Remove(selectedPlanet);
                numOfStation++;
            }
            else
            {
                int stationInCluster = 0;
                foreach (var solar in selectedPlanet.ownerSolarSystem.ownerCluster.solarSystems)
                {
                    stationInCluster += solar.FinalProductStations.Count;
                }
                if (stationInCluster >= StaticVariablesStorage.numOfFinalProductStationInCluster)
                {
                    emptyPlanetList.Remove(selectedPlanet);
                    numOfStation++;
                }
                else
                {
                    emptyPlanetList.Remove(selectedPlanet);
                    FinalProductStation newStation = Instantiate(finalProductStationPrefab, selectedPlanet.ownerSolarSystem.gameObject.transform);
                    newStation.transform.position = selectedPlanet.transform.position;
                    newStation.Product = product;
                    newStation.stationName = "Final Product Station";
                    newStation.ownerSolarSystem = selectedPlanet.ownerSolarSystem;
                    selectedPlanet.ownerSolarSystem.planets = selectedPlanet.ownerSolarSystem.planets.Where((source) => source != selectedPlanet).ToArray();
                    selectedPlanet.ownerSolarSystem.PlanetCount--;
                    selectedPlanet.ownerSolarSystem.FinalProductStations.Add(newStation);
                    Destroy(selectedPlanet.orbit.gameObject);//destroy orbit which is belong to selected planet
                    Destroy(selectedPlanet.gameObject); //destroy selected planet
                }
            }
        }
    }
    private void CreatePlanets()
    {
        foreach (SolarCluster cluster in solarClusters)
        {
            foreach (SolarSystem solar in cluster.solarSystems)
            {
                solar.PlanetRandomization(planetList, emptyPlanetList);
                solar.CreateRawMaterialBillboard();
            }
        }
    }
    private void CalculateRawMaterialsCount()
    {
        planetList = new List<Planet>();
        emptyPlanetList = new List<Planet>();
        int numberoforganic = (int)(totalPlanetCount * StaticVariablesStorage.rawMaterialProbability);
        int numberofmetal = (int)(totalPlanetCount * StaticVariablesStorage.rawMaterialProbability);
        int numberofmineral = (int)(totalPlanetCount * StaticVariablesStorage.rawMaterialProbability);
        int numberofgas = (int)(totalPlanetCount * StaticVariablesStorage.rawMaterialProbability);
        tempPlanetMatList.ShuffleList();

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
        planetList.ShuffleList();
    }
    private void CreatePlanetMatList()
    {
        tempPlanetMatList = new List<Material>();
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
        tempPlanetMatList = planetMatList.CreateMatList(totalPlanetCount);
    }
    private void CreateClustersAndSystems()
    {
        solarClusters = new SolarCluster[solarClustersStruct.Length];
        for (int i = 0; i < solarClustersStruct.Length; i++)
        {
            SolarCluster cluster = Instantiate(solarClusterPrefab, transform);
            cluster.name = "cluster " + i;
            cluster.solarClusterStruct = solarClustersStruct[i];
            for (int j = 0; j < solarClustersStruct[i].solarSystemsStruct.Length; j++)
            {
                SolarSystem solarSystem = Instantiate(solarSystemPrefab, cluster.transform);
                solarSystem.solarSystemStruct = solarClustersStruct[i].solarSystemsStruct[j];
                solarSystem.name = cluster.name + " - solar " + j;
                solarSystem.solarSystemName = solarSystem.name;
                solarSystem.ownerCluster = cluster;
                solarSystem.gameObject.transform.position = solarSystem.solarSystemStruct.solarLocation;
                solarClustersStruct[i].solarSystemsStruct[j].setSolarSystem(solarSystem);
                cluster.solarSystems.Add(solarSystem);
            }
            solarClusters[i] = cluster;
        }
    }
    private void InitializeSolarSystems()
    {
        foreach (var cluster in solarClusters)
        {
            foreach (var solar in cluster.solarSystems)
            {
                solar.CreateSystem();
                CreateStarInSolar(solar);
                solar.CreateSolarPortsWithStruct();
            }
        }
    }
    public void CreateStarInSolar(SolarSystem parent)
    {
        var star = Instantiate(starPrefab, parent.transform);
        int randomStar = Random.Range(0, tempStarMaterials.Count);
        star.GetComponentInChildren<MeshRenderer>().material = tempStarMaterials[randomStar];
        parent.star = star.GetComponent<Star>();
        switch (tempStarMaterials[randomStar].name)
        {
            case "DwarfStar":
                parent.star.StarType = StarType.DwarfStar;
                break;
            case "BlueGiant":
                parent.star.StarType = StarType.BlueGiant;
                break;
            case "RedGiant":
                parent.star.StarType = StarType.RedGiant;
                break;
            case "SuperGiant":
                parent.star.StarType = StarType.SuperGiant;
                break;
            case "YellowStar":
                parent.star.StarType = StarType.YellowStar;
                break;
            default:
                break;
        }
        tempStarMaterials.RemoveAt(randomStar);
    }
    private void CreateStarMatList()
    {
        tempStarMaterials = new List<Material>();
        int totalSolarCount = 0;
        for (int i = 0; i < solarClustersStruct.Length; i++)
        {
            for (int j = 0; j < solarClustersStruct[i].solarSystemsStruct.Length; j++)
            {
                totalSolarCount++;
            }
        }
        tempStarMaterials = starMatList.CreateMatList(totalSolarCount);
    }
    private void CreateRoads()
    {
        GameObject RoadContainer = new GameObject("RoadContainer");
        RoadContainer.transform.parent = transform;
        foreach (var road in newRoads.roads)
        {
            LineRenderer roadPrefab = Instantiate(roadRendererPrefab, RoadContainer.transform);
            roadPrefab.SetPosition(0, road.startSolar.solarLocation);
            roadPrefab.SetPosition(1, road.endSolar.solarLocation);
            newRoads.roadsWGo.Add(road, roadPrefab);
        }
    }
    private void CheckConnection()
    {
        PathFinderWithStruct.CalculateAllDistances(solarClustersStruct[0].solarSystemsStruct[0]);
        // PathFinderWithStruct.pathFindingWithDistance(solarClustersStruct[0].solarSystemsStruct[0], solarClustersStruct[12].solarSystemsStruct[1], solarClustersStruct);
        List<SolarSystemStruct> solarsystemsWithoutConnection = new List<SolarSystemStruct>();
        foreach (var cluster in solarClustersStruct)
        {
            foreach (var system in cluster.solarSystemsStruct)
            {
                if (solarClustersStruct[0].solarSystemsStruct[0].solarLocation != system.solarLocation)
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
            StartCoroutine(GenerateUniverse());
        }
        else
        {
            Debug.Log("All systems are connected!");
            StartCoroutine(CreateUniverse());
        }
    }
    private void CalculateRoads()
    {
        newRoads = new Roads();
        for (int i = 0; i < solarClustersStruct.Length; i++)
        {
            for (int j = 1; j < solarClustersStruct[i].solarSystemsStruct.Length; j++)
            {
                float distance = Vector3.Distance(solarClustersStruct[i].solarSystemsStruct[0].solarLocation, solarClustersStruct[i].solarSystemsStruct[j].solarLocation);
                if (Mathf.Abs(distance) < StaticVariablesStorage.solarSystemDistance + StaticVariablesStorage.randomizationRange)
                {
                    Road newRoad = new Road(solarClustersStruct[i].solarSystemsStruct[0], solarClustersStruct[i].solarSystemsStruct[j]);
                    solarClustersStruct[i].solarSystemsStruct[0].connectedSolars.Add(solarClustersStruct[i].solarSystemsStruct[j]);
                    solarClustersStruct[i].solarSystemsStruct[j].connectedSolars.Add(solarClustersStruct[i].solarSystemsStruct[0]);
                    newRoads.roads.Add(newRoad);
                }
                if (j != 1)
                {
                    Road newRoad = new Road(solarClustersStruct[i].solarSystemsStruct[j - 1], solarClustersStruct[i].solarSystemsStruct[j]);
                    solarClustersStruct[i].solarSystemsStruct[j - 1].connectedSolars.Add(solarClustersStruct[i].solarSystemsStruct[j]);
                    solarClustersStruct[i].solarSystemsStruct[j].connectedSolars.Add(solarClustersStruct[i].solarSystemsStruct[j - 1]);
                    newRoads.roads.Add(newRoad);

                }

            }
            for (int t = 0; t < solarClustersStruct.Length; t++)
            {
                if (solarClustersStruct[i].clusterLocation != solarClustersStruct[t].clusterLocation)
                {
                    float clusterDistance = Vector3.Distance(solarClustersStruct[i].clusterLocation, solarClustersStruct[t].clusterLocation);
                    if (clusterDistance < StaticVariablesStorage.solarClusterDistance + StaticVariablesStorage.randomizationRange)
                    {
                        Road newRoad = new Road();
                        float distanceClusterCon = StaticVariablesStorage.solarClusterDistance;
                        for (int y = 0; y < solarClustersStruct[i].solarSystemsStruct.Length; y++)
                        {

                            for (int x = 0; x < solarClustersStruct[t].solarSystemsStruct.Length; x++)
                            {
                                float distanceClusterConnection = Vector3.Distance(solarClustersStruct[i].solarSystemsStruct[y].solarLocation, solarClustersStruct[t].solarSystemsStruct[x].solarLocation);

                                if (distanceClusterConnection < distanceClusterCon)
                                {
                                    distanceClusterCon = distanceClusterConnection;
                                    newRoad.startSolar = solarClustersStruct[i].solarSystemsStruct[y];
                                    newRoad.endSolar = solarClustersStruct[t].solarSystemsStruct[x];

                                }
                            }
                        }
                        newRoad.startSolar.connectedSolars.Add(newRoad.endSolar);
                        newRoad.endSolar.connectedSolars.Add(newRoad.startSolar);
                        newRoads.roads.Add(newRoad);
                        //Debug.DrawLine(tempRoad[0].transform.position, tempRoad[1].transform.position, Color.gray, 100f);
                    }
                }

            }

        }

        for (int i = 0; i < newRoads.roads.Count; i++)
        {
            for (int j = 0; j < newRoads.roads.Count; j++)
            {
                if (newRoads.roads[i] != newRoads.roads[j])
                {
                    if (newRoads.roads[i].startSolar.solarLocation == newRoads.roads[j].endSolar.solarLocation &&
                     newRoads.roads[i].endSolar.solarLocation == newRoads.roads[j].startSolar.solarLocation)
                    {
                        newRoads.roads[j].startSolar.connectedSolars.Remove(newRoads.roads[j].endSolar);
                        newRoads.roads[j].endSolar.connectedSolars.Remove(newRoads.roads[j].startSolar);
                        newRoads.roads.Remove(newRoads.roads[j]);
                        i--;
                        j--;
                    }
                }
            }
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
        solarClustersStruct = new SolarClusterStruct[solarClusterCount];
        for (int i = 0; i < solarClusterCount; i++)
        {
            int randomX = Random.Range(-StaticVariablesStorage.randomizationRange, StaticVariablesStorage.randomizationRange);
            int randomZ = Random.Range(-StaticVariablesStorage.randomizationRange, StaticVariablesStorage.randomizationRange);
            Vector3 randomPos = new Vector3(randomX, 0, randomZ);
            int solarSystemCountInCluster = Random.Range(StaticVariablesStorage.minSolarSystemCount, StaticVariablesStorage.maxSolarSystemCount);

            var solarClusterStruct = new SolarClusterStruct
            {
                clusterLocation = ClusterPositionList[i] + randomPos,
                solarSystemsStruct = SolarSystemLocationCreator(ClusterPositionList[i] + randomPos, solarSystemCountInCluster),
            };
            solarClustersStruct[i] = solarClusterStruct;
        }
    }
    [BurstCompile]
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
    [BurstCompile]
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
    [BurstCompile]
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
    [BurstCompile]
    private Vector3 ApplyRotationToVector(Vector3 vec, float angle)
    {
        return Quaternion.Euler(0, angle, 0) * vec;
    }
}
public class SolarClusterStruct
{
    public Vector3 clusterLocation;
    public SolarSystemStruct[] solarSystemsStruct;
}
public class SolarSystemStruct
{

    public float solarDistance { get; protected set; }
    public Vector3 solarLocation;
    public List<SolarSystemStruct> connectedSolars;
    public SolarSystem solarSystem { get; protected set; }

    public void setSolarSystem(SolarSystem solar)
    {
        solarSystem = solar;
    }
    public void solarDistanceChange(float distance)
    {
        solarDistance = distance;
    }
}
public class Road
{
    public SolarSystemStruct startSolar;
    public SolarSystemStruct endSolar;
    public LineRenderer lineRenderer;
    public Road()
    {

    }
    public Road(SolarSystemStruct _startSolar, SolarSystemStruct _endSolar)
    {
        this.startSolar = _startSolar;
        this.endSolar = _endSolar;
    }
}

public class Roads
{
    public Dictionary<Road, LineRenderer> roadsWGo;
    public List<Road> roads;
    public Roads()
    {
        roads = new List<Road>();
        roadsWGo = new Dictionary<Road, LineRenderer>();
    }
}
