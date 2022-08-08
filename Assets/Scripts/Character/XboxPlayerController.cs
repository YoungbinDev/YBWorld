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
        Vector3 forward = mainCam.transform.forward;
        Vector3 right = mainCam.transform.right;

        forward.y = 0; right.y = 0;

        forward.Normalize();
        right.Normalize();
        
        if (isInput)
        {
            moveDir = (forward * inputVec.y + right * inputVec.x).normalized;

            isMove = true;

            if (isRun)
            {
                if(anim.GetFloat("vertical") > 1.0f)
                    anim.SetFloat("vertical", Mathf.MoveTowards(anim.GetFloat("vertical"), inputVec.magnitude * 1.5f, Time.deltaTime * timeToMaxAnimSpeed * 0.5f));
                else
                    anim.SetFloat("vertical", Mathf.MoveTowards(anim.GetFloat("vertical"), inputVec.magnitude * 1.5f, Time.deltaTime * timeToMaxAnimSpeed * 1.3f));
            }
            else
                anim.SetFloat("vertical", Mathf.MoveTowards(anim.GetFloat("vertical"), inputVec.magnitude, Time.deltaTime * timeToMaxAnimSpeed));
        }
        else
        {
            if (!firstStep)
            {
                if (anim.GetFloat("vertical") > 1.3f) //달리기 중이면
                {
                    anim.SetFloat("vertical", Mathf.MoveTowards(anim.GetFloat("vertical"), 2.3f, Time.deltaTime * timeToMaxAnimSpeed * 0.8f));

                    if(anim.GetFloat("vertical") > 2.28f) //멈추는 애니메이션 끝나면
                    {
                        isMove = false;
                    }
                }
                else //걷는 중이면
                {
                    isMove = false;
                }
            }
        }

        anim.SetBool("isMove", isMove);

        if(moveDir != Vector3.zero)
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
