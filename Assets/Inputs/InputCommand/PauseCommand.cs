using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseCommand : Command
{
    float currentTimeScale = 0;

    public override void ExecuteWithVector2(Vector2 vector2)
    {
        if (vector2 == Vector2.up)
        {
            if (Time.deltaTime > 0)
            {
                currentTimeScale = Time.timeScale;
                Time.timeScale = 0;

            }
            else
            {
                Time.timeScale = currentTimeScale;
            }
        }
        else if (vector2 == Vector2.down)
        {
            Time.timeScale = 1;
        }
        else if (vector2 == Vector2.left)
        {
            Time.timeScale = 2;
        }
        else if (vector2 == Vector2.right)
        {
            Time.timeScale = 4;
        }
        UIEventHandler.PauseTextClicked?.Invoke(Time.timeScale);
    }
}