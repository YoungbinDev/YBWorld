using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowObject : MonoBehaviour
{
    public Transform followPos;

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.position = followPos.position;
    }
}
