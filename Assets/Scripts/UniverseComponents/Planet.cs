using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Planet : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField]
    private GameObject selectionBox;
    public string planetName;
    public PlanetType planetType;
    public Orbit orbit;
    public Moon[] moons;
    public SolarSystem ownerSolarSystem;
    public Item Item;
    public int planetIndex;

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
        infoTexts.Add("<align=center>PLANET</align>");
        infoTexts.Add("Name: " + planetName);
        infoTexts.Add("Solar System: " + ownerSolarSystem.solarSystemName);
        if (planetType != PlanetType.NullPlanet)
        {
            infoTexts.Add("Planet Type: " + planetType.ToString());
            infoTexts.Add("material: " + Item.itemName);
        }
        else
        {
            infoTexts.Add("Planet has no useful material!");
        }

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

public enum PlanetType
{
    NullPlanet,
    GasPlanet,
    OrganicPlanet,
    MetalPlanet,
    MineralPlanet
}