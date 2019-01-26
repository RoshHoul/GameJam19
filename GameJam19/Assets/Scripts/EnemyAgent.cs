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

    public NavMeshAgent agent;


    private void Start()
    {
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

               // Debug.Log("Target is: " + target.name);
                agent.SetDestination(target.transform.position);
                Debug.Log("Dist is " + Vector3.Distance(agent.destination, agent.transform.position));
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
                target = TakeRandomWaypoint();
                Debug.Log("We idle");
                if (target != null && IsValidWaypoint(target))
                {
                    Debug.Log("NEW TARGET IS " + target.name);
                    
                    prevState = currentState;
                    currentState = AgentState.Patrolling;
                }
            }
        }
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
}

public enum AgentState
{
    Idle,
    Patrolling
}
