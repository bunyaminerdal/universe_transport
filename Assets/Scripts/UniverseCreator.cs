using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UniverseCreator : MonoBehaviour
{
    [SerializeField]
    private GameObject solarSystemPrefab;
    // Start is called before the first frame update

    [SerializeField]
    private int solarSystemCount = 1;

    [SerializeField]
    private float solarSystemDistance = 50;

    public List<SolarSystem> solarSystems = new List<SolarSystem>();
    private void OnEnable()
    {
        for (int i = 0; i < solarSystemCount; i++)
        {
            var solarSystem = Instantiate(solarSystemPrefab);
            solarSystem.transform.parent = transform;
            solarSystems.Add(solarSystem.GetComponent<SolarSystem>());
        }
        LocationCreator(Vector3.zero);
    }
    void Start()
    {

        for (int i = 0; i < solarSystemCount; i++)
        {
            solarSystems[i].GetComponent<PathSpawner>().CreatePaths();
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void LocationCreator(Vector3 destination)
    {

        List<Vector3> targetPositionList = GetPositionListAround(destination, new float[] { solarSystemDistance, solarSystemDistance * 2, solarSystemDistance * 3, solarSystemDistance * 4, solarSystemDistance * 5 }, new int[] { 5, 10, 15, 20, 25 });
        List<Vector3> arrangedTargetPositionList = new List<Vector3>();
        for (int i = 0; i < solarSystemCount; i++)
        {
            arrangedTargetPositionList.Add(targetPositionList[i]);
        }
        arrangedTargetPositionList.Reverse();
        var targetPositionListIndex = 0;
        foreach (var selectableObj in solarSystems)
        {

            selectableObj.transform.position = arrangedTargetPositionList[targetPositionListIndex];
            targetPositionListIndex = (targetPositionListIndex + 1) % arrangedTargetPositionList.Count;
        }

    }

    private List<Vector3> GetPositionListAround(Vector3 startPosition, float[] ringDistanceArray, int[] ringPositionCountArray)
    {
        List<Vector3> positionList = new List<Vector3>();
        positionList.Add(startPosition);
        for (int i = 0; i < ringDistanceArray.Length; i++)
        {

            positionList.AddRange(GetPositionListAround(startPosition, ringDistanceArray[i], ringPositionCountArray[i]));
        }
        return positionList;
    }
    private List<Vector3> GetPositionListAround(Vector3 startPosition, float distance, int positionCount)
    {
        List<Vector3> positionList = new List<Vector3>();
        for (int i = 0; i < positionCount; i++)
        {
            float angle = i * (360 / positionCount);
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
