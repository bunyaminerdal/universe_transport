using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Collections;

public struct SolarSystemStruct
{
    public float solarDistance;
    public NativeArray<SolarSystemStruct> connectedSolars;

}
