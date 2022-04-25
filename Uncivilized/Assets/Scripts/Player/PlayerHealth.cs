using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [Header("Health Values")]
    public PlayerStats stats;
    public float maxHealth;
    public float health;

    [Header("UI")]
    public PlayerHealthBarUI healthBarUI;

    private Animator _animator;
    private PlayerThrow _playerThrow;
    private PlayerMovement _playerMovement;

    //event for player death
    public delegate void OnPlayerDeath();
    public static OnPlayerDeath playerDeathCallback;

    private void Awake()
    {
        //calculate max health from player stats
        maxHealth = 100 + (stats.GetHealthLvl() * 10);
    }

    void Start()
    {   
        //get components
        _animator = GetComponent<Animator>();
        _playerThrow = GetComponent<PlayerThrow>();
        _playerMovement = GetComponent<PlayerMovement>();

        //initialize health to max health
        health = maxHealth;

        //update health bar 
        UpdateHealthBar();
    }


    void UpdateHealthBar()
    {
        healthBarUI.SetHealthBarPercentage(health / maxHealth);
    }


    public void LoseHealth()
    {
        health -= 25.0f;
        if (health <= 0.0f)
        {
            StartCoroutine(Die());
            //_animator.SetTrigger("takeDamage");
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
        _playerMovement.FreezeMovement();

        yield return new WaitForSeconds(3);
        Destroy(gameObject);

        if (playerDeathCallback != null)
        {
            playerDeathCallback.Invoke();
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("EnemyWeapon"))
        {
            LoseHealth();

            //destroy projectile if player is mid-throw
            if (_playerThrow.ProjectileHeld())
            {
                _playerThrow.DestroyProjectile();
            }
        }
    }
}
