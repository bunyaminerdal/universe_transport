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
    private Vector2 moveDirection;


    //private Vector3 moveDirection = Vector3.zero;
    private Coroutine movementCoroutine;
    private Vector3 newPosition;
    private float movementSpeedModifier = 1f;

    private void Awake()
    {

    }
    private void OnEnable()
    {
        // SaveLoadHandlers.PlayerManagerTransformLoad.AddListener(PlayerManagerTransformLoad);
        PlayerManagerEventHandler.BoundaryChangeEvent.AddListener(boundryChange);
        PlayerManagerEventHandler.MovementModifierEvent.AddListener(movementModifier);
        currentMovementSpeed = movementSpeed;
    }
    private void Start()
    {
        boundryLimitCluster = (StaticVariablesStorage.solarClusterDistance * StaticVariablesStorage.solarClusterCircleCount) + StaticVariablesStorage.solarSystemDistance;
        //boundry limit needs to be automated
        boundryLimitSolar = StaticVariablesStorage.solarSystemDistance / 20f;
        boundryLimit = boundryLimitCluster;
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
        PlayerManagerEventHandler.BoundaryChangeEvent.RemoveListener(boundryChange);
        PlayerManagerEventHandler.MovementModifierEvent.RemoveListener(movementModifier);
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

    public override void ExecuteWithVector2(Vector2 vector2)
    {
        moveDirection = vector2;
    }
    private void Update()
    {
        if (moveDirection != Vector2.zero)
        {
            newPosition = Vector3.zero;
            if (moveDirection.y > 0)
            {
                newPosition += (transform.forward * (movementSpeedModifier + currentMovementSpeed) * Time.deltaTime);
            }
            if (moveDirection.y < 0)
            {
                newPosition -= (transform.forward * (movementSpeedModifier + currentMovementSpeed) * Time.deltaTime);
            }
            if (moveDirection.x > 0)
            {
                newPosition += (transform.right * (movementSpeedModifier + currentMovementSpeed) * Time.deltaTime);
            }
            if (moveDirection.x < 0)
            {
                newPosition -= (transform.right * (movementSpeedModifier + currentMovementSpeed) * Time.deltaTime);
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


        }
    }



}
