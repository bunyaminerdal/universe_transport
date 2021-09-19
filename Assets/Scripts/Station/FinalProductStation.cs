using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class FinalProductStation : MonoBehaviour, IStation, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField]
    private GameObject selectionBox;
    public StationTypes stationType = StationTypes.finalProduct;
    public string stationName;
    public Item Product;
    public SolarSystem ownerSolarSystem;
    private List<string> infoTexts = new List<string>();
    private TooltipController tooltipController;
    private GameObject selection;
    private void Awake()
    {
        tooltipController = FindObjectOfType<TooltipController>();
    }
    private void CreateInfo()
    {
        infoTexts.Clear();
        infoTexts.Add("<align=center>STATION</align>");
        infoTexts.Add("Name: " + stationName);
        infoTexts.Add("Station Type: " + stationType.ToString());
        infoTexts.Add("Solar System: " + ownerSolarSystem.solarSystemName);
        infoTexts.Add("Product: " + Product.itemName);
        string needs = "";
        foreach (var item in Product.inputs)
        {
            needs += item.itemName + ", ";
        }
        infoTexts.Add("Needs: " + needs);
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        CreateInfo();
        selection = Instantiate(selectionBox, transform);
        tooltipController.DisplayInfo(eventData.position, infoTexts);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        Destroy(selection);
        tooltipController.HideInfo();
    }

}
