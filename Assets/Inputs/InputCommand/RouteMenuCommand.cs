using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class RouteMenuCommand : Command
{

    public override void Execute()
    {
        UIEventHandler.RouteMenuOpenEvent?.Invoke();
    }



}
