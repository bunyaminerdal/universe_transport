using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public static class UIEventHandler
{
    public static UnityEvent<float> PauseTextClicked = new UnityEvent<float>();
    public static UnityEvent<Vector2> PauseButtonClicked = new UnityEvent<Vector2>();
    public static UnityEvent<bool> CreatingUniverse = new UnityEvent<bool>();
    public static UnityEvent<int> DayChanged = new UnityEvent<int>();
    public static UnityEvent<int> MonthChanged = new UnityEvent<int>();
    public static UnityEvent<int> YearChanged = new UnityEvent<int>();
    public static UnityEvent<Route, bool> SingleRouteItemClickedEvent = new UnityEvent<Route, bool>();
    public static UnityEvent<Route> StationListItemCreateEvent = new UnityEvent<Route>();
}
