using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InteractCommand : Command
{
    // private PlayerManager playerManager;
    private Camera cameraMain;
    private void Awake()
    {
        cameraMain = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        // playerManager = GetComponent<PlayerManager>();


    }
    public override void ExecuteWithVector2(Vector2 vector2, bool isMultiSelection)
    {
        // if (!IsMouseOverUI())
        // {
        //     var camRay = cameraMain.ScreenPointToRay(vector2);
        //     RaycastHit hit;
        //     //Shoot that ray and get the hit data
        //     if (Physics.Raycast(camRay, out hit))
        //     {
        //         //Do something with that data herbiri i�in ayr� state e girecek
        //         if (hit.transform.TryGetComponent<EnemyUnitController>(out EnemyUnitController enemyUnit))
        //         {
        //             playerManager.SelectedEnemy(enemyUnit);
        //         }
        //         //Debug.Log(enemyUnit);

        //         //ekip arkadaşımızla nasıl interaction olacağı belli değil
        //         hit.transform.TryGetComponent<PlayerUnitController>(out PlayerUnitController playerUnit);
        //         //Debug.Log(playerUnit);
        //         if (hit.transform.TryGetComponent<Interactable>(out Interactable interact))
        //         {
        //             playerManager.SelectedInteractable(interact);
        //         }
        //         //Debug.Log(interact);
        //         if (hit.transform.TryGetComponent<GroundIneraction>(out GroundIneraction ground))
        //         {
        //             playerManager.MoveAction(hit.point);
        //         }
        //         //Debug.Log(ground);
        //         if (hit.transform.TryGetComponent<GroundItem>(out GroundItem groundItem))
        //         {
        //             playerManager.selectedGroundItem(groundItem);
        //         }
        //     }
        // }
    }
    private bool IsMouseOverUI()
    {
        return EventSystem.current.IsPointerOverGameObject();
    }

}
