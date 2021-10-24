using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RepairStation : MonoBehaviour, IStation
{
    private StationTypes stationType = StationTypes.Repair;
    public StationTypes StationType { get => stationType; set => stationType = value; }
    private string stationName;
    public string StationName { get => stationName; set => stationName = value; }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
