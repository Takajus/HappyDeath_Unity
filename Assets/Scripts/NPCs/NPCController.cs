using System.Collections;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.AI;

public class NPCController : MonoBehaviour
{
    NavMeshAgent agent;
    public float range = 40f;
    public bool shouldMove;
    private bool canMove = true;
    private float pauseTimeAmount = 0f;

    public Transform areaCenter;

    float timerForceChangeTarget = 5f;

    Animator animator;

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        areaCenter = GameObject.Find("AreaCenter").transform;
        shouldMove = true;
        //StartCoroutine(MoveAndWait());
    }
    void Update()
    {
        if (shouldMove)
        {
/*            if (timerForceChangeTarget > 0)
            {
                timerForceChangeTarget -= Time.deltaTime;
            }
            else
            {
                Vector3 point;
                animator.SetBool("Walking", false);

                if (pauseTimeAmount >= 0)
                {
                    //isMoving = false;
                    Debug.Log(pauseTimeAmount);
                    pauseTimeAmount -= Time.deltaTime;
                }
                else if (RandomPoint(range, out point))
                {
                    Debug.DrawRay(point, Vector3.up, Color.blue, 1.0f);
                    //agent.SetDestination(point);
                    //isMoving = true;
                    StartCoroutine(RotateTowards(point));
                    pauseTimeAmount = 2f;
                }
                timerForceChangeTarget = 5f;
            }*/
            //animator.SetBool("Walking", isMoving);
            if (agent.remainingDistance <= agent.stoppingDistance && canMove)
            {
                Vector3 point;
                animator.SetBool("Walking", false);

                if (pauseTimeAmount >= 0)
                {
                    //isMoving = false;
                    Debug.Log(pauseTimeAmount);
                    pauseTimeAmount -= Time.deltaTime;
                }
                else if (RandomPoint(range, out point))
                {
                    Debug.DrawRay(point, Vector3.up, Color.blue, 1.0f);
                    //agent.SetDestination(point);
                    //isMoving = true;
                    StartCoroutine(RotateTowards(point));
                    pauseTimeAmount = 2f;
                }
                /*else if (RandomPoint(areaCenter.position, range, out point))
                {
                    Debug.DrawRay(point, Vector3.up, Color.blue, 1.0f);
                    //agent.SetDestination(point);
                    //isMoving = true;
                    StartCoroutine(RotateTowards(point));
                    pauseTimeAmount = 2f;
                }*/
            }
        }
    }
    /*bool RandomPoint(Vector3 center, float range, out Vector3 result)
    {
        Vector3 randomPoint = center + UnityEngine.Random.insideUnitSphere * range;
        NavMeshHit hit;
        if (NavMesh.SamplePosition(randomPoint, out hit, 1.0f, NavMesh.AllAreas))
        {
            result = hit.position;
            return true;
        }

        result = Vector3.zero;
        return false;
    }*/
    
    bool RandomPoint(float range, out Vector3 result)
    {
        Vector3 randomPoint = UnityEngine.Random.insideUnitSphere * range;
        NavMeshHit hit;
        if (NavMesh.SamplePosition(randomPoint, out hit, 1.0f, NavMesh.AllAreas))
        {
            result = hit.position;
            return true;
        }

        result = Vector3.zero;
        return false;
    }

    IEnumerator RotateTowards(Vector3 point)
    {
        canMove = false;
        float turnSpeed = 3;

        Vector3 direction = (new Vector3(point.x, transform.position.y, point.z) - transform.position).normalized;
        float elaspedTime = 0;
        float duration = Mathf.Abs(Vector3.Angle(transform.forward, direction) / 180) * turnSpeed;

        Quaternion startRotation = transform.rotation;
        Quaternion endRotation = Quaternion.LookRotation(direction, Vector3.up);


        animator.SetBool("Walking", true);
        while (elaspedTime < duration) 
        {
            float percentage = Mathf.SmoothStep(0, 1 ,elaspedTime / duration);

            transform.rotation = Quaternion.Lerp(startRotation, endRotation, percentage);

            elaspedTime += Time.deltaTime;
            yield return null;
        }

        agent.SetDestination(point);
        canMove = true;
        yield return null;
    }
}
