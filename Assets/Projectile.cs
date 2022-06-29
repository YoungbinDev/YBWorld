using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    private Transform target;
    private Vector3 acceleration;
    private Rigidbody rigid => this.GetComponent<Rigidbody>();

    public void Init(Transform target, Vector3 acceleration)
    {
        this.target = target;
        this.acceleration = acceleration;
    }

    private void FixedUpdate()
    {
        rigid.AddForce(acceleration);
    }
}
