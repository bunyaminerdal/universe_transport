using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementCommand : Command
{
    // animasyonu da burada yapabilirim. sadece y�r�me
    [SerializeField]
    private float movementSpeed;
    [SerializeField]
    private float movementSpeed1;
    private float currentMovementSpeed;
    private float boundryLimitCluster;
    private float boundryLimitSolar;
    private Vector3 transformLocalPosition;
    private float boundryLimit;

    private IMovementInput _move;

    //private Vector3 moveDirection = Vector3.zero;
    private Coroutine movementCoroutine;
    private Vector3 newPosition;
    private float movementSpeedModifier = 1f;

    private void Awake()
    {
        _move = GetComponent<IMovementInput>();

    }
    private void OnEnable()
    {
        // SaveLoadHandlers.PlayerManagerTransformLoad.AddListener(PlayerManagerTransformLoad);
        PlayerManagerEventHandler.BoundaryCreateEvent.AddListener(boundryCreate);
        PlayerManagerEventHandler.BoundaryChangeEvent.AddListener(boundryChange);
        PlayerManagerEventHandler.MovementModifier.AddListener(movementModifier);
        currentMovementSpeed = movementSpeed;
    }

    private void movementModifier(float zoomAmount)
    {
        zoomAmount *= 0.001f;
        movementSpeedModifier = zoomAmount;
        //Debug.Log(movementSpeedModifier);
    }

    private void OnDisable()
    {
        // SaveLoadHandlers.PlayerManagerTransformLoad.RemoveListener(PlayerManagerTransformLoad);
        PlayerManagerEventHandler.BoundaryCreateEvent.RemoveListener(boundryCreate);
        PlayerManagerEventHandler.BoundaryChangeEvent.RemoveListener(boundryChange);
        PlayerManagerEventHandler.MovementModifier.RemoveListener(movementModifier);
    }
    private void boundryCreate(float _boundryLimitCluster, float _boundryLimitSolar)
    {
        boundryLimitCluster = _boundryLimitCluster;
        boundryLimitSolar = _boundryLimitSolar;
        boundryLimit = _boundryLimitCluster;
    }
    private void boundryChange(bool isSolarMapOpened)
    {
        if (isSolarMapOpened)
        {
            boundryLimit = boundryLimitSolar;
            transformLocalPosition = transform.position;
            currentMovementSpeed = movementSpeed1;

        }
        else
        {
            boundryLimit = boundryLimitCluster;
            currentMovementSpeed = movementSpeed;
        }
    }

    private void PlayerManagerTransformLoad(float arg0, float arg1, float arg2)
    {
        transform.position = new Vector3(arg0, arg1, arg2);
    }

    public override void ExecuteWithVector3(Vector3 vector3)
    {
        newPosition = Vector3.zero;
        if (movementCoroutine == null) movementCoroutine = StartCoroutine(Move());
        // SaveLoadHandlers.PlayerManagerTransform?.Invoke(transform.position.x, transform.position.y, transform.position.z);
    }

    private IEnumerator Move()
    {

        while (_move.moveDirection != Vector3.zero)
        {
            newPosition = Vector3.zero;
            if (_move.moveDirection.z > 0)
            {
                newPosition += (transform.forward * (movementSpeedModifier + currentMovementSpeed));
            }
            if (_move.moveDirection.z < 0)
            {
                newPosition -= (transform.forward * (movementSpeedModifier + currentMovementSpeed));
            }
            if (_move.moveDirection.x > 0)
            {
                newPosition += (transform.right * (movementSpeedModifier + currentMovementSpeed));
            }
            if (_move.moveDirection.x < 0)
            {
                newPosition -= (transform.right * (movementSpeedModifier + currentMovementSpeed));
            }
            if (boundryLimit == boundryLimitCluster)
            {
                if (transform.position.x + newPosition.x < boundryLimit && transform.position.x + newPosition.x > -boundryLimit &&
                    transform.position.z + newPosition.z < boundryLimit && transform.position.z + newPosition.z > -boundryLimit)
                    transform.position += newPosition;
            }
            else
            {
                if (transform.position.x + newPosition.x < transformLocalPosition.x + boundryLimit && transform.position.x + newPosition.x > transformLocalPosition.x - boundryLimit &&
                transform.position.z + newPosition.z < transformLocalPosition.z + boundryLimit && transform.position.z + newPosition.z > transformLocalPosition.z - boundryLimit)
                    transform.position += newPosition;
            }

            yield return null;
        }

        movementCoroutine = null;
    }

}
