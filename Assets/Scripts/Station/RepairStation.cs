using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RepairStation : MonoBehaviour, IStation
{
    private StationTypes stationType = StationTypes.repair;
    public StationTypes StationType { get => stationType; set => stationType = value; }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
