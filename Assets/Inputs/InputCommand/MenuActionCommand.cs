using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MenuActionCommand : Command
{
    float timeScaleNow1;
    // MenuType openedMenu;
    PlayerInputActions playerInputActions;

    private void Awake()
    {
        // openedMenu = MenuType.GameMenu;
        playerInputActions = new PlayerInputActions();
    }
    private void OnEnable()
    {
        playerInputActions.InMenu.ESC.performed += InMenuEscPerformed;
        // MenuEventHandler.ResumeButtonClicked.AddListener(Resume);
        // MenuEventHandler.CurrentMenuChanged.AddListener(CurrentMenuChanged);
    }

    private void InMenuEscPerformed(InputAction.CallbackContext context)
    {
        // switch (openedMenu)
        // {
        //     case MenuType.Closed:
        //         break;
        //     case MenuType.GameMenu:
        //         MenuEventHandler.ResumeButtonClicked?.Invoke();
        //         break;
        //     case MenuType.LoadMenu:
        //         openedMenu = MenuType.GameMenu;
        //         MenuEventHandler.GameMenuClicked?.Invoke(openedMenu);
        //         break;
        //     case MenuType.SaveMenu:
        //         openedMenu = MenuType.GameMenu;
        //         MenuEventHandler.GameMenuClicked?.Invoke(openedMenu);
        //         break;
        //     case MenuType.OptionsMenu:
        //         openedMenu = MenuType.GameMenu;
        //         MenuEventHandler.GameMenuClicked?.Invoke(openedMenu);
        //         break;
        //     default:
        //         break;
        // }

    }

    private void Resume()
    {
        // openedMenu = MenuType.Closed;
        Time.timeScale = timeScaleNow1;
        playerInputActions.InMenu.Disable();
        // MenuEventHandler.GameMenuClicked?.Invoke(openedMenu);
    }
    // private void CurrentMenuChanged(MenuType menuType)
    // {
    //     openedMenu = menuType;
    // }

    private void OnDisable()
    {
        playerInputActions.InMenu.ESC.performed -= InMenuEscPerformed;
        // MenuEventHandler.ResumeButtonClicked.RemoveListener(Resume);
        // MenuEventHandler.CurrentMenuChanged.RemoveListener(CurrentMenuChanged);
    }
    public override void Execute()
    {
        // openedMenu = MenuType.GameMenu;
        timeScaleNow1 = Time.timeScale;
        Time.timeScale = 0;
        playerInputActions.InMenu.Enable();
        // MenuEventHandler.GameMenuClicked?.Invoke(openedMenu);

    }
}
