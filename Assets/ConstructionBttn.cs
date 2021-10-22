using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ConstructionBttn : MonoBehaviour
{
    public GameObject prefab;
    private void OnEnable()
    {
        GetComponent<Button>().onClick.AddListener(ConstructionBttnClicked);
    }
    private void OnDisable()
    {
        GetComponent<Button>().onClick.AddListener(ConstructionBttnClicked);
    }

    private void ConstructionBttnClicked()
    {
        UIEventHandler.ConstructionBegunEvent?.Invoke();
    }
}
