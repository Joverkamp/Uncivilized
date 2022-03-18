using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AiAgent : MonoBehaviour
{
    public AiStateMachine stateMachine;
    public AiStateId initialState;
    public NavMeshAgent navMeshAgent;

    [Header("Chasing Player")]
    public float chaseTime = 1.0f; //time between pathfinding calculations
    public float chaseDistance = 10.0f; //distance enemy will pursue player
    public float stopDistance = 2.0f; //stopping distance to player

    [Header("Patroling area")]
    public float patrolTime = 4.0f; //time waiting before changing patrol path
    public float patrolDistance = 10.0f; //area to patrol
    public Vector3 patrolPosition; //where to calculate patrol path from


    void Start()
    {
        //get components
        navMeshAgent = GetComponent<NavMeshAgent>();
        patrolPosition = GetComponent<Transform>().position;

        //create state machine
        stateMachine = new AiStateMachine(this);

        //register states
        stateMachine.RegisterState(new AiStateChasePlayer());
        stateMachine.RegisterState(new AiStatePatrol());
        stateMachine.RegisterState(new AiStateDeath());
        stateMachine.RegisterState(new AiStateCombat());

        //set initialState
        stateMachine.ChangeState(initialState);

    }

    void Update()
    {
        stateMachine.Update();
    }
}
