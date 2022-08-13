using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;

public class IKFootPlacement : MonoBehaviour
{
    [SerializeField] private Transform originRightFoot;
    [SerializeField] private Transform hideRightFoot;
    [SerializeField] private Transform rightFootTarget;
    
    [SerializeField] private float rayOffsetY;
    [SerializeField] private float footPosOffsetY;

    private void LateUpdate()
    {
        RaycastHit hit;
        if(Physics.Raycast(hideRightFoot.position + Vector3.up * rayOffsetY, Vector3.down, out hit, 0.5f,  1 << LayerMask.NameToLayer("Terrain")))
        {
            rightFootTarget.position = Vector3.Lerp(rightFootTarget.position, hit.point + Vector3.up * footPosOffsetY, Time.deltaTime * 10);
            rightFootTarget.up = hit.normal;
            //originRightFoot.GetChild(0).up = hit.normal;
        }
        else
        {
            rightFootTarget.position = Vector3.Lerp(rightFootTarget.position, hideRightFoot.position + Vector3.up * footPosOffsetY, Time.deltaTime * 7);
            //rightFootTarget.up = Vector3.Lerp(rightFootTarget.up, hideRightFoot.up, Time.deltaTime * 12);
            //originRightFoot.GetChild(0).up = Vector3.Lerp(rightFootTarget.up, hideRightFoot.GetChild(0).up, Time.deltaTime * 12);
        }
    }
}
