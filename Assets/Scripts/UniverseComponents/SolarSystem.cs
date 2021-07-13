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
    public string solarSystemName;
    public Planet[] planets;
    public Star star;
    public float solarDistance = float.MaxValue;
    public List<SolarSystem> connectedSolars;
    public List<IStation> stations;

    private GameObject spawnPoint;
    private Transform[] spawnPoints;
    private float planetDistance = 40f;
    private float sunScale = 10;

    public void CreateSystem()
    {
        int planetCount = Random.Range(3, 6);

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
            planet.GetComponentInChildren<MeshRenderer>().material = tempMaterials[randomplanet];

        }


    }
    public void ShowSystem(float systemDepth)
    {
        foreach (var spawnPoint in spawnPoints)
        {
            spawnPoint.transform.position = new Vector3(spawnPoint.transform.position.x, systemDepth, spawnPoint.transform.position.z);

        }
        sunScale = star.transform.localScale.x;
        star.transform.position = new Vector3(star.transform.position.x, systemDepth, star.transform.position.z);
        star.transform.localScale = Vector3.one * sunScale * starScaleFactor;
    }
    public void HideSystem()
    {
        foreach (var spawnPoint in spawnPoints)
        {
            spawnPoint.transform.position = new Vector3(spawnPoint.transform.position.x, 0, spawnPoint.transform.position.z);

        }

        star.transform.position = new Vector3(star.transform.position.x, 0, star.transform.position.z);
        star.transform.localScale = Vector3.one * sunScale;
    }

}
