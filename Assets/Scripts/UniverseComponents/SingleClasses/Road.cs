using UnityEngine;

public class Road
{
    public SolarSystemStruct startSolar;
    public SolarSystemStruct endSolar;
    public LineRenderer lineRenderer;
    public Road()
    {

    }
    public Road(SolarSystemStruct _startSolar, SolarSystemStruct _endSolar)
    {
        this.startSolar = _startSolar;
        this.endSolar = _endSolar;
    }
}
