using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CargoStation : MonoBehaviour, IStation
{
    public int vehicleCapacity;
    public float cargoCapacity;
    public float cargoLoadSpeed;
    public SolarSystem solarSystem;

    private StationTypes stationType = StationTypes.cargo;
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
