using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AiStateDeath : AiState
{
    public AiStateId GetId()
    {
        return AiStateId.Death;
    }

    public void Enter(AiAgent agent)
    {
        if (agent.navMeshAgent.hasPath)
        {
            agent.navMeshAgent.ResetPath();
        }
    }

    public void Update(AiAgent agent)
    {
    }

    public void Exit(AiAgent agent)
    {
    }

}
