using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;

public class IKFootPlacement : MonoBehaviour
{
    private Vector3 rightFootPosition, leftFootPosition, leftFootIKPosition, rightFootIKPosition;
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

    public string leftFootAnimVariableName = "LeftFootCurve";
    public string rightFootAnimVariableName = "RightFootCurve";

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

        //AdjustFeetTarget(ref rightFootPosition, HumanBodyBones.RightFoot);
        //AdjustFeetTarget(ref leftFootPosition, HumanBodyBones.LeftFoot);

        FeetPositionSolver(hideRightFoot.position + Vector3.up * heightFromGroundRaycast, ref rightFootIKPosition, ref rightFootIKRotation);
        FeetPositionSolver(hideLeftFoot.position + Vector3.up * heightFromGroundRaycast, ref leftFootIKPosition, ref leftFootIKRotation);
    }

    private void LateUpdate()
    {
        if (enableFeetIK == false) return;
        if (anim == null) return;

        //MoveRootHeight();

        rightFootIK.data.targetPositionWeight = 1;
        //anim.SetIKPositionWeight(AvatarIKGoal.RightFoot, 1);

        if (useProIKFeature)
        {
            rightFootIK.data.targetRotationWeight = anim.GetFloat(rightFootAnimVariableName);
            //anim.SetIKRotationWeight(AvatarIKGoal.RightFoot, anim.GetFloat(rightFootAnimVariableName));
        }

        MoveFeetToIKPoint(rightFootTarget, hideRightFoot, rightFootIKPosition, rightFootIKRotation, ref lastRightFootPositionY);

        leftFootIK.data.targetPositionWeight = 1;
        //anim.SetIKPositionWeight(AvatarIKGoal.LeftFoot, 1);

        if (useProIKFeature)
        {
            leftFootIK.data.targetRotationWeight = anim.GetFloat(leftFootAnimVariableName);
            //anim.SetIKRotationWeight(AvatarIKGoal.LeftFoot, anim.GetFloat(leftFootAnimVariableName));
        }

        MoveFeetToIKPoint(leftFootTarget, hideLeftFoot, leftFootIKPosition, leftFootIKRotation, ref lastLeftFootPositionY);
    }

    private void OnAnimatorIK(int layerIndex)
    {
        //MovePelvisHeight();
        //if (enableFeetIK == false) return;
        //if (anim == null) return;

        //MovePelvisHeight();

        //rightFootIK.data.targetPositionWeight = 1;
        ////anim.SetIKPositionWeight(AvatarIKGoal.RightFoot, 1);

        //if(useProIKFeature)
        //{
        //    rightFootIK.data.targetRotationWeight = anim.GetFloat(rightFootAnimVariableName);
        //    //anim.SetIKRotationWeight(AvatarIKGoal.RightFoot, anim.GetFloat(rightFootAnimVariableName));
        //}

        //MoveFeetToIKPoint(rightFootTarget, hideRightFoot, rightFootIKPosition, rightFootIKRotation, ref lastRightFootPositionY);

        //leftFootIK.data.targetPositionWeight = 1;
        ////anim.SetIKPositionWeight(AvatarIKGoal.LeftFoot, 1);

        //if (useProIKFeature)
        //{
        //    leftFootIK.data.targetRotationWeight = anim.GetFloat(leftFootAnimVariableName);
        //    //anim.SetIKRotationWeight(AvatarIKGoal.LeftFoot, anim.GetFloat(leftFootAnimVariableName));
        //}

        //MoveFeetToIKPoint(leftFootTarget, hideLeftFoot, leftFootIKPosition, leftFootIKRotation, ref lastLeftFootPositionY);
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

    //private void MoveFeetToIKPoint(AvatarIKGoal foot, Vector3 positionIKHolder, Quaternion rotationIKHolder, ref float lastFootPositionY)
    //{
    //    Vector3 targetIKPosition = anim.GetIKPosition(foot);

    //    if(positionIKHolder != Vector3.zero)
    //    {
    //        targetIKPosition = transform.InverseTransformPoint(targetIKPosition);
    //        positionIKHolder = transform.InverseTransformPoint(positionIKHolder);

    //        float yVariable = Mathf.Lerp(lastFootPositionY, positionIKHolder.y, feetToIKPositionSpeed);
    //        targetIKPosition.y += yVariable;

    //        lastFootPositionY = yVariable;

    //        targetIKPosition = transform.TransformPoint(targetIKPosition);

    //        anim.SetIKRotation(foot, rotationIKHolder);
    //    }

    //    anim.SetIKPosition(foot, targetIKPosition);
    //}

    public Vector3 MoveRootHeight()
    {
        if(rightFootIKPosition == Vector3.zero || leftFootIKPosition == Vector3.zero || lastRootPositionY == 0)
        {
            lastRootPositionY = transform.position.y;
            return transform.position;
        }

        float lOffsetPosition = leftFootIKPosition.y - transform.position.y;
        float rOffsetPosition = rightFootIKPosition.y - transform.position.y;

        float totalOffset = (lOffsetPosition < rOffsetPosition) ? lOffsetPosition : rOffsetPosition;

        Vector3 newRootPosition = transform.position + Vector3.up * totalOffset;

        newRootPosition.y = Mathf.Lerp(lastRootPositionY, newRootPosition.y, rootUpAndDownSpeed);

        lastRootPositionY = transform.position.y;

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

        feetIKPositions = Vector3.zero;
    }

    //private void AdjustFeetTarget (ref Vector3 feetPositions, HumanBodyBones foot)
    //{
    //    feetPositions = anim.GetBoneTransform(foot).position;
    //    feetPositions.y = transform.position.y + heightFromGroundRaycast;
    //}

    [SerializeField] private TwoBoneIKConstraint rightFootIK;
    [SerializeField] private Transform originRightFoot;
    [SerializeField] private Transform hideRightFoot;
    [SerializeField] private Transform rightFootTarget;

    [SerializeField] private TwoBoneIKConstraint leftFootIK;
    [SerializeField] private Transform originLeftFoot;
    [SerializeField] private Transform hideLeftFoot;
    [SerializeField] private Transform leftFootTarget;

    //[SerializeField] private Vector3 rayOffset;
    //[SerializeField] private float rayDistance;
    //[SerializeField] private float footPosOffsetY;


    //private void LateUpdate()
    //{
    //    RaycastHit hit;

    //    if (Physics.Raycast(hideRightFoot.position + transform.rotation * rayOffset, Vector3.down, out hit, rayDistance,  1 << LayerMask.NameToLayer("Terrain")))
    //    {
    //        float rightToeOffset = hit.point.y - originRightFoot.GetChild(0).GetChild(0).position.y;

    //        rightFootTarget.rotation = Quaternion.Lerp(rightFootTarget.rotation, Quaternion.LookRotation(Vector3.Cross(hideRightFoot.right, hit.normal)) * Quaternion.Euler(hideRightFoot.eulerAngles.x, 0, 0), Time.deltaTime * 10);
    //        originRightFoot.GetChild(0).rotation = Quaternion.LookRotation(Vector3.Cross(hideRightFoot.GetChild(0).right, hit.normal));
    //        rightFootTarget.position = Vector3.Lerp(rightFootTarget.position, hit.point + Vector3.up * (footPosOffsetY + rightToeOffset), Time.deltaTime * 10);

    //        rightFootIK.weight = Mathf.Lerp(rightFootIK.weight, 1, Time.deltaTime * 10);
    //    }
    //    else
    //    {
    //        rightFootTarget.rotation = Quaternion.Lerp(rightFootTarget.rotation, hideRightFoot.rotation, Time.deltaTime * 10);
    //        rightFootTarget.position = Vector3.Lerp(rightFootTarget.position, hideRightFoot.position, Time.deltaTime * 7);

    //        rightFootIK.weight = Mathf.Lerp(rightFootIK.weight, 0, Time.deltaTime * 10);
    //    }

    //    if (Physics.Raycast(hideLeftFoot.position + transform.rotation * rayOffset, Vector3.down, out hit, rayDistance, 1 << LayerMask.NameToLayer("Terrain")))
    //    {
    //        float leftToeOffset = hit.point.y - originLeftFoot.GetChild(0).GetChild(0).position.y;

    //        leftFootTarget.rotation = Quaternion.Lerp(leftFootTarget.rotation, Quaternion.LookRotation(Vector3.Cross(hideLeftFoot.right, hit.normal)) * Quaternion.Euler(hideLeftFoot.eulerAngles.x, 0, 0), Time.deltaTime * 10);
    //        originLeftFoot.GetChild(0).rotation = Quaternion.LookRotation(Vector3.Cross(hideLeftFoot.GetChild(0).right, hit.normal));
    //        leftFootTarget.position = Vector3.Lerp(leftFootTarget.position, hit.point + Vector3.up * (footPosOffsetY + leftToeOffset), Time.deltaTime * 10);

    //        leftFootIK.weight = Mathf.Lerp(leftFootIK.weight, 1, Time.deltaTime * 10);
    //    }
    //    else
    //    {
    //        leftFootTarget.rotation = Quaternion.Lerp(leftFootTarget.rotation, hideLeftFoot.rotation, Time.deltaTime * 10);
    //        leftFootTarget.position = Vector3.Lerp(leftFootTarget.position, hideLeftFoot.position, Time.deltaTime * 7);

    //        leftFootIK.weight = Mathf.Lerp(leftFootIK.weight, 0, Time.deltaTime * 10);
    //    }
    //}
}
