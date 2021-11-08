using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CargoStation : MonoBehaviour,
 IStation, IConstructable, IPointerEnterHandler,
  IPointerExitHandler, IDestructable, IPointerClickHandler
{
    public int vehicleCapacity;
    public float cargoCapacity;
    public float cargoLoadSpeed;
    [SerializeField] private Sprite bttnTexture;
    [SerializeField] private GameObject selectionBox;
    public StationTypes StationType { get => stationType; set => stationType = value; }
    public Sprite BttnTexture { get => bttnTexture; set => bttnTexture = value; }
    public string StationName { get => stationName; set => stationName = value; }

    private StationTypes stationType = StationTypes.Cargo;
    private string stationName;
    private bool isPlaced;
    private List<string> infoTexts = new List<string>();
    private TooltipController tooltipController;
    private GameObject selection;

    public SolarSystem OwnerSolarSystem;
    public ConstructionNodePos nodePos;
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
    public void Place(ConstructionNode node)
    {
        OwnerSolarSystem = node.OwnerSolarSystem;
        nodePos = node.nodePos;
        stationName = "Cargo station - " + (node.OwnerSolarSystem.CargoStations.Count + 1).ToString(); ;
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
        OwnerSolarSystem.CargoStations.Remove(this);
        OwnerSolarSystem.RemoveConstruction(nodePos);
        Destroy(gameObject, 0.1f);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        UIEventHandler.PreDestructionEvent?.Invoke(this, this);
    }
}
