using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Orbit : MonoBehaviour
{
    [SerializeField] private int segments;
    [SerializeField] private int segmentsForConstructionNode;

    [SerializeField] OrbitLineRenderer linePrefab;

    private List<Vector3[]> linePosList = new List<Vector3[]>();

    void Start()
    {

    }
    public Vector3 RandomPlanetPos(float xradius, float yradius, int index)
    {
        float x = 0f;
        float y = 0f;
        float z = 0f;

        float angle = 20f;
        int localSegments = (int)(segments * index);
        Vector3 randomPlanetPos = Vector3.zero;
        int randomint = Random.Range(1, localSegments);

        for (int i = 0; i < (localSegments + 1); i++)
        {
            linePosList.Add(new Vector3[2]);
            x = Mathf.Sin(Mathf.Deg2Rad * angle) * xradius;
            z = Mathf.Cos(Mathf.Deg2Rad * angle) * yradius;
            angle += (360f / localSegments);
            linePosList[i][0] = new Vector3(x, 0, z);
            x = Mathf.Sin(Mathf.Deg2Rad * angle) * xradius;
            z = Mathf.Cos(Mathf.Deg2Rad * angle) * yradius;
            angle += (360f / localSegments);
            linePosList[i][1] = new Vector3(x, 0, z);
            angle -= (360f / localSegments);

            if (randomint == i)
            {
                randomPlanetPos = new Vector3(x, y, z);
            }
        }
        return randomPlanetPos;
    }
    public int ShowOrbitLines(int startIndex)
    {
        while (OrbitLineRenderer.OrbitLineRendererList.Count < startIndex + linePosList.Count + 1)
        {
            Instantiate(linePrefab, transform);
        }
        for (int i = 0; i < linePosList.Count; i++)
        {
            OrbitLineRenderer.OrbitLineRendererList[i + startIndex + 1].transform.SetParent(transform);
            OrbitLineRenderer.OrbitLineRendererList[i + startIndex + 1].transform.localPosition = Vector3.zero;
            OrbitLineRenderer.OrbitLineRendererList[i + startIndex + 1].transform.localRotation = Quaternion.identity;
            OrbitLineRenderer.OrbitLineRendererList[i + startIndex + 1].lineRenderer.SetPositions(linePosList[i]);
            OrbitLineRenderer.OrbitLineRendererList[i + startIndex + 1].gameObject.SetActive(true);
        }
        return startIndex + linePosList.Count + 1;
    }
    public void HideOrbitLines()
    {
        foreach (var line in OrbitLineRenderer.OrbitLineRendererList)
        {
            line.gameObject.SetActive(false);
        }
    }
    public List<Vector3> CreatePosibleConstructionNodes(float xradius, float yradius, int index)
    {
        float x = 0f;
        float y = 0f;
        float z = 0f;

        float angle = 20f;
        var nodeList = new List<Vector3>();
        for (int i = 0; i < (int)(segmentsForConstructionNode * index); i++)
        {
            x = Mathf.Sin(Mathf.Deg2Rad * angle) * xradius;
            z = Mathf.Cos(Mathf.Deg2Rad * angle) * yradius;
            angle += (360f / (int)(segmentsForConstructionNode * index));
            nodeList.Add(new Vector3(x, y, z));
        }
        return nodeList;
    }
}
