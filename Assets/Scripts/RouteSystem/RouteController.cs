using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RouteController : MonoBehaviour
{
    [SerializeField]
    private Route routePrefab;
    private void OnEnable()
    {
        for (int i = 0; i < 10; i++)
        {
            Instantiate(routePrefab, transform);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        //Debug.Log(Route.RouteList.Count);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
