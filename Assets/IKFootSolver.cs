using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IKFootSolver : MonoBehaviour
{
    [SerializeField] private Transform root;
    [SerializeField] private Transform body;
    [SerializeField] private Transform foot;
    [SerializeField] private float footSpacing;
    [SerializeField] private LayerMask terrainLayer;

    [SerializeField] private Vector3 offsetPos;
    private Quaternion offsetRot;

    private void Start()
    {
        offsetRot = foot.rotation;
        offsetPos = foot.position - transform.position;
        transform.rotation = offsetRot;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        Ray ray = new Ray(body.position + new Vector3(offsetPos.x, 0, offsetPos.z), Vector3.down);

        if (Physics.Raycast(ray, out RaycastHit hit, 1f, terrainLayer.value))
        {
            transform.position = hit.point + new Vector3(0, offsetPos.y, 0);

            //if (Mathf.Abs(Vector3.Cross(root.up, hit.normal).x) < 0.5f)
            //{
            //    transform.rotation = Quaternion.LookRotation(Vector3.Cross(root.right, hit.normal)) * offsetRot;
            //}

            transform.rotation = Quaternion.LookRotation(Vector3.Cross(root.right, hit.normal)) * offsetRot;
        }
    }
}
