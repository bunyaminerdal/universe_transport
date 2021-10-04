using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseCommand : Command
{
    float currentTimeScale = 0;
    private void OnEnable()
    {
        UIEventHandler.PauseButtonClicked.AddListener(buttonClicked);
    }
    private void OnDisable()
    {
        UIEventHandler.PauseButtonClicked.RemoveListener(buttonClicked);
    }

    public override void ExecuteWithVector2(Vector2 vector2)
    {
        mainMethod(vector2);
        UIEventHandler.PauseTextClicked?.Invoke(Time.timeScale);
    }

    private void mainMethod(Vector2 vector2)
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
    }

    private void buttonClicked(Vector2 vector2)
    {
        mainMethod(vector2);
    }
}