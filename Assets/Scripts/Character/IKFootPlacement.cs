using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;

public class IKFootPlacement : MonoBehaviour
{
    [Header("Feet Transform")]
    [SerializeField] private TwoBoneIKConstraint rightFootIK;
    [SerializeField] private Transform hideRightFoot;
    [SerializeField] private Transform rightFootTarget;

    [SerializeField] private TwoBoneIKConstraint leftFootIK;
    [SerializeField] private Transform hideLeftFoot;
    [SerializeField] private Transform leftFootTarget;

    private Vector3 leftFootIKPosition, rightFootIKPosition;
    private Quaternion leftFootIKRotation, rightFootIKRotation;
    private float lastRootPositionY, lastRightFootPositionY, lastLeftFootPositionY;

    [Header("Feet Grounder")]
    public bool enableFeetIK = true;
    [Range(0, 2), SerializeField] private float heightFromGroundRaycast = 1.14f;
    [Range(0, 2), SerializeField] private float raycastDownDistance = 1.5f;
    [SerializeField] private LayerMask enviromentLayer;
    [SerializeField] private float rootOffset = 0f;
    [Range(0, 1), SerializeField] private float rootUpAndDownSpeed = 0.28f;
    [Range(0, 1), SerializeField] private float feetToIKPositionSpeed = 0.5f;

    public string leftFootAnimVariableName = "IKLeftFootWeight";
    public string rightFootAnimVariableName = "IKRightFootWeight";

    public bool useProIKFeature = false;
    public bool showSolverDebug = true;

    private Animator anim;

    private void Start()
    {
        anim = this.GetComponent<Animator>();
    }

    private void FixedUpdate()
    {
        if (enableFeetIK == false) return;
        if (anim == null) return;

        FeetPositionSolver(new Vector3(hideRightFoot.position.x, transform.position.y, hideRightFoot.position.z) + Vector3.up * heightFromGroundRaycast, ref rightFootIKPosition, ref rightFootIKRotation);
        FeetPositionSolver(new Vector3(hideLeftFoot.position.x, transform.position.y, hideLeftFoot.position.z) + Vector3.up * heightFromGroundRaycast, ref leftFootIKPosition, ref leftFootIKRotation);

        //FeetPositionSolver(hideRightFoot.position + Vector3.up * heightFromGroundRaycast, ref rightFootIKPosition, ref rightFootIKRotation);
        //FeetPositionSolver(hideLeftFoot.position + Vector3.up * heightFromGroundRaycast, ref leftFootIKPosition, ref leftFootIKRotation);
    }

    private void LateUpdate()
    {
        if (enableFeetIK == false) return;
        if (anim == null) return;

        //rightFootIK.data.targetPositionWeight = 1;

        if (useProIKFeature)
        {
            rightFootIK.data.targetPositionWeight = anim.GetFloat(rightFootAnimVariableName);
            rightFootIK.data.targetRotationWeight = anim.GetFloat(rightFootAnimVariableName);
        }

        MoveFeetToIKPoint(rightFootTarget, hideRightFoot, rightFootIKPosition, rightFootIKRotation, ref lastRightFootPositionY);

        //leftFootIK.data.targetPositionWeight = 1;

        if (useProIKFeature)
        {
            leftFootIK.data.targetPositionWeight = anim.GetFloat(leftFootAnimVariableName);
            leftFootIK.data.targetRotationWeight = anim.GetFloat(leftFootAnimVariableName);
        }

        MoveFeetToIKPoint(leftFootTarget, hideLeftFoot, leftFootIKPosition, leftFootIKRotation, ref lastLeftFootPositionY);
    }

    private void MoveFeetToIKPoint(Transform footTarget, Transform hideFoot, Vector3 positionIKHolder, Quaternion rotationIKHolder, ref float lastFootPositionY)
    {
        Vector3 targetIKPosition = hideFoot.position;

        if (positionIKHolder != Vector3.zero)
        {
            targetIKPosition = transform.InverseTransformPoint(targetIKPosition);
            positionIKHolder = transform.InverseTransformPoint(positionIKHolder);

            float yVariable = Mathf.Lerp(lastFootPositionY, positionIKHolder.y, feetToIKPositionSpeed);
            targetIKPosition.y += yVariable;

            lastFootPositionY = yVariable;

            targetIKPosition = transform.TransformPoint(targetIKPosition);

            footTarget.rotation = rotationIKHolder;
        }

        footTarget.position = targetIKPosition;
    }

    public Vector3 GetResultRootPosition()
    {
        if (rightFootIKPosition == Vector3.zero || leftFootIKPosition == Vector3.zero || lastRootPositionY == 0)
        {
            lastRootPositionY = transform.position.y;
            return transform.position;
        }

        float lOffsetPosition = leftFootIKPosition.y - transform.position.y;
        float rOffsetPosition = rightFootIKPosition.y - transform.position.y;

        float totalOffset = (lOffsetPosition < rOffsetPosition) ? lOffsetPosition : rOffsetPosition;

        Vector3 newRootPosition = transform.position + Vector3.up * totalOffset;

        newRootPosition.y = Mathf.Lerp(lastRootPositionY, newRootPosition.y, rootUpAndDownSpeed);

        lastRootPositionY = newRootPosition.y;

        return newRootPosition;
    }

    private void FeetPositionSolver(Vector3 fromSkyPosition, ref Vector3 feetIKPositions, ref Quaternion feetIKRotations)
    {
        RaycastHit hit;

        if (showSolverDebug)
            Debug.DrawLine(fromSkyPosition, fromSkyPosition + Vector3.down * (raycastDownDistance + heightFromGroundRaycast), Color.yellow);

        if(Physics.Raycast(fromSkyPosition, Vector3.down, out hit, raycastDownDistance + heightFromGroundRaycast, enviromentLayer))
        {
            feetIKPositions = fromSkyPosition;
            feetIKPositions.y = hit.point.y + rootOffset;
            feetIKRotations = Quaternion.FromToRotation(Vector3.up, hit.normal) * transform.rotation;

            return;
        }

        Debug.Log("test");
        feetIKPositions = Vector3.zero;
    }
}
