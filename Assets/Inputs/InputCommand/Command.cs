using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Command : MonoBehaviour
{
    public virtual void Execute()
    {

    }

    public virtual void ExecuteWithBool(bool value)
    {

    }
    public virtual void ExecuteWithFloat(float value)
    {

    }
    public virtual void ExecuteWithVector2(Vector2 vector2)
    {

    }
    public virtual void ExecuteWithVector3(Vector3 vector3)
    {

    }

    public virtual void EndWithVector2(Vector2 vector2, bool isMultiSelection = false)
    {

    }
    public virtual void DragWithVector2(Vector2 vector2)
    {

    }
}
