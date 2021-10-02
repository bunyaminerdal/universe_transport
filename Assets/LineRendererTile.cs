using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LineRendererTile : MonoBehaviour
{
    LineRenderer line;


    private void Awake()
    {
        line = GetComponent<LineRenderer>();

    }
    // Start is called before the first frame update
    void Start()
    {
        // line.material.SetTextureScale("routelinefinal", new Vector2(line.transform.position.magnitude, 1));
        // line.material.mainTextureScale = new Vector2(line.transform.position.magnitude, 1);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
