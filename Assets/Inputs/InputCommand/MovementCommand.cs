using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementCommand : Command
{
    // animasyonu da burada yapabilirim. sadece y�r�me
    [SerializeField]
    private float movementSpeed;
    private float boundryLimitCluster;
    private float boundryLimitSolar;

    private IMovementInput _move;

    //private Vector3 moveDirection = Vector3.zero;
    private Coroutine movementCoroutune;
    private Vector3 newPosition;

    private void Awake()
    {
        _move = GetComponent<IMovementInput>();

    }
    private void OnEnable()
    {
        // SaveLoadHandlers.PlayerManagerTransformLoad.AddListener(PlayerManagerTransformLoad);
        PlayerManagerEventHandler.BoundryCreateEvent.AddListener(boundryCreate);
    }
    private void OnDisable()
    {
        // SaveLoadHandlers.PlayerManagerTransformLoad.RemoveListener(PlayerManagerTransformLoad);
        PlayerManagerEventHandler.BoundryCreateEvent.RemoveListener(boundryCreate);
    }
    private void boundryCreate(float _boundryLimitCluster, float _boundryLimitSolar)
    {
        boundryLimitCluster = _boundryLimitCluster;
        boundryLimitSolar = _boundryLimitSolar;
    }

    private void PlayerManagerTransformLoad(float arg0, float arg1, float arg2)
    {
        transform.position = new Vector3(arg0, arg1, arg2);
    }

    public override void ExecuteWithVector3(Vector3 vector3)
    {
        newPosition = Vector3.zero;
        if (movementCoroutune == null) movementCoroutune = StartCoroutine(Move());
        // SaveLoadHandlers.PlayerManagerTransform?.Invoke(transform.position.x, transform.position.y, transform.position.z);
    }

    private IEnumerator Move()
    {

        while (_move.moveDirection != Vector3.zero)
        {
            newPosition = Vector3.zero;
            if (_move.moveDirection.z > 0)
            {
                newPosition += (transform.forward / movementSpeed);
            }
            if (_move.moveDirection.z < 0)
            {
                newPosition -= (transform.forward / movementSpeed);
            }
            if (_move.moveDirection.x > 0)
            {
                newPosition += (transform.right / movementSpeed);
            }
            if (_move.moveDirection.x < 0)
            {
                newPosition -= (transform.right / movementSpeed);
            }
            transform.position += newPosition;

            yield return null;
        }

        movementCoroutune = null;
    }

}
