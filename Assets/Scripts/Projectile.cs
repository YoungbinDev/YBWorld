using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private Vector3 acceleration;

    private Rigidbody rigid => this.GetComponent<Rigidbody>();

    private void FixedUpdate()
    {
        acceleration = GameManager.Instance.weatherManager.worldAcceleration;

        rigid.AddForce(acceleration);
    }
}
