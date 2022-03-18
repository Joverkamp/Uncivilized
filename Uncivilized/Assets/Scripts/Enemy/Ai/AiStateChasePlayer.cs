using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AiStateChasePlayer : AiState
{
    public Transform playerTransform;
    public float timer = 0.0f;
    public AiStateId GetId()
    {
        return AiStateId.ChasePlayer;
    }

    public void Enter(AiAgent agent)
    {
        //get reference to player
        if(playerTransform == null)
        {
            playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        }

        //set stopping distance
        agent.navMeshAgent.stoppingDistance = agent.stopDistance;
    }

    public void Update(AiAgent agent)
    {
        //only update path on a time interval
        timer -= Time.deltaTime;
        if (timer < 0.0f)
        {
            //set destination to player
            agent.navMeshAgent.destination = playerTransform.position;

            //reset timer timer
            timer = agent.chaseTime;
        }

        //transition to patrol state when out of range
        float distance = Vector3.Distance(agent.transform.position, playerTransform.position);
        if (distance > agent.chaseDistance)
        {
            agent.stateMachine.ChangeState(AiStateId.Patrol);
        }
        //transition to combat state when in range
        else if (distance <= agent.stopDistance)
        {
            agent.stateMachine.ChangeState(AiStateId.Combat);
        }
    }

    public void Exit(AiAgent agent)
    {
        //agent.navMeshAgent.ResetPath();
    }
}
