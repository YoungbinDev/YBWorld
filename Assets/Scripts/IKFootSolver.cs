using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IKFootSolver : MonoBehaviour
{
    public enum FootType
    {
        Left,
        Right
    }

    public FootType footType;
    [SerializeField] private LayerMask terrainLayer;
    [SerializeField] private Transform root;
    [SerializeField] private Transform body;
    [SerializeField] private Transform foot;

    [SerializeField] private bool isFirstStep;
    [SerializeField] private float footStepDistance;

    [HideInInspector]
    public Vector3 offsetPos;
    [HideInInspector]
    public Quaternion offsetRot;

    private Vector3 originPos;
    private Vector3 targetPos;
    private Vector3 tempPos;

    private Quaternion originRot;
    private Quaternion targetRot;

    private Coroutine currentCoroutine;

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

        if (Physics.Raycast(ray, out RaycastHit hit, 3f, terrainLayer.value))
        {
            Debug.DrawRay(targetPos, hit.point - targetPos, Color.blue);

            if (root.GetComponent<ProceduralAnimationController>().isWalk)
            {
                if (isFirstStep)
                {
                    if (Vector3.Distance(targetPos, hit.point + new Vector3(0, offsetPos.y, 0)) >= footStepDistance)
                    {
                        tempPos = targetPos;
                        targetPos = hit.point + new Vector3(0, offsetPos.y, 0);
                        targetRot = Quaternion.LookRotation(Vector3.Cross(root.right, hit.normal)) * offsetRot;

                        isFirstStep = false;

                        currentCoroutine = StartCoroutine(Walk(tempPos, targetPos, 0.15f));
                    }
                }
                else
                {
                    if (Vector3.Distance(targetPos, hit.point) >= footStepDistance * 2)
                    {
                        tempPos = targetPos;
                        targetPos = hit.point + new Vector3(0, offsetPos.y, 0);
                        targetRot = Quaternion.LookRotation(Vector3.Cross(root.right, hit.normal)) * offsetRot;

                        currentCoroutine = StartCoroutine(Walk(tempPos, targetPos, 0.15f));
                    }
                }
            }
        }
        else
        {
            tempPos = targetPos;
            targetPos = originPos;
            targetRot = originRot;

            isFirstStep = true;
        }

        if (currentCoroutine == null)
        {
            transform.position = targetPos;
            transform.rotation = targetRot;
        }
    }

    IEnumerator Walk(Vector3 currentPos, Vector3 targetPos, float timeToEnd)
    {
        float currentTime = 0;

        while (currentTime <= 1)
        {
            Vector3 resultPos = Vector3.Slerp(currentPos, targetPos, currentTime);

            transform.position = resultPos;
            transform.rotation = targetRot;

            currentTime += Time.deltaTime / timeToEnd;

            yield return null;
        }

        currentCoroutine = null;
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(targetPos, 0.05f);
    }
}
