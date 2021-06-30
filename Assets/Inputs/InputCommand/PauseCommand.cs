using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseCommand : Command
{
    float currentTime = 0;
    public override void Execute()
    {
        if (Time.deltaTime > 0)
        {
            currentTime = Time.timeScale;
            Time.timeScale = 0;

        }
        else
        {
            Time.timeScale = currentTime;
        }
        UIEventHandler.PauseTextClicked?.Invoke(Time.timeScale);
    }
}
