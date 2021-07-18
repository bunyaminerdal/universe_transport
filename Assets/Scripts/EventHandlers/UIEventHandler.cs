using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public static class UIEventHandler
{
    public static UnityEvent<float> PauseTextClicked = new UnityEvent<float>();
    public static UnityEvent<int> DayChanged = new UnityEvent<int>();
    public static UnityEvent<int> MonthChanged = new UnityEvent<int>();
    public static UnityEvent<int> YearChanged = new UnityEvent<int>();
}
