using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public float maxHealth = 5.0f;
    public float health;

    private AiAgent _agent;
    private Animator _animator;

    void Start()
    {
        _agent = GetComponent<AiAgent>();
        _animator = GetComponent<Animator>();

        health = maxHealth; 
    }

    void Update()
    {
        
    }

    public void TakeDamage()
    {
        health -= 1.0f;
        if (health <= 0.0f)
        {
            _agent.stateMachine.ChangeState(AiStateId.Death);
            StartCoroutine(Die());
        }
        else
        {
            _animator.SetTrigger("takeDamage");
        }
    }

    IEnumerator Die()
    {
        _animator.SetTrigger("death");
        yield return new WaitForSeconds(3);
        Destroy(gameObject);
    }



    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("PlayerWeapon")){
            other.enabled = false;
            TakeDamage();
        }
    }
}
