using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationCommand : Command
{
    [SerializeField]
    private float rotationSpeed = 5;
    private bool isRotating;
    private Vector2 mouseStartPositon;
    private Vector2 mouseEndPosition;

    private void OnEnable()
    {
        // SaveLoadHandlers.PlayerManagerRotationYLoad.AddListener(PlayerManagerRotationYLoad);
    }
    private void OnDisable()
    {
        // SaveLoadHandlers.PlayerManagerRotationYLoad.RemoveListener(PlayerManagerRotationYLoad);
    }
    private void PlayerManagerRotationYLoad(float arg0)
    {
        transform.rotation = new Quaternion(transform.rotation.x, arg0, transform.rotation.z, transform.rotation.w);
    }

    public override void ExecuteWithVector2(Vector2 vector2)
    {
        if (isRotating) return;
        mouseEndPosition = vector2;
        mouseStartPositon = vector2;
        isRotating = true;

    }
    public override void EndWithVector2(Vector2 vector2, bool isMultiSelection)
    {
        isRotating = false;
        // SaveLoadHandlers.PlayerManagerRotationY?.Invoke(transform.rotation.y);
    }

    public override void DragWithVector2(Vector2 vector2)
    {
        if (!isRotating) return;
        mouseEndPosition = vector2;
        Vector3 difference = mouseStartPositon - mouseEndPosition;
        mouseStartPositon = mouseEndPosition;
        transform.rotation *= Quaternion.Euler(Vector3.up * (-difference.x / rotationSpeed));
    }
}
