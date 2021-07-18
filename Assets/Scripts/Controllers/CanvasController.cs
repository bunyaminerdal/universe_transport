
using UnityEngine;
using TMPro;
using System.Globalization;

public class CanvasController : MonoBehaviour
{
    [SerializeField]
    private TMP_Text pauseText;

    [Header("date time text")]
    [SerializeField]
    private TMP_Text yearText;
    [SerializeField]
    private TMP_Text monthText;
    [SerializeField]
    private TMP_Text dayText;

    private void OnEnable()
    {
        UIEventHandler.PauseTextClicked.AddListener(PauseTextClicked);
        UIEventHandler.YearChanged.AddListener(OnYearChanged);
        UIEventHandler.MonthChanged.AddListener(OnMonthChanged);
        UIEventHandler.DayChanged.AddListener(OnDayChanged);
    }
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    private void OnDisable()
    {
        UIEventHandler.PauseTextClicked.RemoveListener(PauseTextClicked);
        UIEventHandler.YearChanged.RemoveListener(OnYearChanged);
        UIEventHandler.MonthChanged.RemoveListener(OnMonthChanged);
        UIEventHandler.DayChanged.RemoveListener(OnDayChanged);
    }
    private void PauseTextClicked(float time)
    {
        if (time == 0)
            pauseText.enabled = true;
        else
            pauseText.enabled = false;
    }

    private void OnYearChanged(int year)
    {
        yearText.text = year.ToString();
    }
    private void OnMonthChanged(int month)
    {
        monthText.text = CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(month);
    }
    private void OnDayChanged(int day)
    {
        dayText.text = day.ToString();
    }

}
