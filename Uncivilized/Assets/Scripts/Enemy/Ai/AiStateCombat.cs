using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AiStateCombat : AiState
{
    public Transform playerTransform;
    private EnemyAttack enemyAttack;
    public float timer = 5.0f;

    public AiStateId GetId()
    {
        return AiStateId.Combat;
    }

    public void Enter(AiAgent agent)
    {
        //get reference to player
        if (playerTransform == null)
        {
            playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        }

        enemyAttack = agent.GetComponent<EnemyAttack>();
    }

    public void Update(AiAgent agent)
    {
        //only attack on a time interval
        timer -= Time.deltaTime;
        if(timer <= 0.0f)
        {
            enemyAttack.Attack();
            timer = 5.0f;
        }

        //rotate towards player
        agent.transform.LookAt(playerTransform);

        //transition to chase player state when out of range
        float distance = Vector3.Distance(agent.transform.position, playerTransform.position);
        if (distance > agent.stopDistance)
        {
            agent.stateMachine.ChangeState(AiStateId.ChasePlayer);
        }

    }

    public void Exit(AiAgent agent)
    {
    }
}
