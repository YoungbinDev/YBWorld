using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using static PlayerInputActions;

public class PlayerController : MonoBehaviour
{
    public EnumInputDevice inputDevice = EnumInputDevice.None;
    public float walkSpeed;
    public float runSpeed;
    public bool isRun;
    public bool isMove;
    public Vector2 moveVec;

    protected Animator anim;
    protected Rigidbody rigid;
    protected PlayerActions playerAction;

    public PlayerInput input;

    protected virtual void Init()
    {
        anim = this.GetComponent<Animator>();
        rigid = this.GetComponent<Rigidbody>();
    }

    protected void AddInputAction()
    {
        input.actions["Movement"].performed += Movement;
        input.actions["Run"].performed += Run;
    }

    protected void RemoveInputAction()
    {
        input.actions["Movement"].performed -= Movement;
        input.actions["Run"].performed -= Run;
    }

    public void Movement(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            isMove = !isMove;
        }
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
