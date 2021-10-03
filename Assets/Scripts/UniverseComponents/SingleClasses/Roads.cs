using System.Collections.Generic;
using UnityEngine;

public class Roads
{
    public Dictionary<Road, LineRenderer> roadsWGo;
    public List<Road> roads;
    public Roads()
    {
        this.roads = new List<Road>();
        this.roadsWGo = new Dictionary<Road, LineRenderer>();
    }
}
