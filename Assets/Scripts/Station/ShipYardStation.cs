using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


public class ShipyardStation : MonoBehaviour, IStation, IConstructable, IPointerEnterHandler, IPointerExitHandler, IDestructable, IPointerClickHandler
{
    [SerializeField] private Sprite bttnTexture;
    [SerializeField] private GameObject selectionBox;
    public StationTypes StationType { get => stationType; set => stationType = value; }
    public Sprite BttnTexture { get => bttnTexture; set => bttnTexture = value; }
    private StationTypes stationType = StationTypes.Shipyard;
    private string stationName;
    public string StationName { get => stationName; set => stationName = value; }
    private bool isPlaced;
    private List<string> infoTexts = new List<string>();
    private TooltipController tooltipController;
    private GameObject selection;

    public SolarSystem OwnerSolarSystem;
    private void Awake()
    {
        tooltipController = FindObjectOfType<TooltipController>();
    }
    private void CreateInfo()
    {
        infoTexts.Clear();
        infoTexts.Add("<align=center>STATION</align>");
        infoTexts.Add("Name: " + stationName);
        infoTexts.Add("Station Type: " + StationType.ToString());
        infoTexts.Add("Solar System: " + OwnerSolarSystem.solarSystemName);

    }
    public void Place(SolarSystem solar)
    {
        OwnerSolarSystem = solar;
        stationName = "Shipyard - " + (solar.Shipyards.Count + 1).ToString(); ;
        isPlaced = true;
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        if (!isPlaced) return;
        CreateInfo();
        selection = Instantiate(selectionBox, transform);
        tooltipController.DisplayInfo(eventData.position, infoTexts);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (!isPlaced) return;
        Destroy(selection);
        tooltipController.HideInfo();
    }


    public void Destruct()
    {
        OwnerSolarSystem.Shipyards.Remove(this);
        OwnerSolarSystem.RemoveConstruction(transform.position);
        Destroy(gameObject, 0.1f);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        UIEventHandler.PreDestructionEvent?.Invoke(this, this);
    }
}
