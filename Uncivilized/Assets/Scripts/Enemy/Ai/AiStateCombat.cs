using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AiStateCombat : AiState
{
    public Transform playerTransform;
    private EnemyAttack enemyAttack;
    public float timer = 1.0f;

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
            timer = agent.attackTime;
        }

        //rotate towards player
        var playerPosition = playerTransform.position;
        playerPosition.y = agent.transform.position.y;
        agent.transform.LookAt(playerPosition);

        //transition to chase player state when out of range
        float distance = Vector3.Distance(agent.transform.position, playerTransform.position);
        if (distance > agent.stopDistance)
        {
            agent.stateMachine.ChangeState(AiStateId.ChasePlayer);
        }

    }

    public void Exit(AiAgent agent)
    {
        timer = 1.0f;
    }
}
