using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProceduralAnimationController : MonoBehaviour
{
    [SerializeField] private LayerMask terrainLayer;
    [SerializeField] private Transform body;
    [SerializeField] private Transform leftFootTarget;
    [SerializeField] private Transform rightFootTarget;
    [SerializeField] private float stepHeight;

    private Vector3 targetPos;
    private Vector3 offsetPos;
    private Vector3 tempPos;

    private Animator anim;

    [HideInInspector]
    public Coroutine currentRootCoroutine;

    public bool isWalk = false;

    // Start is called before the first frame update
    void Start()
    {
        offsetPos = body.transform.position - transform.position;
        anim = this.GetComponent<Animator>();
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.W) && currentRootCoroutine == null)
        {
            isWalk = true;
            this.transform.Translate(this.transform.forward * Time.deltaTime);
        }
        else
            isWalk = false;

        anim.SetBool("isWalk", isWalk);
    }

    // Update is called once per frame
    void LateUpdate()
    {
        Ray ray = new Ray(transform.position + offsetPos + transform.forward * 0.3f, Vector3.down);
        Debug.DrawRay(transform.position + offsetPos + transform.forward * 0.3f, Vector3.down);

        float test1 = Vector3.Dot(transform.forward, (leftFootTarget.position - transform.position).normalized);
        float test2 = Vector3.Dot(transform.forward, (rightFootTarget.position - transform.position).normalized);

        float leftFootDis = Vector3.Distance(new Vector3(0, transform.position.y, 0), new Vector3(0, leftFootTarget.position.y, 0));
        float rightFootDis = Vector3.Distance(new Vector3(0, transform.position.y, 0), new Vector3(0, rightFootTarget.position.y, 0));

        if (Physics.Raycast(ray, out RaycastHit hit, 3f, terrainLayer.value))
        {
            if (currentRootCoroutine == null)
            {
                if (test1 > 0 || test2 > 0)
                {
                    if (transform.position.y > leftFootTarget.position.y || transform.position.y > rightFootTarget.position.y)
                    {
                        Debug.Log(leftFootDis);
                        Debug.Log(rightFootDis);

                        if (leftFootDis > stepHeight / 2 || rightFootDis > stepHeight / 2)
                        {
                            tempPos = transform.position;
                            //targetPos = new Vector3(transform.position.x, 0, transform.position.z);
                            targetPos = transform.position.y > leftFootTarget.position.y ? leftFootTarget.position - leftFootTarget.GetComponent<IKFootSolver>().offsetPos : rightFootTarget.position - rightFootTarget.GetComponent<IKFootSolver>().offsetPos;
                            currentRootCoroutine = StartCoroutine(MoveTo(tempPos, targetPos, 0.15f));
                        }
                        else
                        {
                            //transform.position = Vector3.Lerp(transform.position, new Vector3(transform.position.x, hit.point.y, transform.position.z), Time.deltaTime * 10);
                        }
                    }
                    else
                    {
                        if (leftFootDis > stepHeight && rightFootDis > stepHeight)
                        {
                            tempPos = transform.position;
                            targetPos = hit.point;
                            currentRootCoroutine = StartCoroutine(MoveTo(tempPos, targetPos, 0.15f));
                        }
                        else
                        {
                            //transform.position = Vector3.Lerp(transform.position, new Vector3(transform.position.x, hit.point.y, transform.position.z), Time.deltaTime * 10);
                        }
                    }
                }
            }
        }
    }

    IEnumerator MoveTo(Vector3 currentPos, Vector3 targetPos, float timeToEnd)
    {
        float currentTime = 0;

        while (currentTime <= 1)
        {
            Vector3 resultPos = Vector3.Slerp(currentPos, targetPos, currentTime);

            transform.position = resultPos;

            currentTime += Time.deltaTime / timeToEnd;

            yield return null;
        }

        currentRootCoroutine = null;
    }
}
