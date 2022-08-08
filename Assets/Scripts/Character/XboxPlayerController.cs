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

            anim.SetFloat("horizontal", Mathf.MoveTowards(anim.GetFloat("horizontal"), 0, Time.deltaTime * timeToMaxAnimSpeed));

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
        if (anim.GetFloat("vertical") > 1.75f)
        {
            while (anim.GetFloat("vertical") != 0 || anim.GetFloat("horizontal") != 1)
            {
                yield return null;

                anim.SetFloat("vertical", Mathf.MoveTowards(anim.GetFloat("vertical"), 0f, Time.deltaTime * timeToMaxAnimSpeed * 1.5f));
                anim.SetFloat("horizontal", Mathf.MoveTowards(anim.GetFloat("horizontal"), 1f, Time.deltaTime * timeToMaxAnimSpeed));
            }

            isMove = false;
        }
        else
            isMove = false;

        yield return new WaitUntil(() => anim.GetFloat("vertical") == 0);
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
