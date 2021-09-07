using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    [SerializeField]
    private float rawMaterialProbabilityPercentage = 10f;
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
    private bool isOceanPlanetExists = false;
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

    public void CreateSystem()
    {
        int planetCount = Random.Range(3, 8);

        spawnPoints = new Transform[planetCount];
        planets = new Planet[planetCount];

        for (int i = 1; i < planetCount + 1; i++)
        {
            spawnPoint = new GameObject();
            spawnPoint.transform.position = transform.position;
            spawnPoint.transform.rotation = new Quaternion(spawnPoint.transform.rotation.x, Random.rotation.y, spawnPoint.transform.rotation.z, spawnPoint.transform.rotation.w);
            spawnPoints[i - 1] = spawnPoint.transform;
            spawnPoint.transform.parent = transform;
            Orbit orbit = Instantiate(OrbitPrefab, spawnPoint.transform);
            var planetPos = orbit.CreatePoints(i * planetDistance, i * planetDistance);
            Planet planet = Instantiate(PlanetPrefab, spawnPoint.transform);
            planets[i - 1] = planet;
            planet.transform.localPosition = planetPos;
            planet.ownerSolarSystem = this;
        }
        portDistance = (planetCount + 1) * planetDistance;

    }


    public void CreateBillboard()
    {
        foreach (var planet in planets)
        {
            switch (planet.planetType)
            {
                case PlanetType.OrganicPlanet:
                    GameObject oceanPlanet = Instantiate(planetBillboard, planetBillboardTransform);
                    break;
                case PlanetType.MetalPlanet:
                    GameObject rockPlanet = Instantiate(resourceBillboard, resourceBillboardTransform);
                    break;
                case PlanetType.GasPlanet:
                    GameObject gasPlanet = Instantiate(resourceBillboard, resourceBillboardTransform);
                    break;
                default:
                    break;
            }
        }
    }
    public List<Material> PlanetRandomization(List<Material> tempMaterialList)
    {
        foreach (var planet in planets)
        {
            int randomplanet = Random.Range(0, tempMaterialList.Count);
            string[] matname = tempMaterialList[randomplanet].name.Split('_');

            switch (matname[0])
            {
                case "rock":
                    planet.planetType = PlanetType.MetalPlanet;
                    break;
                case "ocean":
                    if (isOceanPlanetExists)
                    {
                        while (matname[0] != "ocean")
                        {
                            randomplanet = Random.Range(0, tempMaterialList.Count);
                            matname = tempMaterialList[randomplanet].name.Split('_');
                        }
                        switch (matname[0])
                        {
                            case "rock":
                                planet.planetType = PlanetType.MetalPlanet;
                                break;
                            case "gas":
                                planet.planetType = PlanetType.GasPlanet;
                                break;
                            default:
                                break;
                        }
                    }
                    else
                    {
                        planet.planetType = PlanetType.OrganicPlanet;
                        isOceanPlanetExists = true;
                    }
                    break;
                case "gas":
                    planet.planetType = PlanetType.GasPlanet;
                    break;
                default:
                    planet.planetType = PlanetType.MetalPlanet;
                    break;
            }

            planet.GetComponentInChildren<MeshRenderer>().material = tempMaterialList[randomplanet];
            tempMaterialList.RemoveAt(randomplanet);

            if (Random.value > 1 - (rawMaterialProbabilityPercentage / 100))
            {
                switch (planet.planetType)
                {
                    case PlanetType.GasPlanet:
                        Item gasitem = new Item(gasSO);
                        planet.Item = gasitem;
                        break;
                    case PlanetType.MetalPlanet:
                        Item metalitem = new Item(metalSO);
                        planet.Item = metalitem;
                        break;
                    case PlanetType.OrganicPlanet:
                        Item organicitem = new Item(organicSO);
                        planet.Item = organicitem;
                        break;
                    case PlanetType.MineralPlanet:
                        Item mineralitem = new Item(mineralSO);
                        planet.Item = mineralitem;
                        break;
                    default:
                        planet.Item = null;
                        break;
                }
            }
        }
        return tempMaterialList;
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
