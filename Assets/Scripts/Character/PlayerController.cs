using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using static PlayerInputActions;

public enum PlayerBehavior
{
    Idle,
    Walk,
    Run
}

public class PlayerController : MonoBehaviour
{
    public EnumInputDevice inputDevice = EnumInputDevice.None;
    public PlayerInput input;
    public Camera cam;
    public float walkSpeed;
    public float runSpeed;
    public bool isRun;
    public bool isInput;
    public bool isMove;
    public Vector2 inputVec;
    public Vector3 moveDir;
    public Vector3 moveVec;
    public float timeToMaxAnimSpeed = 1.0f;

    protected Animator anim;
    protected Rigidbody rigid;
    protected PlayerActions playerAction;
    protected float lerpRef = 0;

    private void OnEnable()
    {
        AddInputAction();
    }

    private void OnDisable()
    {
        RemoveInputAction();
    }

    protected virtual void SettingComponent()
    {
        anim = this.GetComponent<Animator>();
        rigid = this.GetComponent<Rigidbody>();
    }

    protected void AddInputAction()
    {
        input.actions["Movement"].performed += Movement;
        input.actions["Movement"].canceled += Movement;
        input.actions["Run"].performed += Run;
    }

    protected void RemoveInputAction()
    {
        input.actions["Movement"].performed -= Movement;
        input.actions["Run"].performed -= Run;
    }

    public virtual void Movement(InputAction.CallbackContext context)
    {
        inputVec = context.ReadValue<Vector2>();
        isInput = inputVec.magnitude != 0;
    }

    public void Run(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            isRun = !isRun;
        }
    }

    public virtual void Jump(InputAction.CallbackContext context)
    {

    }
}
