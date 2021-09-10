using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SolarSystem : MonoBehaviour
{
    [SerializeField]
    private Planet PlanetPrefab;
    [SerializeField]
    private Orbit OrbitPrefab;
    [SerializeField]
    private float starScaleFactor;
    [SerializeField]
    private SolarPort solarPortPrefab;

    public string solarSystemName;
    public Planet[] planets;
    public Star star;
    public float solarDistance = float.MaxValue;
    public List<SolarSystem> connectedSolars;
    public List<IStation> stations;

    private GameObject spawnPoint;
    private Transform[] spawnPoints;
    [SerializeField]
    private float planetDistance = 10f;
    private float sunScale = 10;
    private float portDistance;
    private int planetCount;
    private int maxResourceCount = 2;

    [Header("billboard prefabs")]
    [SerializeField]
    private Transform planetBillboardTransform;
    [SerializeField]
    private Transform resourceBillboardTransform;
    [SerializeField]
    private GameObject planetBillboard;
    [SerializeField]
    private GameObject resourceBillboard;

    [Header("Raw Materials")]
    [SerializeField]
    private ItemSO metalSO;
    [SerializeField]
    private ItemSO mineralSO;
    [SerializeField]
    private ItemSO gasSO;
    [SerializeField]
    private ItemSO organicSO;

    private void Start()
    {

    }
    public void CreateSystem()
    {
        planetCount = Random.Range(3, 8);
        spawnPoints = new Transform[planetCount];
        planets = new Planet[planetCount];
        portDistance = (planetCount + 1) * planetDistance;
    }

    public void CreateBillboard()
    {

        foreach (var planet in planets)
        {
            switch (planet.planetType)
            {
                case PlanetType.OrganicPlanet:
                    Item organicItem = new Item(organicSO);
                    planet.Item = organicItem;
                    GameObject organicPlanet = Instantiate(resourceBillboard, resourceBillboardTransform);
                    organicPlanet.GetComponent<Image>().sprite = planet.Item.uiDisplay;
                    UniverseController.organicPlanets.Add(planet);
                    break;
                case PlanetType.MetalPlanet:
                    Item metalItem = new Item(metalSO);
                    planet.Item = metalItem;
                    GameObject rockPlanet = Instantiate(resourceBillboard, resourceBillboardTransform);
                    rockPlanet.GetComponent<Image>().sprite = planet.Item.uiDisplay;
                    UniverseController.metalPlanets.Add(planet);
                    break;
                case PlanetType.GasPlanet:
                    Item gasItem = new Item(gasSO);
                    planet.Item = gasItem;
                    GameObject gasPlanet = Instantiate(resourceBillboard, resourceBillboardTransform);
                    gasPlanet.GetComponent<Image>().sprite = planet.Item.uiDisplay;
                    UniverseController.gasPlanets.Add(planet);
                    break;
                case PlanetType.MineralPlanet:
                    Item mineralItem = new Item(mineralSO);
                    planet.Item = mineralItem;
                    GameObject mineralPlanet = Instantiate(resourceBillboard, resourceBillboardTransform);
                    mineralPlanet.GetComponent<Image>().sprite = planet.Item.uiDisplay;
                    UniverseController.mineralPlanets.Add(planet);
                    break;
                default:
                    break;
            }
        }
    }
    public List<Planet> PlanetRandomization(List<Planet> planetList)
    {
        planetList.Shuffle();
        for (int i = 1; i < planetCount + 1; i++)
        {
            spawnPoint = new GameObject();
            spawnPoint.transform.position = transform.position;
            spawnPoint.transform.rotation = new Quaternion(spawnPoint.transform.rotation.x, Random.rotation.y, spawnPoint.transform.rotation.z, spawnPoint.transform.rotation.w);
            spawnPoints[i - 1] = spawnPoint.transform;
            spawnPoint.transform.parent = transform;
            Orbit orbit = Instantiate(OrbitPrefab, spawnPoint.transform);
            var planetPos = orbit.CreatePoints(i * planetDistance, i * planetDistance);
            int rngPlanet = Random.Range(0, planetList.Count);
            if (planetList[rngPlanet].planetType != PlanetType.NullPlanet)
            {
                if (maxResourceCount > 0)
                {
                    maxResourceCount--;
                    planets[i - 1] = planetList[rngPlanet];
                    planets[i - 1].transform.parent = transform;
                    planets[i - 1].transform.localPosition = planetPos;
                    planets[i - 1].ownerSolarSystem = this;
                    planetList.Remove(planets[i - 1]);
                }
                else
                {
                    planets[i - 1] = planetList[rngPlanet];
                    while (planets[i - 1].planetType != PlanetType.NullPlanet)
                    {
                        int rngPlanetAgain = Random.Range(0, planetList.Count);
                        planets[i - 1] = planetList[rngPlanetAgain];
                    }

                    planets[i - 1].transform.parent = transform;
                    planets[i - 1].transform.localPosition = planetPos;
                    planets[i - 1].ownerSolarSystem = this;
                    planetList.Remove(planets[i - 1]);
                }
            }
            else
            {
                planets[i - 1] = planetList[rngPlanet];
                planets[i - 1].transform.parent = transform;
                planets[i - 1].transform.localPosition = planetPos;
                planets[i - 1].ownerSolarSystem = this;
                planetList.Remove(planets[i - 1]);
            }

        }
        return planetList;

    }


    public void CreateConnections()
    {
        foreach (var solarPort in connectedSolars)
        {
            Vector3 targetDirection = solarPort.transform.position - transform.position;
            SolarPort port = Instantiate(solarPortPrefab, transform);
            port.solarSystemToConnect = solarPort;
            port.transform.rotation = Quaternion.LookRotation(targetDirection);
            port.transform.position += port.transform.forward * portDistance;
        }
    }
    public void ShowSystem()
    {
        sunScale = star.transform.localScale.x;
        star.transform.localScale = Vector3.one * sunScale / starScaleFactor;
    }
    public void HideSystem()
    {
        star.transform.localScale = Vector3.one * sunScale;
    }

}
