using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    public Transform weaponPivotR;
    public Transform playerTransform;

    private Animator _animator;
    private GameObject _weapon;
    private BoxCollider _weaponCollider;

    void Start()
    {
        _animator = GetComponent<Animator>();

        _weapon = weaponPivotR.GetChild(0).gameObject;
        _weaponCollider = _weapon.GetComponent<BoxCollider>();
        _weaponCollider.enabled = false;
    }


    void Update()
    {
    }

    public void Attack()
    {
        //update animator
        _animator.SetBool("attack", true);

        //rotate towards player
        var playerPosition = playerTransform.position;
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
        Debug.Log("Player Collider Disabled");
    }
}
