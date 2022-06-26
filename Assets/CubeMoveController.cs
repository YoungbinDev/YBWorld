using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeMoveController : MonoBehaviour
{
    [SerializeField] private float displacement;
    [SerializeField] private float initialVelocity;
    [SerializeField] private float finalVelocity = 0;
    [SerializeField] private float acceleration = Physics.gravity.y;
    [SerializeField] private float time;

    private Rigidbody rigid;

    // Start is called before the first frame update
    void Start()
    {
        rigid = this.GetComponent<Rigidbody>();

        time = Mathf.Sqrt(2 * displacement);
        initialVelocity = (displacement / time) - ((acceleration * time) / 2);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            time = Mathf.Sqrt(2 * displacement);
            initialVelocity = (displacement / time) - ((acceleration * time) / 2);
            rigid.AddForce(new Vector3(0, initialVelocity, 0), ForceMode.Impulse);
        }
    }
}
