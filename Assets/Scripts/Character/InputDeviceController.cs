using System;
using System.Collections;
using System.Collections.Generic;
#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;
using UnityEngine.InputSystem;

public class InputDeviceController : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private PlayerInputActions asset;

    public EnumInputDevice InputDevices
    {
        get => inputDevice;

        set
        {
            inputDevice = value;
            UpdateInputDevice();
        }
    }

    [SerializeField] private EnumInputDevice inputDevice = EnumInputDevice.XboxController;

    private void OnEnable()
    {
        UpdateInputDevice();
    }

    public void UpdateInputDevice()
    {
        player = GameObject.FindGameObjectWithTag("Player");

        foreach (PlayerController pc in player.GetComponents<PlayerController>())
        {
            if (pc.inputDevice == inputDevice)
            {
                pc.enabled = true;
                pc.input.defaultControlScheme = inputDevice.ToString();
                //pc.input.SwitchCurrentControlScheme(inputDevice.ToString(), pc.input.devices.ToArray());
                pc.input.enabled = false;
                pc.input.enabled = true;
            }
            else
            {
                pc.enabled = false;
            }
        }
    }
}

#if UNITY_EDITOR

[CustomEditor(typeof(InputDeviceController))]
public class InputDeviceControlEditor : Editor
{
    public InputDeviceController controller;

    public void OnEnable()
    {
        controller = (InputDeviceController)target;
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        if (GUILayout.Button("Setting InputDevice"))
        {
            controller.UpdateInputDevice();
        }
    }
}
#endif