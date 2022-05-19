using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IKFootSolver : MonoBehaviour
{
    [SerializeField] private AnimationCurve footAnimY;
    [SerializeField] private Transform root;
    [SerializeField] private Transform body;
    [SerializeField] private Transform foot;
    [SerializeField] private float footStepDistance;
    [SerializeField] private LayerMask terrainLayer;
    [SerializeField] private bool isFirstStep;

    private Vector3 offsetPos;
    private Quaternion offsetRot;

    private Vector3 originPos;
    private Vector3 targetPos;

    private Quaternion originRot;
    private Quaternion targetRot;

    [SerializeField] private float dis;
    private void Start()
    {
        originPos = foot.position;
        targetPos = originPos;

        originRot = foot.rotation;
        targetRot = originRot;

        offsetRot = foot.rotation;
        offsetPos = foot.position - transform.position;

        transform.position = targetPos;
        transform.rotation = targetRot;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        Ray ray = new Ray(body.position + root.rotation * new Vector3(offsetPos.x, 0, offsetPos.z + (isFirstStep ? footStepDistance * 0.7f : footStepDistance)), Vector3.down);
        Debug.DrawRay(body.position + root.rotation * new Vector3(offsetPos.x, 0, offsetPos.z + footStepDistance), Vector3.down, Color.red);

        if (Physics.Raycast(ray, out RaycastHit hit, 1f, terrainLayer.value))
        {
            dis = Vector3.Distance(targetPos, hit.point + new Vector3(0, offsetPos.y, 0));
            Debug.DrawRay(targetPos, hit.point - targetPos, Color.blue);

            if (isFirstStep)
            {
                if (Vector3.Distance(targetPos, hit.point + new Vector3(0, offsetPos.y, 0)) >= footStepDistance)
                {
                    targetPos = hit.point + new Vector3(0, offsetPos.y, 0);
                    targetRot = Quaternion.LookRotation(Vector3.Cross(root.right, hit.normal)) * offsetRot;

                    isFirstStep = false;
                }
            }
            else
            {
                if (Vector3.Distance(targetPos, hit.point) >= footStepDistance * 2)
                {
                    targetPos = hit.point + new Vector3(0, offsetPos.y, 0);
                    targetRot = Quaternion.LookRotation(Vector3.Cross(root.right, hit.normal)) * offsetRot;
                }
            }
        }
        else
        {
            targetPos = originPos;
            targetRot = originRot;

            isFirstStep = true;
        }

        transform.position = Vector3.Lerp(transform.position, targetPos, Time.deltaTime * 10);
        transform.rotation = targetRot;
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(targetPos, 0.05f);
    }
}
