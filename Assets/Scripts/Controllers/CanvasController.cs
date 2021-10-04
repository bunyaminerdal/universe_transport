
using UnityEngine;
using TMPro;
using System.Globalization;
using System.Collections;
using UnityEngine.UI;

public class CanvasController : MonoBehaviour
{
    [SerializeField] private GameObject pauseText;
    [SerializeField] private GameObject creatingUniverse;
    [SerializeField] private Toggle pauseToggle;
    [SerializeField] private Toggle playToggle;
    [SerializeField] private Toggle x2Toggle;
    [SerializeField] private Toggle x4Toggle;
    [Header("Menus")]
    [SerializeField] private GameObject solarRouteMenu;

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
    [Header("Left menu")]
    [SerializeField]
    private GameObject leftBottomMenu;
    private Button[] leftMenuItems;


    private bool isPaused;

    private void OnEnable()
    {
        UIEventHandler.PauseTextClicked.AddListener(PauseTextClicked);
        UIEventHandler.YearChanged.AddListener(OnYearChanged);
        UIEventHandler.MonthChanged.AddListener(OnMonthChanged);
        UIEventHandler.DayChanged.AddListener(OnDayChanged);
        PlayerManagerEventHandler.MapChangeEvent.AddListener(MapChanged);
        UIEventHandler.CreatingUniverse.AddListener(CreatingUniverse);
    }
    void Start()
    {
        rightMenuItems = rightBottomMenu.GetComponentsInChildren<Button>();
        leftMenuItems = leftBottomMenu.GetComponentsInChildren<Button>();
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
        foreach (var item in leftMenuItems)
        {
            item.gameObject.SetActive(!isOpened);
        }
    }
    public void SolarRouteMenu()
    {
        if (solarRouteMenu.activeSelf)
        {
            solarRouteMenu.SetActive(false);
        }
        else
        {
            solarRouteMenu.SetActive(true);
        }
    }
    private void OnDisable()
    {
        UIEventHandler.PauseTextClicked.RemoveListener(PauseTextClicked);
        UIEventHandler.YearChanged.RemoveListener(OnYearChanged);
        UIEventHandler.MonthChanged.RemoveListener(OnMonthChanged);
        UIEventHandler.DayChanged.RemoveListener(OnDayChanged);
        PlayerManagerEventHandler.MapChangeEvent.RemoveListener(MapChanged);
        UIEventHandler.CreatingUniverse.RemoveListener(CreatingUniverse);
    }
    private void CreatingUniverse(bool isCreating)
    {
        creatingUniverse.SetActive(isCreating);
    }
    private void PauseTextClicked(float time)
    {
        switch (time)
        {
            case 0:
                isPaused = true;
                pauseText.SetActive(true);
                pauseText.GetComponent<TMP_Text>().text = "Game Paused...";
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
                pauseText.SetActive(true);
                pauseText.GetComponent<TMP_Text>().text = "Game Paused...";
                pauseToggle.isOn = true;
                break;
        }
    }
    private IEnumerator WaitAndPrint(float waitTime, string text)
    {
        pauseText.SetActive(true);
        pauseText.GetComponent<TMP_Text>().text = text;
        yield return new WaitForSecondsRealtime(waitTime);
        if (!isPaused) pauseText.SetActive(false);
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
