using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NPCController : MonoBehaviour
{
    public NavMeshAgent agent;
    public float range;
    public bool shouldMove;
    private bool isMoving;
    private float pauseTimeAmount = 0f;

    public Transform areaCenter;

    [SerializeField] Animator animator;

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        //StartCoroutine(MoveAndWait());
    }
    void Update()
    {
        if (shouldMove)
        {
            if (isMoving)
                animator.SetBool("Walking", true);
            else
                animator.SetBool("Walking", false);

            if (agent.remainingDistance <= agent.stoppingDistance)
            {
                Vector3 point;

                if (pauseTimeAmount >= 0)
                {
                    isMoving = false;
                    Debug.Log(pauseTimeAmount);
                    pauseTimeAmount -= Time.deltaTime;
                }
                else if (RandomPoint(areaCenter.position, range, out point))
                {
                    Debug.DrawRay(point, Vector3.up, Color.blue, 1.0f);
                    agent.SetDestination(point);
                    isMoving = true;
                    pauseTimeAmount = 2f;
                }
            }
        }
    }
    bool RandomPoint(Vector3 center, float range, out Vector3 result)
    {
        Vector3 randomPoint = center + Random.insideUnitSphere * range;
        NavMeshHit hit;
        if (NavMesh.SamplePosition(randomPoint, out hit, 1.0f, NavMesh.AllAreas))
        {
            result = hit.position;
            return true;
        }

        result = Vector3.zero;
        return false;
    }
}
