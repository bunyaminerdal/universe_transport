using System.Collections;
using System.Collections.Generic;
using PathCreation;
using PathCreation.Examples;
using UnityEngine;



public class PathSpawner : MonoBehaviour
{
    [SerializeField]
    private PathCreator pathPrefab;
    [SerializeField]
    private PathFollower planetPrefab;

    private GameObject spawnPoint;
    private Transform[] spawnPoints;
    private float planetDistance = 2f;


    public void CreatePaths()
    {
        int planetCount = Random.Range(3, 6);

        spawnPoints = new Transform[planetCount];

        for (int i = 1; i < planetCount + 1; i++)
        {
            spawnPoint = new GameObject();
            spawnPoint.transform.position = transform.position;
            spawnPoint.transform.localScale = new Vector3(i * planetDistance, i * planetDistance, i * planetDistance);
            spawnPoint.transform.rotation = Random.rotation;
            spawnPoints[i - 1] = spawnPoint.transform;
            spawnPoint.transform.parent = transform;
        }

        foreach (Transform t in spawnPoints)
        {
            var path = Instantiate(pathPrefab, t.position, t.rotation);

            var follower = Instantiate(planetPrefab);
            follower.pathCreator = path;
            follower.speed = 1f / t.transform.localScale.x;
            path.transform.parent = t;
            follower.transform.parent = t;
        }
    }
}

