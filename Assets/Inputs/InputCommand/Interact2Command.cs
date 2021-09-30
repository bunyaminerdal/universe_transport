using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Interact2Command : Command
{
    // private PlayerManager playerManager;
    //private Camera cameraMain;
    private void Awake()
    {
        //cameraMain = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        // playerManager = GetComponent<PlayerManager>();
    }
    public override void Execute()
    {
        PlayerManagerEventHandler.RouteCreateInteraction?.Invoke();
    }

    private bool IsMouseOverUI()
    {
        return EventSystem.current.IsPointerOverGameObject();
    }

}
