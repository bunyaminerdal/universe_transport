using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


public class ConstructionNode : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private Sprite filledCircle;
    [SerializeField] private Sprite outlinedCircle;
    private SpriteRenderer spriteRenderer;
    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

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
        Debug.Log("click");
    }
}
