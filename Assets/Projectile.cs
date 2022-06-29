using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private float time;

    private bool isArrived = false;

    public Transform target;

    // Update is called once per frame
    void Update()
    {
        if(!isArrived)
        {
            time += Time.deltaTime;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(!isArrived && other.transform == target)
        {
            isArrived = true;

            this.GetComponent<Rigidbody>().velocity = Vector3.zero;
        }
    }
}
