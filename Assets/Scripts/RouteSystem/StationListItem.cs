using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class StationListItem : MonoBehaviour
{
    [SerializeField] private TMP_Text stationName;
    public SolarSystem solarSystem;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    //TODO: this will be cargo station
    public void UpdateDisplay(SolarSystem solar)
    {
        stationName.text = solar.solarSystemName;
        solarSystem = solar;
    }
}
