using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransportVehicle : MonoBehaviour
{
    public static List<TransportVehicle> transportVehicles;
    public VehicleScriptableObject vehicleScriptableObject;
    public Route route;
    public CargoLoadTypes cargoLoadTypes;
    private void Awake()
    {
        transportVehicles.Add(this);
    }

}

public enum CargoLoadTypes
{
    Any,
    Half,
    Full
}