using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    //weapon information
    public Transform weaponPivotR;
    public float staminaUsage = 30.0f;

    //components
    private GameObject _camera;
    private PlayerMovement _playerController;
    private Animator _animator;
    private PlayerInputHandler _input;
    private PlayerStamina _playerStamina;

    private GameObject _weapon;
    private BoxCollider _weaponCollider;
    

    void Start()
    {
        //get components
        _camera = Camera.main.gameObject;
        _playerController = GetComponent<PlayerMovement>();
        _animator = GetComponent<Animator>();
        _input = GetComponent<PlayerInputHandler>();
        _playerStamina = GetComponent<PlayerStamina>();

        //set up wapon collider
        _weapon = weaponPivotR.GetChild(0).gameObject;
        _weaponCollider = _weapon.GetComponent<BoxCollider>();
        _weaponCollider.enabled = false;
    }


    void Update()
    {
        Attack();
        Debug.Log("Throw: " + _input.throwAttack);
        Debug.Log("Attack: " + _input.attack);
    }

    private void Attack()
    {
        _animator.SetBool("attack", false);
        if (_input.attack && _playerStamina.stamina > 1.0f)
        {
            //update animator
            _animator.SetBool("attack", true);

            //turn player to face camera angle
            float targetAngle = _camera.transform.eulerAngles.y;
            transform.localEulerAngles = new Vector3(0, targetAngle, 0);
        }
    }

    public void AttackStart()
    {
        //active collider
        _weaponCollider.enabled = true;

        //update animator
        _animator.SetBool("attack", false);

        //decrease stamina
        _playerStamina.LoseStamina(staminaUsage);

        //start coroutine to disable hitbox
        StartCoroutine(AttackEnd());
    }

    IEnumerator AttackEnd()
    {
        yield return new WaitForSeconds(0.05f);
        //deactivate collider
        _weaponCollider.enabled = false;
    }

    IEnumerator AttackFreeze()
    {
        _playerController.FreezeMovement();
        yield return new WaitForSeconds(1.2f);
        _playerController.UnfreezeMovement();
    }
}
