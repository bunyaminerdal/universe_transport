using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

public class SolarSystem : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    [SerializeField] private Planet PlanetPrefab;
    [SerializeField] private Orbit OrbitPrefab;
    [SerializeField] private float starScaleFactor = 3f;
    [SerializeField] private float planetDistance = 30f;
    [SerializeField] private SolarPort solarPortPrefab;
    [SerializeField] private GameObject selectionBox;
    [SerializeField] private ConstructionNode constructionNodePrefab;
    public SolarSystemStruct solarSystemStruct;
    public string solarSystemName;
    public SolarCluster ownerCluster;
    public Planet[] planets;
    public Star star;
    public float solarDistance = float.MaxValue;
    public List<SolarSystem> connectedSolars;
    public List<IntermediateProductStation> IntermediateProductStations = new List<IntermediateProductStation>();
    public List<FinalProductStation> FinalProductStations = new List<FinalProductStation>();
    public List<City> Cities = new List<City>();
    public int PlanetCount;

    private GameObject spawnPoint;
    private Transform[] spawnPoints;

    private float sunScale;
    private float portDistance;
    private bool isInSolarsystem;
    private List<string> infoTexts = new List<string>();

    private TooltipController tooltipController;
    private GameObject selection;
    private List<ConstructionNode> possibleConstructionNodeList = new List<ConstructionNode>();

    [Header("billboard prefabs")]
    [SerializeField] private Transform planetBillboardTransform;
    [SerializeField] private Transform resourceBillboardTransform;
    [SerializeField] private GameObject planetBillboard;
    [SerializeField] private GameObject resourceBillboard;

    [Header("Raw Materials")]
    [SerializeField] private Item metalSO;
    [SerializeField] private Item mineralSO;
    [SerializeField] private Item gasSO;
    [SerializeField] private Item organicSO;
    private void Awake()
    {
        tooltipController = FindObjectOfType<TooltipController>();
    }
    private void Start()
    {

    }

    public void CreateSystem()
    {
        PlanetCount = Random.Range(StaticVariablesStorage.minPlanetCount, StaticVariablesStorage.maxPlanetCount);
        spawnPoints = new Transform[PlanetCount];
        planets = new Planet[PlanetCount];
        portDistance = (PlanetCount + 1) * planetDistance;
    }

    public void CreateRawMaterialBillboard()
    {

        foreach (var planet in planets)
        {
            switch (planet.planetType)
            {
                case PlanetType.OrganicPlanet:
                    planet.Item = organicSO;
                    GameObject organicPlanet = Instantiate(resourceBillboard, resourceBillboardTransform);
                    organicPlanet.GetComponent<Image>().sprite = planet.Item.uiDisplay;
                    break;
                case PlanetType.MetalPlanet:
                    planet.Item = metalSO;
                    GameObject rockPlanet = Instantiate(resourceBillboard, resourceBillboardTransform);
                    rockPlanet.GetComponent<Image>().sprite = planet.Item.uiDisplay;
                    break;
                case PlanetType.GasPlanet:
                    planet.Item = gasSO;
                    GameObject gasPlanet = Instantiate(resourceBillboard, resourceBillboardTransform);
                    gasPlanet.GetComponent<Image>().sprite = planet.Item.uiDisplay;
                    break;
                case PlanetType.MineralPlanet:
                    planet.Item = mineralSO;
                    GameObject mineralPlanet = Instantiate(resourceBillboard, resourceBillboardTransform);
                    mineralPlanet.GetComponent<Image>().sprite = planet.Item.uiDisplay;
                    break;
                default:
                    break;
            }
        }
        //CreateInfo();
    }
    public void CreateIntermediateProductStationBillboard()
    {
        foreach (var station in IntermediateProductStations)
        {
            GameObject product = Instantiate(resourceBillboard, resourceBillboardTransform);
            product.GetComponent<Image>().sprite = station.Product.uiDisplay;
        }
    }
    public void CreateFinalProductStationBillboard()
    {

        foreach (var station in FinalProductStations)
        {
            GameObject product = Instantiate(resourceBillboard, resourceBillboardTransform);
            product.GetComponent<Image>().sprite = station.Product.uiDisplay;
        }
    }
    public void CreateCityBillboard()
    {

        foreach (var city in Cities)
        {
            GameObject billboard = Instantiate(planetBillboard, planetBillboardTransform);
            billboard.GetComponentInChildren<TMP_Text>().text = city.population.ToString();
        }
        CreateInfo();
    }
    public List<Planet> PlanetRandomization(List<Planet> planetList, List<Planet> emptyPlanetList)
    {
        int maxResourceCount = StaticVariablesStorage.maxResourceCount;
        planetList.ShuffleList();
        for (int i = 1; i < PlanetCount + 1; i++)
        {
            spawnPoint = new GameObject();
            spawnPoint.transform.position = transform.position;
            spawnPoint.transform.rotation = new Quaternion(spawnPoint.transform.rotation.x, Random.rotation.y, spawnPoint.transform.rotation.z, spawnPoint.transform.rotation.w);
            spawnPoints[i - 1] = spawnPoint.transform;
            spawnPoint.transform.parent = transform;
            Orbit orbit = Instantiate(OrbitPrefab, spawnPoint.transform);
            var planetPos = orbit.CreatePoints(i * planetDistance, i * planetDistance, i);
            int rngPlanet = Random.Range(0, planetList.Count);

            //construction nodes creating
            foreach (var node in orbit.CreatePosibleConstructionNodes((i + 0.5f) * planetDistance, (i + 0.5f) * planetDistance, i))
            {
                if (i != PlanetCount)
                {
                    var constructionNode = Instantiate(constructionNodePrefab, spawnPoint.transform);
                    possibleConstructionNodeList.Add(constructionNode);
                    constructionNode.transform.localPosition = node;
                    constructionNode.gameObject.SetActive(false);
                }
            }
            if (planetList[rngPlanet].planetType != PlanetType.NullPlanet)
            {
                //maksimum raw material condition and not to be same raw material in solar system

                bool sameplanet = false;
                if (!StaticVariablesStorage.isSameRawMaterialExistInSolarsystem)
                {
                    if (i - 1 > 0)
                    {
                        foreach (var planet in planets)
                        {
                            if (planet != null)
                            {
                                if (planet.planetType == planetList[rngPlanet].planetType)
                                {
                                    sameplanet = true;
                                }
                            }
                        }
                    }
                }

                planets[i - 1] = planetList[rngPlanet];
                if (maxResourceCount < 1 || sameplanet)
                {
                    for (int j = 0; j < planetList.Count; j++)
                    {
                        if (planetList[j] != planets[i - 1])
                        {
                            if (planetList[j].planetType == PlanetType.NullPlanet)
                            {
                                planets[i - 1] = planetList[j];
                                emptyPlanetList.Add(planets[i - 1]);
                                break;
                            }
                        }
                    }
                    // every sameplanet takes one of maxresourcecount         
                    // if (sameplanet) maxResourceCount++;
                }
                planets[i - 1].transform.parent = transform;
                planets[i - 1].transform.localPosition = planetPos;
                planets[i - 1].ownerSolarSystem = this;
                planetList.Remove(planets[i - 1]);
                maxResourceCount--;
            }
            else
            {
                planets[i - 1] = planetList[rngPlanet];
                planets[i - 1].transform.parent = transform;
                planets[i - 1].transform.localPosition = planetPos;
                planets[i - 1].ownerSolarSystem = this;
                emptyPlanetList.Add(planets[i - 1]);
                planetList.Remove(planets[i - 1]);
            }
            planets[i - 1].orbit = orbit;
        }
        return planetList;

    }
    public void CreateSolarPortsWithStruct()
    {
        foreach (var solarStruct in solarSystemStruct.connectedSolars)
        {
            Vector3 targetDirection = solarStruct.solarLocation - transform.position;
            SolarPort port = Instantiate(solarPortPrefab, transform);

            foreach (var solarCluster in SolarCluster.SolarClusterList)
            {
                foreach (var solar in solarCluster.solarSystems)
                {
                    if (solar.solarSystemStruct == solarStruct)
                    {
                        port.solarSystemToConnect = solar;
                        connectedSolars.Add(solar);
                    }
                }
            }
            port.transform.rotation = Quaternion.LookRotation(targetDirection);
            port.transform.position += port.transform.forward * portDistance;
        }
    }
    public void ShowConstructionNodes(bool isShown)
    {
        foreach (var node in possibleConstructionNodeList)
        {
            node.gameObject.SetActive(isShown);
        }
    }

    private void CreateInfo()
    {
        infoTexts.Add("<align=center>SOLAR SYSTEM</align>");
        infoTexts.Add("Name: " + solarSystemName);
        infoTexts.Add("Star Type: " + star.StarType.ToString());
        infoTexts.Add("Planet Count: " + PlanetCount.ToString());
    }
    public void ShowSystem()
    {
        isInSolarsystem = true;
        sunScale = star.transform.localScale.x;
        star.transform.localScale = Vector3.one * sunScale / starScaleFactor;
    }
    public void HideSystem()
    {
        isInSolarsystem = false;
        star.transform.localScale = Vector3.one * sunScale;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        // when solar system has opened the pointer still inside de solar system and still continue to work
        if (isInSolarsystem) return;
        selection = Instantiate(selectionBox, transform);
        tooltipController.DisplayInfo(eventData.position, infoTexts);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (isInSolarsystem) return;
        Destroy(selection);
        tooltipController.HideInfo();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (isInSolarsystem) return;
        if (eventData.button != PointerEventData.InputButton.Left) return;
        if (selection)
        {
            Destroy(selection);
            tooltipController.HideInfo();
        }
        PlayerManagerEventHandler.SolarSelectionEvent?.Invoke(this);
    }
}
