using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Vehicle", menuName = "Vehicle")]
public class VehicleScriptableObject : ScriptableObject
{
    public string VehicleName;
    public float VehicleSpeed;
    public int VehicleContainerCapacity;
    public float VehicleLifeTime;
    public float VehiclePrice;
    public float VehicleDailyCost;

}


