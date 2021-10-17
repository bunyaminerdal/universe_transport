using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeController : MonoBehaviour
{
    [SerializeField]
    private int startYear;
    [SerializeField]
    private int startMonth;
    [SerializeField]
    private int startDay;
    [SerializeField]
    private float startHour;

    public static float TIMESCALE = 1f;

    private float _timeScale = 2;


    // Start is called before the first frame update
    void Start()
    {
        UIEventHandler.DayChanged?.Invoke(startDay);
        UIEventHandler.MonthChanged?.Invoke(startMonth);
        UIEventHandler.YearChanged?.Invoke(startYear);
    }

    // Update is called once per frame
    void Update()
    {
        if (TIMESCALE > 0)
        {
            startHour += Time.deltaTime * TIMESCALE * _timeScale;

            if (startHour > 23)
            {
                startHour = 0;
                startDay += 1;
                UIEventHandler.DayChanged?.Invoke(startDay);
            }
            if (startDay > DateTime.DaysInMonth(startYear, startMonth))
            {
                startDay = 1;
                UIEventHandler.DayChanged?.Invoke(startDay);
                startMonth += 1;
                UIEventHandler.MonthChanged?.Invoke(startMonth);
            }
            if (startMonth > 11)
            {
                startMonth = 1;
                UIEventHandler.MonthChanged?.Invoke(startMonth);
                startYear += 1;
                UIEventHandler.YearChanged?.Invoke(startYear);
            }
        }
    }

    private void setTime(int year,
                         int month,
                         int day
                         )
    {
        startYear = year;
        startMonth = month;
        startDay = day;
        UIEventHandler.DayChanged?.Invoke(startDay);
        UIEventHandler.MonthChanged?.Invoke(startMonth);
        UIEventHandler.YearChanged?.Invoke(startYear);
    }

}
