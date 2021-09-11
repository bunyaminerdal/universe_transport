// GENERATED AUTOMATICALLY FROM 'Assets/Inputs/InputBasics/PlayerInputActions.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class @PlayerInputActions : IInputActionCollection, IDisposable
{
    public InputActionAsset asset { get; }
    public @PlayerInputActions()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""PlayerInputActions"",
    ""maps"": [
        {
            ""name"": ""Player"",
            ""id"": ""e1265742-e8a1-4ef0-a279-57d2ecc423cd"",
            ""actions"": [
                {
                    ""name"": ""Movement"",
                    ""type"": ""PassThrough"",
                    ""id"": ""1b455362-be95-4f73-b83b-29a3004461c7"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Interact"",
                    ""type"": ""Button"",
                    ""id"": ""59ffc102-269a-491b-94e3-9fba28d5035b"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Selection"",
                    ""type"": ""Button"",
                    ""id"": ""e6ddb73f-9549-4a01-bd79-d116d98a89a8"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": ""Press""
                },
                {
                    ""name"": ""PauseAction"",
                    ""type"": ""Button"",
                    ""id"": ""97ba9c72-a97b-4c36-8221-1e9597fbb5d3"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": ""Press""
                },
                {
                    ""name"": ""MenuAction"",
                    ""type"": ""Button"",
                    ""id"": ""4ef4e33c-8281-4c2e-861a-7c3af1c866a9"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": ""Press""
                },
                {
                    ""name"": ""SelectionBox"",
                    ""type"": ""PassThrough"",
                    ""id"": ""aa091f07-dc8c-4203-a98e-ce09a8780c4a"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": ""Press(behavior=2)""
                },
                {
                    ""name"": ""ZoomAction"",
                    ""type"": ""PassThrough"",
                    ""id"": ""00d3766d-0346-4e0f-8d2a-f936b250707f"",
                    ""expectedControlType"": ""Axis"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""MousePosition"",
                    ""type"": ""Value"",
                    ""id"": ""16c23b3a-bd8a-4415-bf40-0087302b0dd3"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""QuickSaveAction"",
                    ""type"": ""Button"",
                    ""id"": ""2c517b7a-138f-4380-b532-b9d92fdb2390"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": ""Press""
                },
                {
                    ""name"": ""QuickLoadAction"",
                    ""type"": ""Button"",
                    ""id"": ""a2201026-a3c4-4447-a195-9dd21ece4003"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": ""Press""
                },
                {
                    ""name"": ""Rotation"",
                    ""type"": ""PassThrough"",
                    ""id"": ""96763cd7-ff85-4f7f-93fa-d94b7bb40aa3"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""MultiSelection"",
                    ""type"": ""Button"",
                    ""id"": ""5756cadd-e8e4-4cbc-92db-f2a9bf8de4f2"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": ""Press(behavior=2)""
                },
                {
                    ""name"": ""MapChangeAction"",
                    ""type"": ""Button"",
                    ""id"": ""cae50fdd-5553-4f7f-a7d4-f98171ae78d2"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": ""WASD"",
                    ""id"": ""2395c364-6cbb-4110-95f3-35a4ac86160f"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""37232bd3-78af-4413-91f1-5e30258fdd3b"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""8d711879-cc60-4f1d-8449-5e5e92906e29"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""becf0949-21b4-4573-bfe1-066aea2def4f"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""a35f60c7-543d-4109-a18a-37bb2f332134"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""ArrowKeys"",
                    ""id"": ""3d957eba-ddb8-41c3-87d1-5a59f2e22e53"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""ac00dfb4-6dd1-4ee9-990c-67c44a81f7cc"",
                    ""path"": ""<Keyboard>/upArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""a2c3f013-81a1-4c4a-9aba-2eccb54f037b"",
                    ""path"": ""<Keyboard>/downArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""0c25b93f-e1be-4156-8c80-599fea89c40a"",
                    ""path"": ""<Keyboard>/leftArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""caa04558-d04d-4478-bcdd-e51c0b87e079"",
                    ""path"": ""<Keyboard>/rightArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""b7387b0f-51f0-4200-837a-1de9b828af1f"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Selection"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""Space123"",
                    ""id"": ""db8ce1fe-d00f-472a-b6c3-53d200eef1cc"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""PauseAction"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""79901e36-4594-42f8-a7c5-50569035be77"",
                    ""path"": ""<Keyboard>/space"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""PauseAction"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""e3f48307-2ea8-44a8-b9dd-a5e154b7d5ca"",
                    ""path"": ""<Keyboard>/1"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""PauseAction"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""f436d8c1-a3c3-4f05-8462-d0a1ee879ae5"",
                    ""path"": ""<Keyboard>/2"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""PauseAction"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""9ff7fa14-5f21-40ba-9cea-7581750c8219"",
                    ""path"": ""<Keyboard>/3"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""PauseAction"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""6a415a5f-1b8e-4cd6-a50a-022ff9a8cf88"",
                    ""path"": ""<Keyboard>/escape"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MenuAction"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""2e5535ff-3b8b-434d-bcec-26ef2c3aba83"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""SelectionBox"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""8cda1e8a-545a-4ed7-b2a3-8060989b5b91"",
                    ""path"": ""<Mouse>/scroll/y"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""ZoomAction"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""8e3d4a91-51e0-498f-a24c-4355d93e0fc8"",
                    ""path"": ""<Keyboard>/e"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Interact"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""0d115c17-8597-4abd-9b9e-96bf169d69e2"",
                    ""path"": ""<Mouse>/position"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MousePosition"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""e29db291-8493-455a-afb1-e2801b4f0abd"",
                    ""path"": ""<Keyboard>/f5"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""QuickSaveAction"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""e652fd1c-cdc0-4a78-9e03-a7e710174db8"",
                    ""path"": ""<Keyboard>/f6"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""QuickLoadAction"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""8e66519c-94c6-4d88-bf76-244425d5dbbd"",
                    ""path"": ""<Mouse>/middleButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Rotation"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""d316813a-69cc-4e83-98da-9db8f0a02665"",
                    ""path"": ""<Keyboard>/shift"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MultiSelection"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""2a932224-67d0-47b0-8979-8fd26926f80a"",
                    ""path"": ""<Keyboard>/m"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MapChangeAction"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        },
        {
            ""name"": ""UI"",
            ""id"": ""91b0f056-5654-4072-85cb-90f14abf0c8c"",
            ""actions"": [
                {
                    ""name"": ""Navigate"",
                    ""type"": ""Value"",
                    ""id"": ""47cf4f9a-90dd-4bc2-ac44-755729759fcc"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Submit"",
                    ""type"": ""Button"",
                    ""id"": ""ff4e2d09-50ee-4c36-a6d5-1142973a763b"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Cancel"",
                    ""type"": ""Button"",
                    ""id"": ""63c79651-9632-4c8f-ac6c-2f0d98bb8d0a"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Point"",
                    ""type"": ""PassThrough"",
                    ""id"": ""dd6b3dd8-61d2-4c72-8eae-ad6b3f10ed6d"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Click"",
                    ""type"": ""PassThrough"",
                    ""id"": ""cb0db407-eb3f-4ef1-a14f-f119589f0a9c"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""ScrollWheel"",
                    ""type"": ""PassThrough"",
                    ""id"": ""3128af90-9fd4-487a-8d0c-a6ee8f2acbac"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""MiddleClick"",
                    ""type"": ""PassThrough"",
                    ""id"": ""645c2765-b74d-445c-a177-706cd455d91e"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""RightClick"",
                    ""type"": ""PassThrough"",
                    ""id"": ""5d6fb0e3-8d21-4f0c-bb1c-b3236d28f751"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""TrackedDevicePosition"",
                    ""type"": ""PassThrough"",
                    ""id"": ""665918e9-258e-4079-8a96-20ded45cdbc9"",
                    ""expectedControlType"": ""Vector3"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""TrackedDeviceOrientation"",
                    ""type"": ""PassThrough"",
                    ""id"": ""c0cfe37a-9ca1-4fef-8a8e-719f975703ec"",
                    ""expectedControlType"": ""Quaternion"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": ""Gamepad"",
                    ""id"": ""5101eb2f-50c8-4599-ab8a-046fae4cf9be"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Navigate"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""82c21da4-e866-44f5-882b-11f9cc20ebb3"",
                    ""path"": ""<Gamepad>/leftStick/up"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": "";Gamepad"",
                    ""action"": ""Navigate"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""up"",
                    ""id"": ""26e857f1-4d82-430a-9d25-79293ede24e6"",
                    ""path"": ""<Gamepad>/rightStick/up"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": "";Gamepad"",
                    ""action"": ""Navigate"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""1fe5ee58-f075-49a8-bd2d-fb2fd92c33d8"",
                    ""path"": ""<Gamepad>/leftStick/down"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": "";Gamepad"",
                    ""action"": ""Navigate"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""49a9cb2f-8238-4f04-aec7-9f6f35bd53f6"",
                    ""path"": ""<Gamepad>/rightStick/down"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": "";Gamepad"",
                    ""action"": ""Navigate"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""cb881f3c-8056-44e1-8175-e972199e5eb7"",
                    ""path"": ""<Gamepad>/leftStick/left"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": "";Gamepad"",
                    ""action"": ""Navigate"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""662a8fab-c3d6-40ce-b299-f1935aa20d95"",
                    ""path"": ""<Gamepad>/rightStick/left"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": "";Gamepad"",
                    ""action"": ""Navigate"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""53da7e29-9ae1-4d84-bb8e-949b5b3be2f7"",
                    ""path"": ""<Gamepad>/leftStick/right"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": "";Gamepad"",
                    ""action"": ""Navigate"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""6a4b31ff-3665-4fc8-88be-8bab3b5c6882"",
                    ""path"": ""<Gamepad>/rightStick/right"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": "";Gamepad"",
                    ""action"": ""Navigate"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""2ae41e30-1964-4e2f-a1e3-6e4e6e1b212e"",
                    ""path"": ""<Gamepad>/dpad"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": "";Gamepad"",
                    ""action"": ""Navigate"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""Joystick"",
                    ""id"": ""f05173c9-7a55-4140-8cae-7ef487cdb89e"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Navigate"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""5bedc34d-a382-47e9-ad10-afcafd2d42fb"",
                    ""path"": ""<Joystick>/stick/up"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Joystick"",
                    ""action"": ""Navigate"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""a2e660ff-66d3-4f3a-bf34-94c92edd32b5"",
                    ""path"": ""<Joystick>/stick/down"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Joystick"",
                    ""action"": ""Navigate"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""e52ea543-fbad-46eb-b69b-7cbed2bb2d1d"",
                    ""path"": ""<Joystick>/stick/left"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Joystick"",
                    ""action"": ""Navigate"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""14f1327d-1460-471a-9648-81e24410b448"",
                    ""path"": ""<Joystick>/stick/right"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Joystick"",
                    ""action"": ""Navigate"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""Keyboard"",
                    ""id"": ""64d4463f-70c7-4197-b1a8-83d413a06add"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Navigate"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""aca3d435-c8ed-4f9c-a7c5-1ab7b769c2f5"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard&Mouse"",
                    ""action"": ""Navigate"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""up"",
                    ""id"": ""115725ab-43c6-44c1-b5cf-dced64e2c1c0"",
                    ""path"": ""<Keyboard>/upArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard&Mouse"",
                    ""action"": ""Navigate"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""0f5d8fe2-8bf5-49df-8800-2fd4f1979132"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard&Mouse"",
                    ""action"": ""Navigate"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""9e3206ac-a1a4-4fbb-93d6-4ea8af1b6272"",
                    ""path"": ""<Keyboard>/downArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard&Mouse"",
                    ""action"": ""Navigate"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""88382977-2919-452d-9bc2-15ae4d0ccf2d"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard&Mouse"",
                    ""action"": ""Navigate"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""8c8625f5-9849-4413-ac6e-c65c74238dcc"",
                    ""path"": ""<Keyboard>/leftArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard&Mouse"",
                    ""action"": ""Navigate"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""0cd2975a-f392-4ebd-855a-ee55c50e60bf"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard&Mouse"",
                    ""action"": ""Navigate"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""37bab3c4-b036-4125-a926-2c0c0df61c79"",
                    ""path"": ""<Keyboard>/rightArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard&Mouse"",
                    ""action"": ""Navigate"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""06de2103-09d6-4785-a5ca-5c4c01637655"",
                    ""path"": ""*/{Submit}"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Submit"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""cfc865e5-02a4-4073-937b-8af30b24607e"",
                    ""path"": ""*/{Cancel}"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Cancel"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""039814ae-a168-4f9e-a7e4-9578bf5bd4cb"",
                    ""path"": ""<Mouse>/position"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard&Mouse"",
                    ""action"": ""Point"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""52162f5e-0013-4598-82b2-4abefd2c540d"",
                    ""path"": ""<Pen>/position"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard&Mouse"",
                    ""action"": ""Point"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""aa2bcbf5-2d2a-4f33-a428-0312233fa5c8"",
                    ""path"": ""<Touchscreen>/touch*/position"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Touch"",
                    ""action"": ""Point"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""bd5f97ac-09ee-4cb6-9057-976847e51b19"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": "";Keyboard&Mouse"",
                    ""action"": ""Click"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""3ca3259f-8b47-4621-b043-b757b248ec52"",
                    ""path"": ""<Pen>/tip"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": "";Keyboard&Mouse"",
                    ""action"": ""Click"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""09e01ffb-dd77-48b5-a9b5-31c45694cb22"",
                    ""path"": ""<Touchscreen>/touch*/press"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Touch"",
                    ""action"": ""Click"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""12f29603-8784-4029-b924-1961795a1929"",
                    ""path"": ""<XRController>/trigger"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""XR"",
                    ""action"": ""Click"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""38587f62-e921-4c36-8a32-639a76cfb971"",
                    ""path"": ""<Mouse>/scroll"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": "";Keyboard&Mouse"",
                    ""action"": ""ScrollWheel"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""c3cafef5-9878-4cd4-b43c-0fd4ed76bb8c"",
                    ""path"": ""<Mouse>/middleButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": "";Keyboard&Mouse"",
                    ""action"": ""MiddleClick"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""6d7a3430-84bc-4479-8cde-6b7121c0e982"",
                    ""path"": ""<Mouse>/rightButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": "";Keyboard&Mouse"",
                    ""action"": ""RightClick"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""c806a590-54b8-4773-a3e6-9dc146d77b14"",
                    ""path"": ""<XRController>/devicePosition"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""XR"",
                    ""action"": ""TrackedDevicePosition"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""585b4da8-ddcf-45a7-940e-db40f2420927"",
                    ""path"": ""<XRController>/deviceRotation"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""XR"",
                    ""action"": ""TrackedDeviceOrientation"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        },
        {
            ""name"": ""InMenu"",
            ""id"": ""88f08d39-1b17-4ec8-92c7-5193240ff298"",
            ""actions"": [
                {
                    ""name"": ""ESC"",
                    ""type"": ""Button"",
                    ""id"": ""f9f9626b-938f-4c74-9dcb-26121a21ebb1"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""269ac23a-8f66-49ff-9f4f-d66a078316e6"",
                    ""path"": ""<Keyboard>/escape"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""ESC"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // Player
        m_Player = asset.FindActionMap("Player", throwIfNotFound: true);
        m_Player_Movement = m_Player.FindAction("Movement", throwIfNotFound: true);
        m_Player_Interact = m_Player.FindAction("Interact", throwIfNotFound: true);
        m_Player_Selection = m_Player.FindAction("Selection", throwIfNotFound: true);
        m_Player_PauseAction = m_Player.FindAction("PauseAction", throwIfNotFound: true);
        m_Player_MenuAction = m_Player.FindAction("MenuAction", throwIfNotFound: true);
        m_Player_SelectionBox = m_Player.FindAction("SelectionBox", throwIfNotFound: true);
        m_Player_ZoomAction = m_Player.FindAction("ZoomAction", throwIfNotFound: true);
        m_Player_MousePosition = m_Player.FindAction("MousePosition", throwIfNotFound: true);
        m_Player_QuickSaveAction = m_Player.FindAction("QuickSaveAction", throwIfNotFound: true);
        m_Player_QuickLoadAction = m_Player.FindAction("QuickLoadAction", throwIfNotFound: true);
        m_Player_Rotation = m_Player.FindAction("Rotation", throwIfNotFound: true);
        m_Player_MultiSelection = m_Player.FindAction("MultiSelection", throwIfNotFound: true);
        m_Player_MapChangeAction = m_Player.FindAction("MapChangeAction", throwIfNotFound: true);
        // UI
        m_UI = asset.FindActionMap("UI", throwIfNotFound: true);
        m_UI_Navigate = m_UI.FindAction("Navigate", throwIfNotFound: true);
        m_UI_Submit = m_UI.FindAction("Submit", throwIfNotFound: true);
        m_UI_Cancel = m_UI.FindAction("Cancel", throwIfNotFound: true);
        m_UI_Point = m_UI.FindAction("Point", throwIfNotFound: true);
        m_UI_Click = m_UI.FindAction("Click", throwIfNotFound: true);
        m_UI_ScrollWheel = m_UI.FindAction("ScrollWheel", throwIfNotFound: true);
        m_UI_MiddleClick = m_UI.FindAction("MiddleClick", throwIfNotFound: true);
        m_UI_RightClick = m_UI.FindAction("RightClick", throwIfNotFound: true);
        m_UI_TrackedDevicePosition = m_UI.FindAction("TrackedDevicePosition", throwIfNotFound: true);
        m_UI_TrackedDeviceOrientation = m_UI.FindAction("TrackedDeviceOrientation", throwIfNotFound: true);
        // InMenu
        m_InMenu = asset.FindActionMap("InMenu", throwIfNotFound: true);
        m_InMenu_ESC = m_InMenu.FindAction("ESC", throwIfNotFound: true);
    }

    public void Dispose()
    {
        UnityEngine.Object.Destroy(asset);
    }

    public InputBinding? bindingMask
    {
        get => asset.bindingMask;
        set => asset.bindingMask = value;
    }

    public ReadOnlyArray<InputDevice>? devices
    {
        get => asset.devices;
        set => asset.devices = value;
    }

    public ReadOnlyArray<InputControlScheme> controlSchemes => asset.controlSchemes;

    public bool Contains(InputAction action)
    {
        return asset.Contains(action);
    }

    public IEnumerator<InputAction> GetEnumerator()
    {
        return asset.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public void Enable()
    {
        asset.Enable();
    }

    public void Disable()
    {
        asset.Disable();
    }

    // Player
    private readonly InputActionMap m_Player;
    private IPlayerActions m_PlayerActionsCallbackInterface;
    private readonly InputAction m_Player_Movement;
    private readonly InputAction m_Player_Interact;
    private readonly InputAction m_Player_Selection;
    private readonly InputAction m_Player_PauseAction;
    private readonly InputAction m_Player_MenuAction;
    private readonly InputAction m_Player_SelectionBox;
    private readonly InputAction m_Player_ZoomAction;
    private readonly InputAction m_Player_MousePosition;
    private readonly InputAction m_Player_QuickSaveAction;
    private readonly InputAction m_Player_QuickLoadAction;
    private readonly InputAction m_Player_Rotation;
    private readonly InputAction m_Player_MultiSelection;
    private readonly InputAction m_Player_MapChangeAction;
    public struct PlayerActions
    {
        private @PlayerInputActions m_Wrapper;
        public PlayerActions(@PlayerInputActions wrapper) { m_Wrapper = wrapper; }
        public InputAction @Movement => m_Wrapper.m_Player_Movement;
        public InputAction @Interact => m_Wrapper.m_Player_Interact;
        public InputAction @Selection => m_Wrapper.m_Player_Selection;
        public InputAction @PauseAction => m_Wrapper.m_Player_PauseAction;
        public InputAction @MenuAction => m_Wrapper.m_Player_MenuAction;
        public InputAction @SelectionBox => m_Wrapper.m_Player_SelectionBox;
        public InputAction @ZoomAction => m_Wrapper.m_Player_ZoomAction;
        public InputAction @MousePosition => m_Wrapper.m_Player_MousePosition;
        public InputAction @QuickSaveAction => m_Wrapper.m_Player_QuickSaveAction;
        public InputAction @QuickLoadAction => m_Wrapper.m_Player_QuickLoadAction;
        public InputAction @Rotation => m_Wrapper.m_Player_Rotation;
        public InputAction @MultiSelection => m_Wrapper.m_Player_MultiSelection;
        public InputAction @MapChangeAction => m_Wrapper.m_Player_MapChangeAction;
        public InputActionMap Get() { return m_Wrapper.m_Player; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(PlayerActions set) { return set.Get(); }
        public void SetCallbacks(IPlayerActions instance)
        {
            if (m_Wrapper.m_PlayerActionsCallbackInterface != null)
            {
                @Movement.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnMovement;
                @Movement.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnMovement;
                @Movement.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnMovement;
                @Interact.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnInteract;
                @Interact.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnInteract;
                @Interact.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnInteract;
                @Selection.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnSelection;
                @Selection.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnSelection;
                @Selection.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnSelection;
                @PauseAction.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnPauseAction;
                @PauseAction.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnPauseAction;
                @PauseAction.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnPauseAction;
                @MenuAction.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnMenuAction;
                @MenuAction.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnMenuAction;
                @MenuAction.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnMenuAction;
                @SelectionBox.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnSelectionBox;
                @SelectionBox.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnSelectionBox;
                @SelectionBox.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnSelectionBox;
                @ZoomAction.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnZoomAction;
                @ZoomAction.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnZoomAction;
                @ZoomAction.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnZoomAction;
                @MousePosition.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnMousePosition;
                @MousePosition.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnMousePosition;
                @MousePosition.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnMousePosition;
                @QuickSaveAction.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnQuickSaveAction;
                @QuickSaveAction.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnQuickSaveAction;
                @QuickSaveAction.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnQuickSaveAction;
                @QuickLoadAction.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnQuickLoadAction;
                @QuickLoadAction.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnQuickLoadAction;
                @QuickLoadAction.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnQuickLoadAction;
                @Rotation.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnRotation;
                @Rotation.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnRotation;
                @Rotation.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnRotation;
                @MultiSelection.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnMultiSelection;
                @MultiSelection.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnMultiSelection;
                @MultiSelection.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnMultiSelection;
                @MapChangeAction.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnMapChangeAction;
                @MapChangeAction.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnMapChangeAction;
                @MapChangeAction.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnMapChangeAction;
            }
            m_Wrapper.m_PlayerActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Movement.started += instance.OnMovement;
                @Movement.performed += instance.OnMovement;
                @Movement.canceled += instance.OnMovement;
                @Interact.started += instance.OnInteract;
                @Interact.performed += instance.OnInteract;
                @Interact.canceled += instance.OnInteract;
                @Selection.started += instance.OnSelection;
                @Selection.performed += instance.OnSelection;
                @Selection.canceled += instance.OnSelection;
                @PauseAction.started += instance.OnPauseAction;
                @PauseAction.performed += instance.OnPauseAction;
                @PauseAction.canceled += instance.OnPauseAction;
                @MenuAction.started += instance.OnMenuAction;
                @MenuAction.performed += instance.OnMenuAction;
                @MenuAction.canceled += instance.OnMenuAction;
                @SelectionBox.started += instance.OnSelectionBox;
                @SelectionBox.performed += instance.OnSelectionBox;
                @SelectionBox.canceled += instance.OnSelectionBox;
                @ZoomAction.started += instance.OnZoomAction;
                @ZoomAction.performed += instance.OnZoomAction;
                @ZoomAction.canceled += instance.OnZoomAction;
                @MousePosition.started += instance.OnMousePosition;
                @MousePosition.performed += instance.OnMousePosition;
                @MousePosition.canceled += instance.OnMousePosition;
                @QuickSaveAction.started += instance.OnQuickSaveAction;
                @QuickSaveAction.performed += instance.OnQuickSaveAction;
                @QuickSaveAction.canceled += instance.OnQuickSaveAction;
                @QuickLoadAction.started += instance.OnQuickLoadAction;
                @QuickLoadAction.performed += instance.OnQuickLoadAction;
                @QuickLoadAction.canceled += instance.OnQuickLoadAction;
                @Rotation.started += instance.OnRotation;
                @Rotation.performed += instance.OnRotation;
                @Rotation.canceled += instance.OnRotation;
                @MultiSelection.started += instance.OnMultiSelection;
                @MultiSelection.performed += instance.OnMultiSelection;
                @MultiSelection.canceled += instance.OnMultiSelection;
                @MapChangeAction.started += instance.OnMapChangeAction;
                @MapChangeAction.performed += instance.OnMapChangeAction;
                @MapChangeAction.canceled += instance.OnMapChangeAction;
            }
        }
    }
    public PlayerActions @Player => new PlayerActions(this);

    // UI
    private readonly InputActionMap m_UI;
    private IUIActions m_UIActionsCallbackInterface;
    private readonly InputAction m_UI_Navigate;
    private readonly InputAction m_UI_Submit;
    private readonly InputAction m_UI_Cancel;
    private readonly InputAction m_UI_Point;
    private readonly InputAction m_UI_Click;
    private readonly InputAction m_UI_ScrollWheel;
    private readonly InputAction m_UI_MiddleClick;
    private readonly InputAction m_UI_RightClick;
    private readonly InputAction m_UI_TrackedDevicePosition;
    private readonly InputAction m_UI_TrackedDeviceOrientation;
    public struct UIActions
    {
        private @PlayerInputActions m_Wrapper;
        public UIActions(@PlayerInputActions wrapper) { m_Wrapper = wrapper; }
        public InputAction @Navigate => m_Wrapper.m_UI_Navigate;
        public InputAction @Submit => m_Wrapper.m_UI_Submit;
        public InputAction @Cancel => m_Wrapper.m_UI_Cancel;
        public InputAction @Point => m_Wrapper.m_UI_Point;
        public InputAction @Click => m_Wrapper.m_UI_Click;
        public InputAction @ScrollWheel => m_Wrapper.m_UI_ScrollWheel;
        public InputAction @MiddleClick => m_Wrapper.m_UI_MiddleClick;
        public InputAction @RightClick => m_Wrapper.m_UI_RightClick;
        public InputAction @TrackedDevicePosition => m_Wrapper.m_UI_TrackedDevicePosition;
        public InputAction @TrackedDeviceOrientation => m_Wrapper.m_UI_TrackedDeviceOrientation;
        public InputActionMap Get() { return m_Wrapper.m_UI; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(UIActions set) { return set.Get(); }
        public void SetCallbacks(IUIActions instance)
        {
            if (m_Wrapper.m_UIActionsCallbackInterface != null)
            {
                @Navigate.started -= m_Wrapper.m_UIActionsCallbackInterface.OnNavigate;
                @Navigate.performed -= m_Wrapper.m_UIActionsCallbackInterface.OnNavigate;
                @Navigate.canceled -= m_Wrapper.m_UIActionsCallbackInterface.OnNavigate;
                @Submit.started -= m_Wrapper.m_UIActionsCallbackInterface.OnSubmit;
                @Submit.performed -= m_Wrapper.m_UIActionsCallbackInterface.OnSubmit;
                @Submit.canceled -= m_Wrapper.m_UIActionsCallbackInterface.OnSubmit;
                @Cancel.started -= m_Wrapper.m_UIActionsCallbackInterface.OnCancel;
                @Cancel.performed -= m_Wrapper.m_UIActionsCallbackInterface.OnCancel;
                @Cancel.canceled -= m_Wrapper.m_UIActionsCallbackInterface.OnCancel;
                @Point.started -= m_Wrapper.m_UIActionsCallbackInterface.OnPoint;
                @Point.performed -= m_Wrapper.m_UIActionsCallbackInterface.OnPoint;
                @Point.canceled -= m_Wrapper.m_UIActionsCallbackInterface.OnPoint;
                @Click.started -= m_Wrapper.m_UIActionsCallbackInterface.OnClick;
                @Click.performed -= m_Wrapper.m_UIActionsCallbackInterface.OnClick;
                @Click.canceled -= m_Wrapper.m_UIActionsCallbackInterface.OnClick;
                @ScrollWheel.started -= m_Wrapper.m_UIActionsCallbackInterface.OnScrollWheel;
                @ScrollWheel.performed -= m_Wrapper.m_UIActionsCallbackInterface.OnScrollWheel;
                @ScrollWheel.canceled -= m_Wrapper.m_UIActionsCallbackInterface.OnScrollWheel;
                @MiddleClick.started -= m_Wrapper.m_UIActionsCallbackInterface.OnMiddleClick;
                @MiddleClick.performed -= m_Wrapper.m_UIActionsCallbackInterface.OnMiddleClick;
                @MiddleClick.canceled -= m_Wrapper.m_UIActionsCallbackInterface.OnMiddleClick;
                @RightClick.started -= m_Wrapper.m_UIActionsCallbackInterface.OnRightClick;
                @RightClick.performed -= m_Wrapper.m_UIActionsCallbackInterface.OnRightClick;
                @RightClick.canceled -= m_Wrapper.m_UIActionsCallbackInterface.OnRightClick;
                @TrackedDevicePosition.started -= m_Wrapper.m_UIActionsCallbackInterface.OnTrackedDevicePosition;
                @TrackedDevicePosition.performed -= m_Wrapper.m_UIActionsCallbackInterface.OnTrackedDevicePosition;
                @TrackedDevicePosition.canceled -= m_Wrapper.m_UIActionsCallbackInterface.OnTrackedDevicePosition;
                @TrackedDeviceOrientation.started -= m_Wrapper.m_UIActionsCallbackInterface.OnTrackedDeviceOrientation;
                @TrackedDeviceOrientation.performed -= m_Wrapper.m_UIActionsCallbackInterface.OnTrackedDeviceOrientation;
                @TrackedDeviceOrientation.canceled -= m_Wrapper.m_UIActionsCallbackInterface.OnTrackedDeviceOrientation;
            }
            m_Wrapper.m_UIActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Navigate.started += instance.OnNavigate;
                @Navigate.performed += instance.OnNavigate;
                @Navigate.canceled += instance.OnNavigate;
                @Submit.started += instance.OnSubmit;
                @Submit.performed += instance.OnSubmit;
                @Submit.canceled += instance.OnSubmit;
                @Cancel.started += instance.OnCancel;
                @Cancel.performed += instance.OnCancel;
                @Cancel.canceled += instance.OnCancel;
                @Point.started += instance.OnPoint;
                @Point.performed += instance.OnPoint;
                @Point.canceled += instance.OnPoint;
                @Click.started += instance.OnClick;
                @Click.performed += instance.OnClick;
                @Click.canceled += instance.OnClick;
                @ScrollWheel.started += instance.OnScrollWheel;
                @ScrollWheel.performed += instance.OnScrollWheel;
                @ScrollWheel.canceled += instance.OnScrollWheel;
                @MiddleClick.started += instance.OnMiddleClick;
                @MiddleClick.performed += instance.OnMiddleClick;
                @MiddleClick.canceled += instance.OnMiddleClick;
                @RightClick.started += instance.OnRightClick;
                @RightClick.performed += instance.OnRightClick;
                @RightClick.canceled += instance.OnRightClick;
                @TrackedDevicePosition.started += instance.OnTrackedDevicePosition;
                @TrackedDevicePosition.performed += instance.OnTrackedDevicePosition;
                @TrackedDevicePosition.canceled += instance.OnTrackedDevicePosition;
                @TrackedDeviceOrientation.started += instance.OnTrackedDeviceOrientation;
                @TrackedDeviceOrientation.performed += instance.OnTrackedDeviceOrientation;
                @TrackedDeviceOrientation.canceled += instance.OnTrackedDeviceOrientation;
            }
        }
    }
    public UIActions @UI => new UIActions(this);

    // InMenu
    private readonly InputActionMap m_InMenu;
    private IInMenuActions m_InMenuActionsCallbackInterface;
    private readonly InputAction m_InMenu_ESC;
    public struct InMenuActions
    {
        private @PlayerInputActions m_Wrapper;
        public InMenuActions(@PlayerInputActions wrapper) { m_Wrapper = wrapper; }
        public InputAction @ESC => m_Wrapper.m_InMenu_ESC;
        public InputActionMap Get() { return m_Wrapper.m_InMenu; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(InMenuActions set) { return set.Get(); }
        public void SetCallbacks(IInMenuActions instance)
        {
            if (m_Wrapper.m_InMenuActionsCallbackInterface != null)
            {
                @ESC.started -= m_Wrapper.m_InMenuActionsCallbackInterface.OnESC;
                @ESC.performed -= m_Wrapper.m_InMenuActionsCallbackInterface.OnESC;
                @ESC.canceled -= m_Wrapper.m_InMenuActionsCallbackInterface.OnESC;
            }
            m_Wrapper.m_InMenuActionsCallbackInterface = instance;
            if (instance != null)
            {
                @ESC.started += instance.OnESC;
                @ESC.performed += instance.OnESC;
                @ESC.canceled += instance.OnESC;
            }
        }
    }
    public InMenuActions @InMenu => new InMenuActions(this);
    public interface IPlayerActions
    {
        void OnMovement(InputAction.CallbackContext context);
        void OnInteract(InputAction.CallbackContext context);
        void OnSelection(InputAction.CallbackContext context);
        void OnPauseAction(InputAction.CallbackContext context);
        void OnMenuAction(InputAction.CallbackContext context);
        void OnSelectionBox(InputAction.CallbackContext context);
        void OnZoomAction(InputAction.CallbackContext context);
        void OnMousePosition(InputAction.CallbackContext context);
        void OnQuickSaveAction(InputAction.CallbackContext context);
        void OnQuickLoadAction(InputAction.CallbackContext context);
        void OnRotation(InputAction.CallbackContext context);
        void OnMultiSelection(InputAction.CallbackContext context);
        void OnMapChangeAction(InputAction.CallbackContext context);
    }
    public interface IUIActions
    {
        void OnNavigate(InputAction.CallbackContext context);
        void OnSubmit(InputAction.CallbackContext context);
        void OnCancel(InputAction.CallbackContext context);
        void OnPoint(InputAction.CallbackContext context);
        void OnClick(InputAction.CallbackContext context);
        void OnScrollWheel(InputAction.CallbackContext context);
        void OnMiddleClick(InputAction.CallbackContext context);
        void OnRightClick(InputAction.CallbackContext context);
        void OnTrackedDevicePosition(InputAction.CallbackContext context);
        void OnTrackedDeviceOrientation(InputAction.CallbackContext context);
    }
    public interface IInMenuActions
    {
        void OnESC(InputAction.CallbackContext context);
    }
}
