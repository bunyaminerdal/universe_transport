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
    private MaterialList listofplanetmat;

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
    private bool isOceanPlanetExists;
    [Header("billboard prefabs")]
    [SerializeField]
    private Transform planetBillboardTransform;
    [SerializeField]
    private Transform resourceBillboardTransform;
    [SerializeField]
    private GameObject planetBillboard;
    [SerializeField]
    private GameObject resourceBillboard;

    public void CreateSystem()
    {
        int planetCount = Random.Range(3, 8);

        spawnPoints = new Transform[planetCount];
        planets = new Planet[planetCount];
        List<Material> tempMaterials = new List<Material>();
        for (int y = 0; y < listofplanetmat.percentages.Length; y++)
        {
            var tempMat = listofplanetmat.listOfMaterial[y];

            for (int j = 0; j < (int)listofplanetmat.percentages[y] * 10; j++)
            {
                tempMaterials.Add(tempMat);
            }
        }

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

            int randomplanet = Random.Range(0, tempMaterials.Count);
            string[] matname = tempMaterials[randomplanet].name.Split('_');

            switch (matname[0])
            {
                case "rock":
                    planet.planetType = PlanetType.RockPlanet;
                    break;
                case "ocean":
                    if (isOceanPlanetExists)
                    {
                        while (matname[0] != "ocean")
                        {
                            randomplanet = Random.Range(0, tempMaterials.Count);
                            matname = tempMaterials[randomplanet].name.Split('_');
                        }
                        switch (matname[0])
                        {
                            case "rock":
                                planet.planetType = PlanetType.RockPlanet;
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
                        isOceanPlanetExists = true;
                        planet.planetType = PlanetType.OceanPlanet;
                    }
                    break;
                case "gas":
                    planet.planetType = PlanetType.GasPlanet;
                    break;
                default:
                    planet.planetType = PlanetType.RockPlanet;
                    break;
            }
            planet.ownerSolarSystem = this;
            planet.GetComponentInChildren<MeshRenderer>().material = tempMaterials[randomplanet];
        }
        portDistance = (planetCount + 1) * planetDistance;
        
    }
    

    public void CreateBillboard()
    {
        foreach (var planet in planets)
        {
            switch (planet.planetType)
            {
                case PlanetType.OceanPlanet:
                    GameObject oceanPlanet = Instantiate(planetBillboard, planetBillboardTransform);
                    break;
                case PlanetType.RockPlanet:
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
