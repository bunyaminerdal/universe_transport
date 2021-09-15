using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SolarPort : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    public SolarSystem solarSystemToConnect;

    public void OnPointerClick(PointerEventData eventData)
    {
        PlayerManagerEventHandler.SolarSelection?.Invoke(solarSystemToConnect);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        Debug.Log(solarSystemToConnect.name);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        Debug.Log(solarSystemToConnect.name);
    }
}
