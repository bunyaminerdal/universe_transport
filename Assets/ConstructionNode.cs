using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


public class ConstructionNode : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    public static List<ConstructionNode> ConstructionNodes = new List<ConstructionNode>();
    [SerializeField] private Sprite filledCircle;
    [SerializeField] private Sprite outlinedCircle;
    [SerializeField] private Button PlaceBttn;
    [SerializeField] private Button CancelBttn;
    [SerializeField] private GameObject canvas;

    private SpriteRenderer spriteRenderer;

    public SolarSystem OwnerSolarSystem;

    public GameObject prefab;

    public GameObject Canvas { get => canvas; set => canvas = value; }

    private void Awake()
    {
        ConstructionNodes.Add(this);
        Canvas.SetActive(false);
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    private void OnEnable()
    {
        PlaceBttn.onClick.AddListener(PlaceBttnClicked);
        CancelBttn.onClick.AddListener(CancelBttnClicked);
    }
    private void OnDisable()
    {
        PlaceBttn.onClick.RemoveListener(PlaceBttnClicked);
        CancelBttn.onClick.RemoveListener(CancelBttnClicked);
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        spriteRenderer.sprite = filledCircle;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        spriteRenderer.sprite = outlinedCircle;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        Canvas.SetActive(true);
        UIEventHandler.ConstructionPrePlacementEvent?.Invoke(this);
    }

    private void PlaceBttnClicked()
    {
        canvas.SetActive(false);
        UIEventHandler.ConstructionPlacementEvent?.Invoke();

    }

    private void CancelBttnClicked()
    {
        UIEventHandler.ConstructionCancelEvent?.Invoke();

    }

    public void Deselect()
    {
        OwnerSolarSystem = null;
        spriteRenderer.sprite = outlinedCircle;
        gameObject.SetActive(false);
    }

    public static ConstructionNode SelectOne()
    {
        foreach (var node in ConstructionNodes)
        {
            if (node.OwnerSolarSystem == null)
            {
                return node;
            }
        }
        return null;
    }

}
