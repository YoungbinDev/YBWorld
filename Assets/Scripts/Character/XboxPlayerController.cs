using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.InputSystem;

public class XboxPlayerController : PlayerController
{
    private void Start()
    {
        SettingComponent();
    }

    protected override void SettingComponent()
    {
        base.SettingComponent();
    }

    private void FixedUpdate()
    {
        Vector3 forward = cam.transform.forward;
        Vector3 right = cam.transform.right;

        forward.y = 0; right.y = 0;

        forward.Normalize();
        right.Normalize();
        
        if (isInput)
        {
            moveDir = (forward * inputVec.y + right * inputVec.x).normalized;

            isMove = true;
            anim.SetFloat("vertical", Mathf.MoveTowards(anim.GetFloat("vertical"), inputVec.magnitude, Time.deltaTime * timeToMaxAnimSpeed));
        }
        else
        {
            if (!firstStep)
            {
                if (anim.GetFloat("vertical") > 0.8f)
                {
                    anim.SetFloat("vertical", Mathf.MoveTowards(anim.GetFloat("vertical"), 1.5f, Time.deltaTime * timeToMaxAnimSpeed / 2));

                    if(anim.GetFloat("vertical") > 1.45f)
                    {
                        isMove = false;
                    }
                }
                else
                {
                    isMove = false;
                }
            }
        }

        anim.SetBool("isMove", isMove);

        rigid.MoveRotation(Quaternion.Lerp(rigid.rotation, Quaternion.LookRotation(moveDir), Time.deltaTime * 5));
        rigid.MovePosition(rigid.position + moveVec);
        moveVec = Vector3.zero;
    }

    private void OnAnimatorMove()
    {
        moveVec += new Vector3(anim.deltaPosition.x, 0, anim.deltaPosition.z);
    }

    public bool firstStep = true;

    public override void Movement(InputAction.CallbackContext context)
    {
        base.Movement(context);

        if(context.performed)
        {
            if (firstStep)
            {
                firstStep = false;
            }
        }
    }

}
