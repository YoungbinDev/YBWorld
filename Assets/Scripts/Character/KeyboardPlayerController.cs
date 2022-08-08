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

        moveVec = transform.position;
    }

    private void FixedUpdate()
    {
        Vector3 moveDir = new Vector3(inputVec.x, 0, inputVec.y).normalized;
        moveDir = mainCam.transform.TransformDirection(moveDir);

        if(isMove)
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(moveDir), Time.deltaTime * 10);


        rigid.MovePosition(moveVec);
        moveVec = Vector3.zero;
    }

    private void OnAnimatorMove()
    {
        moveVec += anim.deltaPosition;
    }
}
