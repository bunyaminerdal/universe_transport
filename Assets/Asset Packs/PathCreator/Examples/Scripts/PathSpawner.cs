using System.Collections;
using System.Collections.Generic;
using PathCreation;
using UnityEngine;

namespace PathCreation.Examples
{

    public class PathSpawner : MonoBehaviour
    {
        [SerializeField]
        private PathCreator pathPrefab;
        [SerializeField]
        private PathFollower planetPrefab;
        [SerializeField]
        private GameObject sunPrefab;

        private GameObject spawnPoint;
        private Transform[] spawnPoints;
        private float planetDistance = 2f;
        void Start()
        {

            int planetCount = Random.Range(3, 6);
            spawnPoints = new Transform[planetCount];
            Debug.Log(planetCount);
            var sun = Instantiate(sunPrefab, transform);
            for (int i = 1; i < planetCount + 1; i++)
            {
                spawnPoint = new GameObject();
                spawnPoint.transform.parent = transform;
                spawnPoint.transform.localScale = new Vector3(i * planetDistance, i * planetDistance, i * planetDistance);
                spawnPoints[i - 1] = spawnPoint.transform;
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

}