using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConstructionController : MonoBehaviour
{
    private ConstructionNode activeConstructionNode;
    private GameObject activeGameObject;
    private void OnEnable()
    {
        UIEventHandler.ConstructionPrePlacementEvent.AddListener(ConstructionPrePlacement);
        UIEventHandler.ConstructionPlacementEvent.AddListener(ConstructionPlaced);
        UIEventHandler.ConstructionCancelEvent.AddListener(ConstructionCanceled);
    }
    private void OnDisable()
    {
        UIEventHandler.ConstructionPrePlacementEvent.RemoveListener(ConstructionPrePlacement);
        UIEventHandler.ConstructionPlacementEvent.RemoveListener(ConstructionPlaced);
        UIEventHandler.ConstructionCancelEvent.RemoveListener(ConstructionCanceled);
    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    public void ConstructionPrePlacement(ConstructionNode node)
    {
        if (activeConstructionNode != node)
        {
            if (activeGameObject) Destroy(activeGameObject);
            if (activeConstructionNode) activeConstructionNode.Canvas.SetActive(false);
            activeConstructionNode = node;
            activeGameObject = Instantiate(node.prefab, node.OwnerSolarSystem.transform);
            activeGameObject.transform.position = node.transform.position;
        }

    }
    private void ConstructionPlaced()
    {
        activeGameObject.GetComponent<IConstructable>().Place(activeConstructionNode.OwnerSolarSystem);
        activeConstructionNode.OwnerSolarSystem.AddConstruction(activeGameObject, activeConstructionNode);
        activeGameObject = null;
        activeConstructionNode = null;
        UIEventHandler.ConstructionEndedEvent?.Invoke();
    }

    private void ConstructionCanceled()
    {
        if (activeConstructionNode) activeConstructionNode.Canvas.SetActive(false);
        activeConstructionNode = null;
        if (activeGameObject) Destroy(activeGameObject);
    }
}
