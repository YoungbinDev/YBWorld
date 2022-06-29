using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileThrower : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private GameObject projectile;

    [SerializeField] private Vector3 acceleration;
    [SerializeField] private float initialVelocityX;
    [SerializeField] private float initialVelocityY;
    [SerializeField] private float initialVelocityZ;
    [SerializeField] private float time;


    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                GameObject temp = Instantiate(projectile, transform.position, Quaternion.identity);
                temp.GetComponent<Rigidbody>().AddForce(CalculateVelocity(transform.position, target.position), ForceMode.Impulse);
                temp.GetComponent<Projectile>().target = target;
            }
        }
    }

    Vector3 CalculateVelocity(Vector3 startPos, Vector3 endPos)
    {
        Vector3 distance = endPos - startPos;

        initialVelocityX = distance.x / time - (acceleration.x * time) / 2;
        initialVelocityY = distance.y / time - (acceleration.y * time) / 2;
        initialVelocityZ = distance.z / time - (acceleration.z * time) / 2;

        Vector3 result = new Vector3(initialVelocityX, initialVelocityY, initialVelocityZ);

        return result;
    }
}
