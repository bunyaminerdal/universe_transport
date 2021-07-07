using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SelectionBoxCommand : Command
{

    private Camera cameraMain;
    private Vector3 mouseStartPositon;
    private Vector3 mouseEndPosition;
    private bool isDragging;
    // private PlayerManager playerManager;
    // private PlayerUnitController[] playerUnits;

    private void Awake()
    {
        // playerManager = GetComponent<PlayerManager>();
        cameraMain = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
    }
    // public override void ExecuteWithVector2(Vector2 vector2, bool isMultiSelection)
    // {
    //     if (isDragging) return;
    //     if (!IsMouseOverUI())
    //     {
    //         isDragging = false;
    //         var camRay = cameraMain.ScreenPointToRay(vector2);
    //         RaycastHit hit;

    //         if (Physics.Raycast(camRay, out hit))
    //         {

    //             // hit.transform.TryGetComponent<GroundIneraction>(out GroundIneraction ground);

    //             // if (ground == null) return;
    //             mouseEndPosition = vector2;
    //             mouseStartPositon = vector2;
    //             isDragging = true;

    //         }

    //     }
    // }

    public override void EndWithVector2(Vector2 vector2, bool isMultiSelection)
    {
        // if (!isDragging) return;
        // playerUnits = PlayerUnitController.AllPlayerUnits.ToArray();
        // List<PlayerUnitController> playerUnitControllers = new List<PlayerUnitController>();
        // var viewportBounds = ScreenHelper.GetViewportBounds(cameraMain, mouseStartPositon, vector2);
        // //DeselectUnits();
        // foreach (var selectableObject in playerUnits)
        // {
        //     if (viewportBounds.Contains(cameraMain.WorldToViewportPoint(selectableObject.transform.position)))
        //     {
        //         playerUnitControllers.Add(selectableObject.GetComponent<PlayerUnitController>());
        //     }
        // }
        // if (!isMultiSelection) playerManager.DeselectUnits();
        // if (!isMultiSelection && playerUnitControllers.Count <= 0) playerManager.DeselectInteractable();
        // if (playerUnitControllers.Count > 0) playerManager.SelectUnits(playerUnitControllers.ToArray(), isMultiSelection);
        // isDragging = false;

    }
    private void OnGUI()
    {
        if (!isDragging) return;

        // var rect = ScreenHelper.GetScreenRect(mouseStartPositon, mouseEndPosition);
        // ScreenHelper.DrawScreenRect(rect, new Color(0.8f, 0.8f, 0.95f, 0.1f));
        // ScreenHelper.DrawScreenRectBorder(rect, 1, Color.blue);

    }

    private bool IsMouseOverUI()
    {
        return EventSystem.current.IsPointerOverGameObject();
    }

    public override void DragWithVector2(Vector2 vector2)
    {
        if (!isDragging) return;
        mouseEndPosition = vector2;
    }

}
