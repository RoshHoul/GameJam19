using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAgent : MonoBehaviour {

    GameObject[] waypoints;
    List<GameObject> visitedWaypoints = new List<GameObject>();

    public AgentState currentState, prevState;
    GameObject target;

    public GameObject player;
    NavMeshAgent agent;

    Animator anim;

    private void Start()
    {
        anim = GetComponent<Animator>();

        if (waypoints == null)
        {
            waypoints = GameObject.FindGameObjectsWithTag("Waypoint");
        }

        if (agent == null)
        {
            agent = GetComponent<NavMeshAgent>();
        }

        currentState = AgentState.Patrolling;
        prevState = AgentState.Idle;

        GameObject tempTarget = TakeRandomWaypoint();

        if (tempTarget != null && IsValidWaypoint(tempTarget))
        {
            target = tempTarget;
        }

    }

    // Update is called once per frame
    void Update () {
		if (prevState != currentState)
        {
            //currentState = prevState;
            if (currentState == AgentState.Patrolling)
            {
                Debug.Log("Target is: " + target.name);
                agent.speed = 3.5f;
                agent.SetDestination(target.transform.position);
                anim.SetBool("startWalking", true);
                //Debug.Log("Dist is " + Vector3.Distance(agent.destination, agent.transform.position));
                if (IsPathReached())
                {
                    Debug.Log(target.name + " reached");
                    prevState = currentState;
                    currentState = AgentState.Idle;
                    target = null;
                }

            }
            else if (currentState == AgentState.Idle)
            {
                //execute idle animations
                anim.SetBool("startWalking", false);
                anim.SetInteger("idleRand", RandomAnimation());
                StartCoroutine(WaitForAnimation());

                target = TakeRandomWaypoint();
                if (target != null && IsValidWaypoint(target))
                {
                    prevState = currentState;
                    currentState = AgentState.Patrolling;

                }
                Debug.Log("druga animaciq beibe");
            }
            if (IsPlayerInReach())
            {
                //    Debug.Log("TAPANAR GUBISH");
                target = player;
            }
        }
	}

    private IEnumerator WaitForAnimation()
    {
        agent.isStopped = true;
        yield return new WaitForSeconds(anim.GetCurrentAnimatorStateInfo(0).length + anim.GetCurrentAnimatorStateInfo(0).normalizedTime);
        agent.isStopped = false;
    }

    private int RandomAnimation()
    {
        int i = UnityEngine.Random.Range(0, 100);

        return i % 2;
    }

    private GameObject TakeRandomWaypoint()
    {
        UnityEngine.Random rand = new UnityEngine.Random();
        GameObject target = waypoints[UnityEngine.Random.Range(0, waypoints.Length)];

        return target;
    }

    private bool IsValidWaypoint(GameObject waypoint)
    {
        if (visitedWaypoints.Contains(waypoint))
        {
            return false;
        }
        return true;
    }

    private bool IsPathReached()
    {
        if (Vector3.Distance(agent.destination, agent.transform.position) <= agent.stoppingDistance)
        {
            if (!agent.hasPath || agent.velocity.sqrMagnitude == 0f)
            {
                return true;
            }
        }

        return false;
    }

    private bool IsPlayerInReach()
    {
        Vector3 dir = player.transform.position - transform.position;
        if (!Physics.Raycast(transform.position, dir, 25,  1 << LayerMask.NameToLayer("Walls"))) {
            if (Physics.Raycast(transform.position, dir, 15,  1 << LayerMask.NameToLayer("Player")))
            {
                return true;
            }
            
            return false;
        }

        return false;
    }
}

public enum AgentState
{
    Idle,
    Patrolling
}
