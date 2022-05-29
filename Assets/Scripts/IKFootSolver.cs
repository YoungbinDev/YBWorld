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
    private ProceduralAnimationController rootControrller;

    private void Start()
    {
        originPos = foot.localPosition;
        targetPos = foot.TransformPoint(originPos);

        originRot = foot.rotation;
        targetRot = originRot;

        offsetRot = foot.rotation;
        offsetPos = foot.position - transform.position;

        transform.position = targetPos;
        transform.rotation = targetRot;

        rootControrller = root.GetComponent<ProceduralAnimationController>();
    }

    // Update is called once per frame
    void LateUpdate()
    {
        Ray ray = new Ray(body.position + root.rotation * root.GetComponent<Rigidbody>().velocity * root.GetComponent<ProceduralAnimationController>().footStepDistance, Vector3.down);
        Debug.DrawRay(body.position + root.rotation * root.GetComponent<Rigidbody>().velocity * root.GetComponent<ProceduralAnimationController>().footStepDistance, Vector3.down, Color.red);

        if (Physics.Raycast(ray, out RaycastHit hit, 3f, terrainLayer.value))
        {
            Debug.DrawRay(targetPos, hit.point - targetPos, Color.blue);

            if (rootControrller.currentLegCoroutine == null && currentCoroutine == null)
            {

                if (rootControrller.frontFoot != transform)
                {
                    if (rootControrller.frontFoot != null)
                    {
                        if (Vector3.Dot(root.forward, (rootControrller.frontFoot.position - body.position).normalized) < 0f)
                        {
                            tempPos = targetPos;
                            targetPos = hit.point + new Vector3(0, offsetPos.y, 0);
                            targetRot = Quaternion.LookRotation(Vector3.Cross(root.right, hit.normal)) * offsetRot;

                            currentCoroutine = StartCoroutine(Walk(tempPos, targetPos, 0.2f));
                            rootControrller.currentLegCoroutine = currentCoroutine;
                            rootControrller.frontFoot = transform;
                        }
                    }
                    else
                    {
                        tempPos = targetPos;
                        targetPos = hit.point + new Vector3(0, offsetPos.y, 0);
                        targetRot = Quaternion.LookRotation(Vector3.Cross(root.right, hit.normal)) * offsetRot;

                        currentCoroutine = StartCoroutine(Walk(tempPos, targetPos, 0.2f));
                        rootControrller.currentLegCoroutine = currentCoroutine;
                        rootControrller.frontFoot = transform;
                    }
                }

            }
        }
        else
        {
            tempPos = targetPos;
            targetPos = foot.TransformPoint(originPos);
            targetRot = originRot;
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
        root.GetComponent<ProceduralAnimationController>().currentLegCoroutine = null;
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(targetPos, 0.05f);
    }
}
