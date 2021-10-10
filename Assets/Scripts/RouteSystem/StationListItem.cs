using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class StationListItem : MonoBehaviour
{
    [SerializeField] private TMP_Text stationName;
    public SolarSystem solarSystem;
    public int index;
    [Header("Buttons")]
    [SerializeField] private Button deleteBttn;
    [SerializeField] private Button upBttn;
    [SerializeField] private Button downBttn;

    private void Awake()
    {
        ShowButtons(false);
    }
    private void OnEnable()
    {
        transform.GetComponent<Toggle>().onValueChanged.AddListener(clickedEvent);
        deleteBttn.onClick.AddListener(deleteBttnClicked);
        upBttn.onClick.AddListener(upButtonClicked);
        downBttn.onClick.AddListener(downButtonClicked);
    }

    private void OnDisable()
    {
        transform.GetComponent<Toggle>().onValueChanged.RemoveListener(clickedEvent);
        deleteBttn.onClick.RemoveListener(deleteBttnClicked);
        upBttn.onClick.RemoveListener(upButtonClicked);
        downBttn.onClick.RemoveListener(downButtonClicked);
    }

    //TODO: this will be cargo station
    public void UpdateDisplay(SolarSystem solar)
    {
        stationName.text = (index + 1).ToString() + " - " + solar.solarSystemName;
        solarSystem = solar;
    }
    private void clickedEvent(bool isOn)
    {
        ShowButtons(isOn);
    }

    private void ShowButtons(bool isOn)
    {
        deleteBttn.gameObject.SetActive(isOn);
        upBttn.gameObject.SetActive(isOn);
        downBttn.gameObject.SetActive(isOn);
    }
    private void deleteBttnClicked()
    {
        UIEventHandler.RouteStationDeleteEvent?.Invoke(index);
    }
    private void upButtonClicked()
    {
        UIEventHandler.RouteStationUpEvent?.Invoke(index);
    }
    private void downButtonClicked()
    {
        UIEventHandler.RouteStationDownEvent?.Invoke(index);
    }
}
