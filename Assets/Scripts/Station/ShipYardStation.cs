using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipYardStation : MonoBehaviour, IStation, IConstructable
{
    private StationTypes stationType = StationTypes.shipyard;
    [SerializeField] private Sprite bttnTexture;
    public StationTypes StationType { get => stationType; set => stationType = value; }
    public Sprite BttnTexture { get => bttnTexture; set => bttnTexture = value; }
}
