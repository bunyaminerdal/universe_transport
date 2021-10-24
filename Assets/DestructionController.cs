using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DestructionController : MonoBehaviour
{
    [SerializeField] private GameObject preDestruction;
    [SerializeField] private GameObject destruction;
    [SerializeField] private TMP_Text stationName;
    [SerializeField] private TMP_Text stationType;

    private void OnEnable()
    {
        UIEventHandler.PreDestructionEvent.AddListener(PreDestruct);
    }
    private void OnDisable()
    {
        UIEventHandler.PreDestructionEvent.RemoveListener(PreDestruct);
    }
    private IStation station;
    private IDestructable destructable;

    public void Destruct()
    {
        destructable.Destruct();
        MenuChanger(false);
    }
    public void Cancel()
    {
        MenuChanger(false);
        station = null;
    }

    private void MenuChanger(bool isActive)
    {
        preDestruction.SetActive(!isActive);
        destruction.SetActive(isActive);
    }

    private void PreDestruct(IStation _station, IDestructable _destructable)
    {
        station = _station;
        destructable = _destructable;
        MenuChanger(true);
        stationName.text = _station.StationName;
        stationType.text = _station.StationType.ToString();
    }
}
