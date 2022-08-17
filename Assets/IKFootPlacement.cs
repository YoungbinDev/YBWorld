using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;

public class IKFootPlacement : MonoBehaviour
{
    [SerializeField] private TwoBoneIKConstraint rightFootIK;
    [SerializeField] private Transform originRightFoot;
    [SerializeField] private Transform hideRightFoot;
    [SerializeField] private Transform rightFootTarget;

    [SerializeField] private TwoBoneIKConstraint leftFootIK;
    [SerializeField] private Transform originLeftFoot;
    [SerializeField] private Transform hideLeftFoot;
    [SerializeField] private Transform leftFootTarget;

    [SerializeField] private Vector3 rayOffset;
    [SerializeField] private float rayDistance;
    [SerializeField] private float footPosOffsetY;


    private void LateUpdate()
    {
        RaycastHit hit;

        if (Physics.Raycast(hideRightFoot.position + transform.rotation * rayOffset, Vector3.down, out hit, rayDistance,  1 << LayerMask.NameToLayer("Terrain")))
        {
            float rightToeOffset = hit.point.y - originRightFoot.GetChild(0).GetChild(0).position.y;

            rightFootTarget.rotation = Quaternion.Lerp(rightFootTarget.rotation, Quaternion.LookRotation(Vector3.Cross(hideRightFoot.right, hit.normal)) * Quaternion.Euler(hideRightFoot.eulerAngles.x, 0, 0), Time.deltaTime * 10);
            originRightFoot.GetChild(0).rotation = Quaternion.LookRotation(Vector3.Cross(hideRightFoot.GetChild(0).right, hit.normal));
            rightFootTarget.position = Vector3.Lerp(rightFootTarget.position, hit.point + Vector3.up * (footPosOffsetY + rightToeOffset), Time.deltaTime * 10);

            rightFootIK.weight = Mathf.Lerp(rightFootIK.weight, 1, Time.deltaTime * 10);
        }
        else
        {
            rightFootTarget.rotation = Quaternion.Lerp(rightFootTarget.rotation, hideRightFoot.rotation, Time.deltaTime * 10);
            rightFootTarget.position = Vector3.Lerp(rightFootTarget.position, hideRightFoot.position, Time.deltaTime * 7);

            rightFootIK.weight = Mathf.Lerp(rightFootIK.weight, 0, Time.deltaTime * 10);
        }

        if (Physics.Raycast(hideLeftFoot.position + transform.rotation * rayOffset, Vector3.down, out hit, rayDistance, 1 << LayerMask.NameToLayer("Terrain")))
        {
            float leftToeOffset = hit.point.y - originLeftFoot.GetChild(0).GetChild(0).position.y;

            leftFootTarget.rotation = Quaternion.Lerp(leftFootTarget.rotation, Quaternion.LookRotation(Vector3.Cross(hideLeftFoot.right, hit.normal)) * Quaternion.Euler(hideLeftFoot.eulerAngles.x, 0, 0), Time.deltaTime * 10);
            originLeftFoot.GetChild(0).rotation = Quaternion.LookRotation(Vector3.Cross(hideLeftFoot.GetChild(0).right, hit.normal));
            leftFootTarget.position = Vector3.Lerp(leftFootTarget.position, hit.point + Vector3.up * (footPosOffsetY + leftToeOffset), Time.deltaTime * 10);

            leftFootIK.weight = Mathf.Lerp(leftFootIK.weight, 1, Time.deltaTime * 10);
        }
        else
        {
            leftFootTarget.rotation = Quaternion.Lerp(leftFootTarget.rotation, hideLeftFoot.rotation, Time.deltaTime * 10);
            leftFootTarget.position = Vector3.Lerp(leftFootTarget.position, hideLeftFoot.position, Time.deltaTime * 7);

            leftFootIK.weight = Mathf.Lerp(leftFootIK.weight, 0, Time.deltaTime * 10);
        }
    }
}
