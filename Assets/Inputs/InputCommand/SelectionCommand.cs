using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SelectionCommand : Command
{
    PlayerManager playerManager;
    private Camera cameraMain;
    private void Awake()
    {
        cameraMain = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        playerManager = GetComponent<PlayerManager>();
    }
    public override void ExecuteWithVector2(Vector2 vector2)
    {
        if (!IsMouseOverUI())
        {
            var camRay = cameraMain.ScreenPointToRay(vector2);
            RaycastHit hit;
            //Shoot that ray and get the hit data
            if (Physics.Raycast(camRay, out hit))
            {
                hit.transform.TryGetComponent<SolarSystem>(out SolarSystem solar);
                if (solar != null)
                {
                    playerManager.SelectedSolarSystem = solar;
                }
                // hit.transform.TryGetComponent<Interactable>(out Interactable interact);
                // if (interact != null)
                // {
                //     playerManager.SelectInteractable(interact);
                // }
                //hit.transform.TryGetComponent<groundItem>(out groundItem groundItem);
                //Debug.Log(groundItem);
            }
        }
    }
    private bool IsMouseOverUI()
    {
        return EventSystem.current.IsPointerOverGameObject();
    }
}
