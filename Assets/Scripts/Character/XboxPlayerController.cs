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

        rootPos = transform.position;
    }

    private void FixedUpdate()
    {

        if (isMove)
            anim.SetFloat("vertical", Mathf.Lerp(anim.GetFloat("vertical"), moveVec.magnitude * 1, Time.deltaTime * timeToMaxAnimSpeed));
        else
        {
            anim.SetFloat("vertical", Mathf.Lerp(anim.GetFloat("vertical"), 0, Time.deltaTime * timeToMaxAnimSpeed * 2));

            if (anim.GetFloat("vertical") < 0.3f)
            {
                firstStep = true;
                anim.ResetTrigger("StartWalking");
            }
        }

        anim.SetBool("isMove", isMove);

        rigid.MovePosition(rootPos);
    }

    private void OnAnimatorMove()
    {
        rootPos += new Vector3(0, 0, anim.deltaPosition.z);
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
                anim.ResetTrigger("StopWalking");
                anim.SetTrigger("StartWalking");
            }
        }

        if (context.canceled)
        {
            anim.SetTrigger("StopWalking");
        }
    }

}
