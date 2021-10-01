using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SolarPort : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    public SolarSystem solarSystemToConnect;

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button != PointerEventData.InputButton.Left) return;

        PlayerManagerEventHandler.SolarSelectionEvent?.Invoke(solarSystemToConnect);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        // Debug.Log(solarSystemToConnect.solarSystemName);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        // Debug.Log(solarSystemToConnect.solarSystemName);
    }
}
