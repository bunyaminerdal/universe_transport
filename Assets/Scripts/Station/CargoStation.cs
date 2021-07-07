using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CargoStation : MonoBehaviour, IStation
{
    public StationTypes stationTypes = StationTypes.cargo;
    public int vehicleCapacity;
    public float cargoCapacity;
    public float cargoLoadSpeed;
    public SolarSystem solarSystem;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
