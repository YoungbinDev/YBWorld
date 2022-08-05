using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

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
        
        anim.SetFloat("vertical", Mathf.Lerp(anim.GetFloat("vertical"), moveVec.magnitude, Time.deltaTime * 5));

        if (!isMove)
        {
            if (anim.GetFloat("vertical") < 0.05f)
                anim.SetFloat("vertical", 0);
        }

        rigid.MovePosition(rootPos);
    }

    private void OnAnimatorMove()
    {
        rootPos += anim.deltaPosition;
    }
}
