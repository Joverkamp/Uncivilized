using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public Transform weaponPivotR;

    private GameObject _camera;
    private PlayerController _playerController;
    private Animator _animator;
    private PlayerInputHandler _input;
    private GameObject _weapon;
    private BoxCollider _weaponCollider;

    void Start()
    {
        _camera = Camera.main.gameObject;
        _playerController = GetComponent<PlayerController>();
        _animator = GetComponent<Animator>();
        _input = GetComponent<PlayerInputHandler>();

        _weapon = weaponPivotR.GetChild(0).gameObject;
        _weaponCollider = _weapon.GetComponent<BoxCollider>();
        _weaponCollider.enabled = false;
    }


    void Update()
    {
        Attack();
    }

    private void Attack()
    {
        if (_input.attack)
        {
            //update animator
            _animator.SetBool("attack", true);

            //freeze movement while attacking
            StartCoroutine(FreezeMovement());

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
    }

    public void AttackEnd()
    {
        //deactivate collider
        _weaponCollider.enabled = false;
    }

    IEnumerator FreezeMovement()
    {
        _playerController.FreezeMovement();
        yield return new WaitForSeconds(1.2f);
        _playerController.UnfreezeMovement();
    }
}
