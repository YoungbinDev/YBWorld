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
        Ray ray = new Ray(body.position + root.rotation * new Vector3(offsetPos.x, 0, offsetPos.z + (isFirstStep ? footStepDistance : footStepDistance)), Vector3.down);
        Debug.DrawRay(body.position + root.rotation * new Vector3(offsetPos.x, 0, offsetPos.z + footStepDistance), Vector3.down, Color.red);

        if (Physics.Raycast(ray, out RaycastHit hit, 3f, terrainLayer.value))
        {
            Debug.DrawRay(targetPos, hit.point - targetPos, Color.blue);

            if(root.GetComponent<ProceduralAnimationController>().currentLegCoroutine == null && currentCoroutine == null)
            {
                if (root.GetComponent<ProceduralAnimationController>().isWalk)
                {
                    if (root.GetComponent<ProceduralAnimationController>().frontFoot != transform)
                    {
                        if (root.GetComponent<ProceduralAnimationController>().frontFoot != null)
                        {
                            if (Vector3.Dot(root.forward, (root.GetComponent<ProceduralAnimationController>().frontFoot.position - body.position).normalized) < 0)
                            {
                                tempPos = targetPos;
                                targetPos = hit.point + new Vector3(0, offsetPos.y, 0);
                                targetRot = Quaternion.LookRotation(Vector3.Cross(root.right, hit.normal)) * offsetRot;

                                currentCoroutine = StartCoroutine(Walk(tempPos, targetPos, 0.2f));
                                root.GetComponent<ProceduralAnimationController>().currentLegCoroutine = currentCoroutine;
                                root.GetComponent<ProceduralAnimationController>().frontFoot = transform;
                            }
                        }
                        else
                        {
                            tempPos = targetPos;
                            targetPos = hit.point + new Vector3(0, offsetPos.y, 0);
                            targetRot = Quaternion.LookRotation(Vector3.Cross(root.right, hit.normal)) * offsetRot;

                            currentCoroutine = StartCoroutine(Walk(tempPos, targetPos, 0.2f));
                            root.GetComponent<ProceduralAnimationController>().currentLegCoroutine = currentCoroutine;
                            root.GetComponent<ProceduralAnimationController>().frontFoot = transform;
                        }
                    }
                }
            }
        }
        else
        {
            tempPos = targetPos;
            targetPos = originPos;
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
