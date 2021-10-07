using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class NewInputManager : MonoBehaviour,
    // IInteractInput,  ISelectionBoxInput,
    //  IMenuActionInput, IQuickSaveInput,
    // IQuickLoadInput, IMultiSelectionInput,
    IZoomInput, IRotationInput, IPauseInput, IMovementInput, IMapChangeInput, IRouteMenuInput

{
    [SerializeField] private Command interactInputCommand;
    [SerializeField] private Command RouteMenuCommand;
    // [SerializeField]
    // private Command selectionBoxInputCommand;
    [SerializeField] private Command pauseInputCommand;
    // [SerializeField]
    // private Command menuActionCommand;
    // [SerializeField]
    // private Command quickSaveCommand;
    // [SerializeField]
    // private Command quickLoadCommand;
    [SerializeField] private Command movementCommand;
    [SerializeField] private Command zoomCommand;
    [SerializeField] private Command rotationCommand;
    [SerializeField] private Command mapChangeCommand;

    private PlayerInputActions playerInputActions;

    public bool IsPressingInteract { get; private set; }
    public bool isPressingSelectionBox { get; private set; }
    public bool isPressingPause { get; private set; }
    public bool isPressingMenuAction { get; private set; }
    public bool isPressingQuickSave { get; private set; }
    public bool isPressingQuickLoad { get; private set; }
    public bool isPressingMovement { get; private set; }
    public bool isPressingZoom { get; private set; }
    public bool isPressingRotation { get; private set; }
    public float rotationAmount { get; private set; }
    public bool isMultiSelection { get; private set; }

    public bool isPressingMapChangeAction { get; private set; }

    public bool IsRouteMenuOpened { get; private set; }

    private void Awake()
    {
        playerInputActions = new PlayerInputActions();
    }

    private void OnEnable()
    {
        playerInputActions.Enable();
        playerInputActions.Player.Interact.performed += Interact_performed;
        playerInputActions.Player.Interact2.performed += RouteMenuInput_performed;
        // playerInputActions.Player.SelectionBox.performed += SelectionBox_performed;
        playerInputActions.Player.PauseAction.performed += PauseAction_performed;
        // playerInputActions.Player.MenuAction.performed += MenuAction_performed;
        // playerInputActions.Player.QuickSaveAction.performed += QuickSaveAction_performed;
        // playerInputActions.Player.QuickLoadAction.performed += QuickLoadAction_performed;
        playerInputActions.Player.Movement.performed += Movement_performed;
        playerInputActions.Player.ZoomAction.performed += ZoomAction_performed;
        playerInputActions.Player.Rotation.performed += Rotation_performed;
        // playerInputActions.Player.MultiSelection.performed += MultiSelection_performed;
        playerInputActions.Player.MapChangeAction.performed += MapChangeAction_Performed;

        // MenuEventHandler.ResumeButtonClicked.AddListener(MenuClosedRequest);

    }



    private void OnDisable()
    {
        playerInputActions.Player.Interact.performed -= Interact_performed;
        playerInputActions.Player.Interact2.performed -= RouteMenuInput_performed;
        // playerInputActions.Player.SelectionBox.performed -= SelectionBox_performed;
        playerInputActions.Player.PauseAction.performed -= PauseAction_performed;
        // playerInputActions.Player.MenuAction.performed -= MenuAction_performed;
        // playerInputActions.Player.QuickSaveAction.performed -= QuickSaveAction_performed;
        // playerInputActions.Player.QuickLoadAction.performed -= QuickLoadAction_performed;
        playerInputActions.Player.Movement.performed -= Movement_performed;
        playerInputActions.Player.ZoomAction.performed -= ZoomAction_performed;
        playerInputActions.Player.Rotation.performed -= Rotation_performed;
        // playerInputActions.Player.MultiSelection.performed -= MultiSelection_performed;
        playerInputActions.Player.MapChangeAction.performed -= MapChangeAction_Performed;

        playerInputActions.Disable();

        // MenuEventHandler.ResumeButtonClicked.RemoveListener(MenuClosedRequest);
    }
    private void MapChangeAction_Performed(InputAction.CallbackContext context)
    {
        var value = context.ReadValue<float>();
        isPressingMapChangeAction = value >= 0.15;

        if (mapChangeCommand != null && isPressingMapChangeAction)
        {
            mapChangeCommand.Execute();
        }
    }
    private void MenuClosedRequest()
    {
        playerInputActions.Player.Enable();
        isPressingMenuAction = false;
    }
    private void Interact_performed(InputAction.CallbackContext context)
    {
        if (isPressingMenuAction) return;
        var value = context.ReadValue<float>();
        IsPressingInteract = value >= 0.15;
        if (interactInputCommand != null && IsPressingInteract)
        {
            interactInputCommand.Execute();
        }
    }
    private void RouteMenuInput_performed(InputAction.CallbackContext context)
    {
        if (isPressingMenuAction) return;
        var value = context.ReadValue<float>();
        IsRouteMenuOpened = value >= 0.15;
        if (RouteMenuCommand != null && IsRouteMenuOpened)
        {
            RouteMenuCommand.Execute();
        }
    }

    // private void SelectionBox_performed(InputAction.CallbackContext context)
    // {
    //     if (isPressingMenuAction) return;
    //     var value = context.ReadValue<float>();
    //     var position = playerInputActions.Player.MousePosition.ReadValue<Vector2>();
    //     isPressingSelectionBox = value >= 0.15;

    //     if (selectionBoxInputCommand != null && isPressingSelectionBox)
    //     {
    //         selectionBoxInputCommand.ExecuteWithVector2(position);
    //         //Debug.Log("selection box start:" + value);
    //         playerInputActions.Player.MousePosition.performed += MousePosition_performed;
    //     }
    //     else
    //     {
    //         playerInputActions.Player.MousePosition.performed -= MousePosition_performed;
    //         selectionBoxInputCommand.EndWithVector2(position, isMultiSelection);
    //         //Debug.Log("selection box end:" + value);
    //     }
    // }
    private void MousePosition_performed(InputAction.CallbackContext context)
    {
        // if (isPressingMenuAction) return;
        // if (selectionBoxInputCommand != null && isPressingSelectionBox)
        // {
        //     var value = context.ReadValue<Vector2>();
        //     selectionBoxInputCommand.DragWithVector2(value);
        // }
        if (rotationCommand != null && isPressingRotation)
        {
            var value = context.ReadValue<Vector2>();
            rotationCommand.DragWithVector2(value);
        }
    }
    private void PauseAction_performed(InputAction.CallbackContext context)
    {
        // if (isPressingMenuAction) return;
        // var value = context.ReadValue<float>();
        // isPressingPause = value >= 0.15;
        // if (pauseInputCommand != null && isPressingPause)
        // {
        //     pauseInputCommand.Execute();
        // }
        if (isPressingMenuAction) return;
        var _pauseDirection = context.ReadValue<Vector2>();
        isPressingPause = _pauseDirection != Vector2.zero;
        if (pauseInputCommand != null && isPressingPause)
        {
            pauseInputCommand.ExecuteWithVector2(_pauseDirection);
        }
    }
    // private void MenuAction_performed(InputAction.CallbackContext context)
    // {
    //     if (menuActionCommand != null)
    //     {
    //         playerInputActions.Player.Disable();
    //         menuActionCommand.Execute();
    //     }

    // }
    // private void QuickSaveAction_performed(InputAction.CallbackContext context)
    // {
    //     if (isPressingMenuAction) return;
    //     var value = context.ReadValue<float>();
    //     isPressingQuickSave = value >= 0.15;

    //     if (quickSaveCommand != null && isPressingQuickSave)
    //     {
    //         quickSaveCommand.Execute();
    //     }
    // }
    // private void QuickLoadAction_performed(InputAction.CallbackContext context)
    // {
    //     if (isPressingMenuAction) return;
    //     var value = context.ReadValue<float>();
    //     isPressingQuickLoad = value >= 0.15;

    //     if (quickLoadCommand != null && isPressingQuickLoad)
    //     {
    //         quickLoadCommand.Execute();
    //     }
    // }
    private void Movement_performed(InputAction.CallbackContext context)
    {
        if (isPressingMenuAction) return;
        var _moveDirection = context.ReadValue<Vector2>();
        isPressingMovement = _moveDirection != Vector2.zero;

        if (movementCommand != null)
        {
            movementCommand.ExecuteWithVector2(_moveDirection);
        }
    }
    private void ZoomAction_performed(InputAction.CallbackContext context)
    {
        if (isPressingMenuAction) return;
        var value = context.ReadValue<float>();
        isPressingZoom = value != 0;

        if (zoomCommand != null && isPressingZoom)
        {
            zoomCommand.ExecuteWithFloat(value);
        }
    }
    private void Rotation_performed(InputAction.CallbackContext context)
    {
        if (isPressingMenuAction) return;
        var _rotationAmount = context.ReadValue<float>();
        var position = playerInputActions.Player.MousePosition.ReadValue<Vector2>();
        isPressingRotation = _rotationAmount >= 0.15;
        if (rotationCommand != null && isPressingRotation)
        {
            rotationCommand.ExecuteWithVector2(position);
            playerInputActions.Player.MousePosition.performed += MousePosition_performed;
        }
        else
        {
            playerInputActions.Player.MousePosition.performed -= MousePosition_performed;
            rotationCommand.EndWithVector2(position);
        }
    }
    // private void MultiSelection_performed(InputAction.CallbackContext context)
    // {
    //     var value = context.ReadValue<float>();
    //     isMultiSelection = value >= 0.15;
    // }
}
