// GENERATED AUTOMATICALLY FROM 'Assets/InputSystem/PlayerInputActions.inputactions'

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
            ""name"": ""TPSPlayer"",
            ""id"": ""7dc812d5-66bb-4f84-9661-4c9a07aa9c8e"",
            ""actions"": [
                {
                    ""name"": ""Movement"",
                    ""type"": ""Button"",
                    ""id"": ""2125e221-5bb7-4cb8-825f-a22878df43a2"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": ""Press(behavior=2)""
                },
                {
                    ""name"": ""Jump"",
                    ""type"": ""Button"",
                    ""id"": ""f1e2500e-fdee-4666-8702-f4d8db3abd57"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Sprint"",
                    ""type"": ""Button"",
                    ""id"": ""c49e43a8-4ebf-4705-b0b1-76f6121127d4"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": ""Press(behavior=2)""
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""1b9e19c5-c387-45ec-be7d-29897d501b31"",
                    ""path"": ""<Keyboard>/space"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Jump"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""3a9586dd-969a-4d6f-a651-dddfa1066daa"",
                    ""path"": ""<XInputController>/buttonSouth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Xbox Controller"",
                    ""action"": ""Jump"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""e908c24a-0c19-40bc-9060-b4d013757f1a"",
                    ""path"": ""<Keyboard>/shift"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Sprint"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""15d2aefb-2088-4739-81f5-cfca5101812f"",
                    ""path"": ""<XInputController>/buttonEast"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Xbox Controller"",
                    ""action"": ""Sprint"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""Keyboard"",
                    ""id"": ""433e8ca4-da32-40ed-ae1c-a847e684faa4"",
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
                    ""id"": ""5e69db07-d093-4127-8ba8-25cae7b6c43e"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""81ded819-b396-4c41-a9ba-528f8c1c1c3f"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""69d143f8-480f-486f-8d04-87851a61005d"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""6f64a64d-fb8e-4dee-9623-3851ab3c80c6"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""088e380e-37e0-4c0f-8d3a-721f8662d11f"",
                    ""path"": ""<XInputController>/leftStick"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Xbox Controller"",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": [
        {
            ""name"": ""Keyboard"",
            ""bindingGroup"": ""Keyboard"",
            ""devices"": [
                {
                    ""devicePath"": ""<Keyboard>"",
                    ""isOptional"": false,
                    ""isOR"": false
                }
            ]
        },
        {
            ""name"": ""Xbox Controller"",
            ""bindingGroup"": ""Xbox Controller"",
            ""devices"": [
                {
                    ""devicePath"": ""<XInputController>"",
                    ""isOptional"": false,
                    ""isOR"": false
                }
            ]
        }
    ]
}");
        // TPSPlayer
        m_TPSPlayer = asset.FindActionMap("TPSPlayer", throwIfNotFound: true);
        m_TPSPlayer_Movement = m_TPSPlayer.FindAction("Movement", throwIfNotFound: true);
        m_TPSPlayer_Jump = m_TPSPlayer.FindAction("Jump", throwIfNotFound: true);
        m_TPSPlayer_Sprint = m_TPSPlayer.FindAction("Sprint", throwIfNotFound: true);
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

    // TPSPlayer
    private readonly InputActionMap m_TPSPlayer;
    private ITPSPlayerActions m_TPSPlayerActionsCallbackInterface;
    private readonly InputAction m_TPSPlayer_Movement;
    private readonly InputAction m_TPSPlayer_Jump;
    private readonly InputAction m_TPSPlayer_Sprint;
    public struct TPSPlayerActions
    {
        private @PlayerInputActions m_Wrapper;
        public TPSPlayerActions(@PlayerInputActions wrapper) { m_Wrapper = wrapper; }
        public InputAction @Movement => m_Wrapper.m_TPSPlayer_Movement;
        public InputAction @Jump => m_Wrapper.m_TPSPlayer_Jump;
        public InputAction @Sprint => m_Wrapper.m_TPSPlayer_Sprint;
        public InputActionMap Get() { return m_Wrapper.m_TPSPlayer; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(TPSPlayerActions set) { return set.Get(); }
        public void SetCallbacks(ITPSPlayerActions instance)
        {
            if (m_Wrapper.m_TPSPlayerActionsCallbackInterface != null)
            {
                @Movement.started -= m_Wrapper.m_TPSPlayerActionsCallbackInterface.OnMovement;
                @Movement.performed -= m_Wrapper.m_TPSPlayerActionsCallbackInterface.OnMovement;
                @Movement.canceled -= m_Wrapper.m_TPSPlayerActionsCallbackInterface.OnMovement;
                @Jump.started -= m_Wrapper.m_TPSPlayerActionsCallbackInterface.OnJump;
                @Jump.performed -= m_Wrapper.m_TPSPlayerActionsCallbackInterface.OnJump;
                @Jump.canceled -= m_Wrapper.m_TPSPlayerActionsCallbackInterface.OnJump;
                @Sprint.started -= m_Wrapper.m_TPSPlayerActionsCallbackInterface.OnSprint;
                @Sprint.performed -= m_Wrapper.m_TPSPlayerActionsCallbackInterface.OnSprint;
                @Sprint.canceled -= m_Wrapper.m_TPSPlayerActionsCallbackInterface.OnSprint;
            }
            m_Wrapper.m_TPSPlayerActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Movement.started += instance.OnMovement;
                @Movement.performed += instance.OnMovement;
                @Movement.canceled += instance.OnMovement;
                @Jump.started += instance.OnJump;
                @Jump.performed += instance.OnJump;
                @Jump.canceled += instance.OnJump;
                @Sprint.started += instance.OnSprint;
                @Sprint.performed += instance.OnSprint;
                @Sprint.canceled += instance.OnSprint;
            }
        }
    }
    public TPSPlayerActions @TPSPlayer => new TPSPlayerActions(this);
    private int m_KeyboardSchemeIndex = -1;
    public InputControlScheme KeyboardScheme
    {
        get
        {
            if (m_KeyboardSchemeIndex == -1) m_KeyboardSchemeIndex = asset.FindControlSchemeIndex("Keyboard");
            return asset.controlSchemes[m_KeyboardSchemeIndex];
        }
    }
    private int m_XboxControllerSchemeIndex = -1;
    public InputControlScheme XboxControllerScheme
    {
        get
        {
            if (m_XboxControllerSchemeIndex == -1) m_XboxControllerSchemeIndex = asset.FindControlSchemeIndex("Xbox Controller");
            return asset.controlSchemes[m_XboxControllerSchemeIndex];
        }
    }
    public interface ITPSPlayerActions
    {
        void OnMovement(InputAction.CallbackContext context);
        void OnJump(InputAction.CallbackContext context);
        void OnSprint(InputAction.CallbackContext context);
    }
}
