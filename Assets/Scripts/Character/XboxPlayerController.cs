using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class XboxPlayerController : PlayerController
{
    [SerializeField] private float timeToMaxSpeed = 1.5f;
    private float lerpRef = 0;

    private void OnEnable()
    {
        AddInputAction();
    }

    private void OnDisable()
    {
        RemoveInputAction();
    }

    private void Start()
    {
        Init();
    }

    protected override void Init()
    {
        base.Init();
    }

    private void FixedUpdate()
    {
        lerpRef = Mathf.Clamp(lerpRef + (isMove == true ? Time.deltaTime : -Time.deltaTime) * timeToMaxSpeed, 0, 1);
        anim.SetFloat("vertical", Mathf.Lerp(0, 1, lerpRef));
    }
}
