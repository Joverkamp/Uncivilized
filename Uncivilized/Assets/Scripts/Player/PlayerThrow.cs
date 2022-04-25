using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerThrow : MonoBehaviour
{
    //throw information
    public Transform throwPivot;
    public GameObject projectilePrefab;
    public float staminaUsage = 15.0f;
    public Transform environment;

    //components
    private GameObject _camera;
    private PlayerMovement _playerController;
    private Animator _animator;
    private PlayerInputHandler _input;
    private PlayerStamina _playerStamina;

    private GameObject _projectile;
    private bool _isHeld = false;

    void Start()
    {
        //get components
        _camera = Camera.main.gameObject;
        _playerController = GetComponent<PlayerMovement>();
        _animator = GetComponent<Animator>();
        _input = GetComponent<PlayerInputHandler>();
        _playerStamina = GetComponent<PlayerStamina>();
    }


    void Update()
    {
        Throw();
    }

    private void Throw()
    {
        _animator.SetBool("throw", false);
        if (_input.throwAttack && _playerStamina.stamina > 1.0f)
        {
            //update animator
            _animator.SetBool("throw", true);
        }
    }

    public void ThrowStart()
    {
        //spawn projectile
        _projectile = Instantiate(projectilePrefab, throwPivot);
        _isHeld = true;

        //update animator
        _animator.SetBool("throw", false);
    }

    public void ThrowEnd()
    {

        //turn player to face camera angle
        float targetAngle = _camera.transform.eulerAngles.y;
        transform.localEulerAngles = new Vector3(0, targetAngle, 0);

        //throw
        if (_projectile != null)
        {
            //decrease stamina
            _playerStamina.LoseStamina(staminaUsage);

            //deparent projectile, calculate and add force
            _projectile.transform.SetParent(environment);
            Rigidbody rb = _projectile.GetComponent<Rigidbody>();
            Vector3 throwDirection = (_camera.transform.forward * 700.0f) + (Vector3.up * 250.0f) + (transform.right * 30.0f);
            rb.isKinematic = false;
            rb.AddForce(throwDirection);
            _isHeld = false;
        }
    }

    IEnumerator ThrowAttackFreeze()
    {
        _playerController.FreezeMovement();
        yield return new WaitForSeconds(1.8f);
        _playerController.UnfreezeMovement();
    }

    public bool ProjectileHeld()
    {
        return _isHeld;
    }

    public void DestroyProjectile()
    {
        Destroy(_projectile);
    }
}
