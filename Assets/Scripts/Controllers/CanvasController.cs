
using UnityEngine;
using TMPro;
using System.Globalization;
using System.Collections;
using UnityEngine.UI;

public class CanvasController : MonoBehaviour
{
    [SerializeField]
    private TMP_Text pauseText;
    [SerializeField]
    private Toggle pauseToggle;
    [SerializeField]
    private Toggle playToggle;
    [SerializeField]
    private Toggle x2Toggle;
    [SerializeField]
    private Toggle x4Toggle;

    [Header("date time text")]
    [SerializeField]
    private TMP_Text yearText;
    [SerializeField]
    private TMP_Text monthText;
    [SerializeField]
    private TMP_Text dayText;
    [Header("Right menu")]
    [SerializeField]
    private GameObject rightBottomMenu;
    private Button[] rightMenuItems;


    private bool isPaused;

    private void OnEnable()
    {
        UIEventHandler.PauseTextClicked.AddListener(PauseTextClicked);
        UIEventHandler.YearChanged.AddListener(OnYearChanged);
        UIEventHandler.MonthChanged.AddListener(OnMonthChanged);
        UIEventHandler.DayChanged.AddListener(OnDayChanged);
        PlayerManagerEventHandler.MapChangeEvent.AddListener(MapChanged);
    }
    void Start()
    {
        rightMenuItems = rightBottomMenu.GetComponentsInChildren<Button>();
        RightMenuOpener(false);
    }
    private void MapChanged(bool isOpened)
    {
        RightMenuOpener(isOpened);
    }
    private void RightMenuOpener(bool isOpened)
    {
        foreach (var item in rightMenuItems)
        {
            item.gameObject.SetActive(isOpened);
        }
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
        PlayerManagerEventHandler.MapChangeEvent.RemoveListener(MapChanged);
    }
    private void PauseTextClicked(float time)
    {
        switch (time)
        {
            case 0:
                isPaused = true;
                pauseText.text = "Game Paused...";
                pauseText.enabled = true;
                pauseToggle.isOn = true;
                break;
            case 1:
                isPaused = false;
                StartCoroutine(WaitAndPrint(2f, "Game Speed x" + (int)time));
                playToggle.isOn = true;
                break;
            case 2:
                isPaused = false;
                StartCoroutine(WaitAndPrint(2f, "Game Speed x" + (int)time));
                x2Toggle.isOn = true;
                break;
            case 4:
                isPaused = false;
                StartCoroutine(WaitAndPrint(2f, "Game Speed x" + (int)time));
                x4Toggle.isOn = true;
                break;
            default:
                isPaused = true;
                pauseText.text = "Game Paused...";
                pauseText.enabled = true;
                pauseToggle.isOn = true;
                break;
        }
    }
    private IEnumerator WaitAndPrint(float waitTime, string text)
    {
        pauseText.text = text;
        pauseText.enabled = true;
        yield return new WaitForSecondsRealtime(waitTime);
        if (!isPaused) pauseText.enabled = false;

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
