using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class City : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private GameObject selectionBox;
    public string cityName;
    public SolarSystem ownerSolarSystem;
    public int population;
    public Item[] needs;
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
        infoTexts.Add("<align=center>CITY</align>");
        infoTexts.Add("Name: " + cityName);
        infoTexts.Add("Solar System: " + ownerSolarSystem.solarSystemName);
        infoTexts.Add("Population: " + population);
        string needs1 = "";
        foreach (var item in needs)
        {
            needs1 += item.itemName + ", ";
        }
        infoTexts.Add("Needs: " + needs1);
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

