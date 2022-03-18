using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public float maxHealth = 5.0f;
    public float health;
    public PlayerHealthBarUI healthBarUI;

    private Animator _animator;

    void Start()
    {   
        //get components
        _animator = GetComponent<Animator>();

        //initialize health to max health
        health = maxHealth;

        //update health bar 
        UpdateHealthBar();
    }

    void Update()
    {

    }

    void UpdateHealthBar()
    {
        healthBarUI.SetHealthBarPercentage(health / maxHealth);
    }

    public void TakeDamage()
    {
        health -= 1.0f;
        if (health <= 0.0f)
        {
            //StartCoroutine(Die());
        }
        else
        {
            _animator.SetTrigger("takeDamage");
        }
        UpdateHealthBar();
    }

    IEnumerator Die()
    {
        _animator.SetTrigger("death");
        yield return new WaitForSeconds(3);
        Destroy(gameObject);
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("EnemyWeapon"))
        {
            TakeDamage();
            Debug.Log("Player Health: " + health);
        }
    }
}
