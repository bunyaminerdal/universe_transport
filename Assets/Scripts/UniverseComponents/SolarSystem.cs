using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SolarSystem : MonoBehaviour
{
    [SerializeField]
    private Planet PlanetPrefab;

    public string solarSystemName;
    public Planet[] planets;
    public Star star;
    public float solarDistance = float.MaxValue;
    public List<SolarSystem> connectedSolars;
    public List<IStation> stations;

    private GameObject spawnPoint;
    private Transform[] spawnPoints;
    private float planetDistance = 30f;

    public void CreateSystem()
    {
        int planetCount = Random.Range(3, 6);

        spawnPoints = new Transform[planetCount];
        planets = new Planet[planetCount];

        for (int i = 1; i < planetCount + 1; i++)
        {
            spawnPoint = new GameObject();
            spawnPoint.transform.position = transform.position;
            //spawnPoint.transform.localScale = new Vector3(i * planetDistance, i * planetDistance, i * planetDistance);
            spawnPoint.transform.rotation = new Quaternion(spawnPoint.transform.rotation.x, Random.rotation.y, spawnPoint.transform.rotation.z, spawnPoint.transform.rotation.w);
            spawnPoints[i - 1] = spawnPoint.transform;
            spawnPoint.transform.parent = transform;
            Planet planet = Instantiate(PlanetPrefab, spawnPoint.transform);
            planets[i - 1] = planet;
            planet.transform.localPosition = new Vector3(i * planetDistance, planet.transform.position.y, i * planetDistance);
            //planet.gameObject.SetActive(false);
        }


    }
    public void ShowSystem(float systemDepth)
    {
        foreach (var spawnPoint in spawnPoints)
        {
            spawnPoint.transform.position = new Vector3(spawnPoint.transform.position.x, systemDepth, spawnPoint.transform.position.z);

        }
        foreach (var planet in planets)
        {
            planet.gameObject.SetActive(true);
        }
        star.transform.position = new Vector3(star.transform.position.x, systemDepth, star.transform.position.z);
    }

}
