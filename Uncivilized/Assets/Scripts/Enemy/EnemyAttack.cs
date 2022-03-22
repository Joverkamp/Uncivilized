using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    public Transform weaponPivotR;

    private GameObject _weapon;
    private BoxCollider _weaponCollider;

    private Transform _playerTransform;
    private Animator _animator;

    void Start()
    {
        //weapon components
        _weapon = weaponPivotR.GetChild(0).gameObject;
        _weaponCollider = _weapon.GetComponent<BoxCollider>();
        _weaponCollider.enabled = false;


        //other components
        _playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        _animator = GetComponent<Animator>();
    }


    void Update()
    {
    }

    public void Attack()
    {
        //update animator
        _animator.SetBool("attack", true);

        //rotate towards player
        var playerPosition = _playerTransform.position;
        playerPosition.y = transform.position.y;
        transform.LookAt(playerPosition);
    }

    public void AttackStart()
    {
        //active collider
        _weaponCollider.enabled = true;

        //update animator
        _animator.SetBool("attack", false);

        //start coroutine to disable hitbox
        StartCoroutine(AttackEnd());
    }

    IEnumerator AttackEnd()
    {
        yield return new WaitForSeconds(0.05f);
        //deactivate collider
        _weaponCollider.enabled = false;
    }
}
