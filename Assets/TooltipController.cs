using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.InputSystem;

public class TooltipController : MonoBehaviour
{
    [SerializeField] private GameObject tooltipcontroller;
    [SerializeField] private RectTransform tooltipcanvasTransform;
    [SerializeField] private TextMeshProUGUI infoText;
    [SerializeField] private Vector3 offset;
    [SerializeField] private float padding; //screen edge padding

    private Canvas tooltipCanvas;
    private Vector3 mouseposition;
    private Vector3 playerposition;
    private void Awake()
    {
        tooltipCanvas = tooltipcontroller.GetComponent<Canvas>();
        mouseposition = Vector3.zero;

    }
    private void Update()
    {

    }

    private void FollowCursor(Vector3 position)
    {
        if (!tooltipcontroller.activeSelf) return;
        offset.x = tooltipcanvasTransform.rect.width / 2;
        Vector3 newPos = position + offset;
        newPos.z = 0f;
        float rightEdgeToScreenEdgeDistance = Screen.width - (newPos.x + tooltipcanvasTransform.rect.width * tooltipCanvas.scaleFactor / 2) - padding;
        if (rightEdgeToScreenEdgeDistance < 0)
        {
            newPos.x += rightEdgeToScreenEdgeDistance;
        }
        float leftEdgeToScreenEdgeDistance = 0 - (newPos.x - tooltipcanvasTransform.rect.width * tooltipCanvas.scaleFactor / 2) + padding;
        if (leftEdgeToScreenEdgeDistance > 0)
        {
            newPos.x += leftEdgeToScreenEdgeDistance;
        }
        float topEdgeToScreenEdgeDistance = Screen.height - (newPos.y + tooltipcanvasTransform.rect.height * tooltipCanvas.scaleFactor / 2) - padding;
        if (topEdgeToScreenEdgeDistance < 0)
        {
            newPos.y += topEdgeToScreenEdgeDistance;
        }
        tooltipcanvasTransform.transform.position = newPos;
    }

    public void DisplayInfo(Vector3 position)
    {
        infoText.text = "asdfasdfsadf";
        tooltipcontroller.SetActive(true);
        LayoutRebuilder.ForceRebuildLayoutImmediate(tooltipcanvasTransform);
        FollowCursor(position);
    }
    public void HideInfo()
    {
        tooltipcontroller.SetActive(false);
    }
}
