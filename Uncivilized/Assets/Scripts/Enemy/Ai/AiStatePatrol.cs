using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class AiStatePatrol : AiState
{
    public Transform playerTransform;
    public float timer = 0.0f;

    public AiStateId GetId()
    {
        return AiStateId.Patrol;
    }

    public void Enter(AiAgent agent)
    {
        //get reference to player
        if (playerTransform == null)
        {
            playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        }

        //set stopping distance
        agent.navMeshAgent.stoppingDistance = 0.0f;

        //reset path if unfinished
        if (agent.navMeshAgent.pathPending)
        {
            agent.navMeshAgent.ResetPath();
        }
    }

    public void Update(AiAgent agent)
    {
        //check if agent does not have a path
        if (!agent.navMeshAgent.hasPath)
        {
            //olny update path on a timer interval
            timer -= Time.deltaTime;
            if (timer <= 0.0f)
            {

                //get random position in patrol area
                float randX = Random.Range(0.0f, agent.patrolDistance);
                float randZ = Random.Range(0.0f, agent.patrolDistance);
                Vector3 patrolOffset = new Vector3(randX, 0.0f, randZ);

                //set destination to patrol point
                agent.navMeshAgent.SetDestination(agent.patrolPosition + patrolOffset);

                //reset timer
                timer = agent.patrolTime;
            }
        }

        //transition to chase state when in range
        float distance = Vector3.Distance(agent.transform.position, playerTransform.position);
        if (distance < 10.0f)
        {
            agent.stateMachine.ChangeState(AiStateId.ChasePlayer);
        }
    }

    public void Exit(AiAgent agent)
    {

    }

}
