using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProceduralAnimationController : MonoBehaviour
{
    [SerializeField] private LayerMask terrainLayer;
    [SerializeField] private Transform body;
    [SerializeField] private Transform leftFootTarget;
    [SerializeField] private Transform rightFootTarget;
    public Transform frontFoot;
    [SerializeField] private float stepHeight;
    public Vector3 centerOfMass;
    [SerializeField] private IKFootSolver leftFootIK;
    [SerializeField] private IKFootSolver rightFootIK;

    private Vector3 targetPos;
    private Vector3 offsetPos;
    private Vector3 tempPos;

    private Animator anim;
    private Rigidbody rigid;

    [HideInInspector]
    public Coroutine currentRootCoroutine;
    [HideInInspector]
    public Coroutine currentLegCoroutine;

    public bool isWalk = false;

    // Start is called before the first frame update
    void Start()
    {
        offsetPos = body.transform.position - transform.position;
        anim = this.GetComponent<Animator>();
        rigid = this.GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.W) && currentRootCoroutine == null && currentLegCoroutine == null)
        {
            isWalk = true;
            //this.transform.Translate(this.transform.forward * Time.deltaTime);
        }
        else
        {
            isWalk = false;
        }

        centerOfMass = (leftFootTarget.position + rightFootTarget.position + body.position) / 3;

        anim.SetBool("isWalk", isWalk);
    }

    private void FixedUpdate()
    {
        if(isWalk)
        {
            rigid.velocity = Vector3.Lerp(rigid.velocity, rigid.velocity + transform.forward, Time.deltaTime * 10);
        }
        else
        {
            rigid.velocity = Vector3.Lerp(rigid.velocity, Vector3.zero, Time.deltaTime * 9);
        }
    }

    // Update is called once per frame
    void LateUpdate()
    {
        Ray ray = new Ray(transform.position + offsetPos + transform.forward * 0.3f , Vector3.down);
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
                        if (leftFootDis > stepHeight / 2 || rightFootDis > stepHeight / 2)
                        {
                            tempPos = transform.position;
                            //targetPos = new Vector3(transform.position.x, 0, transform.position.z);
                            targetPos = hit.point;
                            currentRootCoroutine = StartCoroutine(MoveTo(tempPos, targetPos, 0.15f));
                        }
                        else
                        {
                            //transform.position = new Vector3(transform.position.x, hit.point.y, transform.position.z);
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
                            //transform.position = new Vector3(transform.position.x, hit.point.y, transform.position.z);
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

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(centerOfMass, 0.05f);
    }
}
