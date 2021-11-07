using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConstructionNodePos
{
    public Vector3 pos;
    public bool isOccupied;

    public ConstructionNodePos(Vector3 pos, bool isOccupied)
    {
        this.pos = pos;
        this.isOccupied = isOccupied;
    }
}
