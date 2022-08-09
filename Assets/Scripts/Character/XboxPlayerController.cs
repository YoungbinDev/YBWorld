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

            if (currentCoroutine != null)
            {
                StopCoroutine(currentCoroutine);
                currentCoroutine = null;
            }

            anim.SetFloat("horizontal", Mathf.MoveTowards(anim.GetFloat("horizontal"), 0, Time.deltaTime * timeToMaxAnimSpeed * 3));

            if (isRun)
            {
                anim.SetFloat("vertical", Mathf.MoveTowards(anim.GetFloat("vertical"), inputVec.magnitude * 2f, Time.deltaTime * timeToMaxAnimSpeed));
            }
            else
            {
                anim.SetFloat("vertical", Mathf.MoveTowards(anim.GetFloat("vertical"), inputVec.magnitude, Time.deltaTime * timeToMaxAnimSpeed));
            }
        }
        else
        {
            if (anim.GetFloat("vertical") > 0)
            {
                if(currentCoroutine == null)
                    currentCoroutine = StartCoroutine(StopWalking());
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

    Coroutine currentCoroutine = null;

    IEnumerator StopWalking()
    {
        float tempV = anim.GetFloat("vertical");
        float tempH = anim.GetFloat("horizontal");
        float tempTime = 0;

        if (tempV > 1.5f)
        {
            while (anim.GetFloat("vertical") != 0 || anim.GetFloat("horizontal") != 1)
            {
                yield return null;

                tempTime = Mathf.Clamp(tempTime + Time.deltaTime * 0.75f, 0, 1);

                anim.SetFloat("vertical", Mathf.Lerp(tempV, 0f, tempTime));
                anim.SetFloat("horizontal", Mathf.Lerp(tempH, 1f, tempTime));
            }
        }
        else
        {
            while (anim.GetFloat("vertical") > 0.3f)
            {
                yield return null;

                tempTime = Mathf.Clamp(tempTime + Time.deltaTime * 2, 0, 1);

                anim.SetFloat("vertical", Mathf.Lerp(tempV, 0f, tempTime));
                anim.SetFloat("horizontal", Mathf.Lerp(tempH, 0f, tempTime));
            }
        }

        isMove = false;

        currentCoroutine = null;
    }

    public bool firstStep = true;

    public override void Movement(InputAction.CallbackContext context)
    {
        base.Movement(context);

        if(context.performed)
        {
           
        }
    }

}
