using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbitLineRenderer : MonoBehaviour
{
    public LineRenderer lineRenderer;
    public static List<OrbitLineRenderer> OrbitLineRendererList = new List<OrbitLineRenderer>();
    private void Awake()
    {
        OrbitLineRendererList.Add(this);
    }
    private void OnEnable()
    {
        lineRenderer = GetComponent<LineRenderer>();
    }
}
