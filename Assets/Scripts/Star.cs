using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Star : MonoBehaviour
{
    public StarType StarType;
}

public enum StarType
{
    DwarfStar,
    YellowStar,
    RedGiant,
    BlueGiant,
    SuperGiant
}