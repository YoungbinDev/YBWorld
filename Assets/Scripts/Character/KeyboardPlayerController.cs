using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyboardPlayerController : PlayerController
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
        Vector3 moveDir = new Vector3(moveVec.x, 0, moveVec.y).normalized;
        moveDir = cam.transform.TransformDirection(moveDir);

        if(isMove)
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(moveDir), Time.deltaTime * 10);


        rigid.MovePosition(rootPos);
    }

    private void OnAnimatorMove()
    {
        rootPos += anim.deltaPosition;
    }
}
