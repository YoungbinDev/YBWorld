using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using static PlayerInputActions;

public class TPSController : MonoBehaviour
{
    [SerializeField] EnumInputDevice inputDevice;
    [SerializeField] private float walkSpeed;
    [SerializeField] private float sprintSpeed;
    [SerializeField] private bool isSprint;

    private Rigidbody rigid;
    private Animator anim;
    private TPSPlayerActions tpsPlayerAction;
    [SerializeField] private float temp;
    [SerializeField] private bool isMove;

    // Start is called before the first frame update
    void Start()
    {
        InitInputAction();

        rigid = this.GetComponent<Rigidbody>();
        anim = this.GetComponent<Animator>();
    }

    void InitInputAction()
    {
        PlayerInputActions playerInputActions = new PlayerInputActions();
        tpsPlayerAction = playerInputActions.TPSPlayer;
        tpsPlayerAction.Enable();
        tpsPlayerAction.Jump.performed += Jump;
        tpsPlayerAction.Sprint.performed += Sprint;
        tpsPlayerAction.Movement.performed += Movement;
    }

    private void FixedUpdate()
    {
        isSprint = tpsPlayerAction.Sprint.ReadValue<float>() != 0;

        Vector2 inputVec = tpsPlayerAction.Movement.ReadValue<Vector2>();

        if (inputDevice == EnumInputDevice.Keyboard)
        {
            temp = Mathf.Clamp(temp + (isMove == true ? Time.deltaTime : -Time.deltaTime) , 0, 1);
            anim.SetFloat("vertical", Mathf.Lerp(0, 1, temp));
        }
        else if (inputDevice == EnumInputDevice.XboxController)
        {
            anim.SetFloat("vertical", inputVec.magnitude);
        }
    }

    public void Movement(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            isMove = !isMove;
        }
    }

    public void Sprint(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            isSprint = !isSprint;
        }
    }

    public void Jump(InputAction.CallbackContext context)
    {
        if(context.performed)
        {
            rigid.AddForce(Vector3.up * 5f, ForceMode.Impulse);
        }
    }
}
