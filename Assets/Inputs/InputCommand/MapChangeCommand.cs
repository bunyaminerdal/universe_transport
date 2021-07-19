using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapChangeCommand : Command
{
    PlayerManager playerManager;

    private void Awake()
    {
        playerManager = GetComponent<PlayerManager>();
    }
    public override void Execute()
    {
        playerManager.CloseSolarSystem();
    }
}
