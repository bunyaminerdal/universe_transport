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
        TimeChanger(vector2);
        UIEventHandler.PauseTextClicked?.Invoke(TimeController.TIMESCALE);
    }

    private void TimeChanger(Vector2 vector2)
    {
        if (vector2 == Vector2.up)
        {
            if (TimeController.TIMESCALE > 0)
            {
                currentTimeScale = TimeController.TIMESCALE;
                TimeController.TIMESCALE = 0;
            }
            else
            {
                TimeController.TIMESCALE = currentTimeScale;
            }
        }
        else if (vector2 == Vector2.down)
        {
            TimeController.TIMESCALE = 1;
        }
        else if (vector2 == Vector2.left)
        {
            TimeController.TIMESCALE = 2;
        }
        else if (vector2 == Vector2.right)
        {
            TimeController.TIMESCALE = 4;
        }
    }

    private void buttonClicked(Vector2 vector2)
    {
        TimeChanger(vector2);
    }
}