using UnityEngine;


public class PlayerMovement : MonoBehaviour
{
    //movement properties
    [Header("Player Movement")]
    public float baseSpeed = 5.0f;          //how fast player moves
    public float jumpForce = 10.0f;         //how high player jumps
    public float gravity = 9.81f;           //rate of gravity
    public float jumpTimeout = 0.50f;       //jump cooldown
    public float fallTimeout = 0.25f;       //time ungrounded before falling

    [Header("Player Grounded")]
    public bool grounded = true;            //grouded state
    public float groundedOffset = -0.14f;   //offset for collision sphere
    public float groundedRadius = 0.28f;    //radius of grounded check
    public LayerMask groundLayers;          //layers considered ground

    private bool _freezeMovement;
    private float speed;                    //player speed calculated from baseSpeed
    private Vector3 deltaY;                 //vertical movement axis
    private bool isRunning;                 //state of sprint
    private float _turnSmoothVelocity;      //rate player rotates
    private float _turnSmoothTime = 0.1f;   //time taken for player to rotate
    private float _jumpTimeoutDelta;        //time 
    private float _fallTimeoutDelta;

    //virtual camera properties

    [Header("Player Camera")]
    public float TopClamp = 70.0f;          //how far in degrees can you move the camera up
    public float BottomClamp = -30.0f;      //how far in degrees can you move the camera down
    public bool LockCameraPosition = false; //for locking the camera position on all axis

    private Transform _virtualCameraTarget;
    private float _cinemachineTargetYaw;
    private float _cinemachineTargetPitch;
    private const float _threshold = 0.01f;

    //components

    private GameObject _camera;
    private CharacterController _characterController;
    private PlayerInputHandler _input;
    private Animator _animator;
    private PlayerStamina _playerStamina;

    //current state of animator
    AnimatorStateInfo animationState;


    private void Start()
    {
        _camera = Camera.main.gameObject;
        _virtualCameraTarget = GameObject.Find("CameraFollowRoot").transform;
        _characterController = GetComponent<CharacterController>();
        _input = GetComponent<PlayerInputHandler>();
        _animator = GetComponentInChildren<Animator>();
        _playerStamina = GetComponent<PlayerStamina>();
    }


    // Update is called once per frame
    private void Update()
    {

        animationState = _animator.GetCurrentAnimatorStateInfo(0);

        JumpAndGravity();
        GroundedCheck();
        if (!_freezeMovement) Movement();
    }


    private void LateUpdate()
    {
        CameraRotation();
    }


    private void JumpAndGravity()
    {
        if (grounded)
        {
            //keep deltaY constant
            deltaY.y = -1.0f;

            //reset falling timeout
            _fallTimeoutDelta = fallTimeout;

            //update animator
            _animator.SetBool("falling", false);
            _animator.SetBool("jump", false);

            //check jump 
            if (_input.jump && _jumpTimeoutDelta <= 0.0f)
            {
                //set jumping velocity and update animator
                deltaY.y = jumpForce;
                _animator.SetBool("jump", true);
            }

            // jump timeout
            if (_jumpTimeoutDelta >= 0.0f)
            {
                _jumpTimeoutDelta -= Time.deltaTime;
            }
        }
        else
        {
            //apply gravity to y velocity
            deltaY.y -= gravity * 2 * Time.deltaTime;

            // reset the jump timeout timer
            _jumpTimeoutDelta = jumpTimeout;

            // fall timeout
            if (_fallTimeoutDelta >= 0.0f)
            {
                _fallTimeoutDelta -= Time.deltaTime;
            }
            else
            {
                // update animator
                _animator.SetBool("falling", true);
                _animator.SetBool("jump", false);
            }

            // if we are not grounded, do not jump
            _input.jump = false;
        }

        //move y
        _characterController.Move(deltaY * Time.deltaTime);
    }


    private void GroundedCheck()
    {
        // set sphere position, with offset
        Vector3 spherePosition = new Vector3(transform.position.x, transform.position.y - groundedOffset, transform.position.z);
        grounded = Physics.CheckSphere(spherePosition, groundedRadius, groundLayers, QueryTriggerInteraction.Ignore);

        // update animator if using character

         _animator.SetBool("isGrounded", grounded);
    }


    private void Movement()
    {
        //get movement vector
        Vector3 deltaXZ = new Vector3(_input.horizontal, 0f, _input.vertical);

        //check for sprint if moving forward
        if (_input.sprint && deltaXZ == Vector3.forward && _playerStamina.stamina > 0.0f)
        {
            //flag running and increase speed
            isRunning = true;
            speed = baseSpeed + 3.0f;

            //decrease stamina
            _playerStamina.LoseStamina(20.0f * Time.deltaTime);
        }
        else
        {
            isRunning = false;
            speed = baseSpeed;
        }

        //move x
        if (deltaXZ.magnitude > 0)
        {
            //get camera rotation, smooth transition, rotate
            float targetAngle = _camera.transform.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref _turnSmoothVelocity, _turnSmoothTime);
            transform.localEulerAngles = new Vector3(0, angle, 0);

            //clamp speed for diags, rotation relative movement, move
            deltaXZ = Vector3.ClampMagnitude(deltaXZ, 1.0f);
            deltaXZ = transform.TransformDirection(deltaXZ);
            _characterController.Move(deltaXZ * speed * Time.deltaTime);
        }

        //animation
        float velocityX = Vector3.Dot(deltaXZ, transform.forward);
        float velocityZ = Vector3.Dot(deltaXZ, transform.right);

        //for blend tree
        if (isRunning == false)
        {
            velocityX /= 2;
            velocityZ /= 2;
        }

        //set animator properties
        _animator.SetFloat("velocityForward", velocityX, 0.1f, Time.deltaTime);
        _animator.SetFloat("velocitySide", velocityZ, 0.1f, Time.deltaTime);
    }


    public void FreezeMovement()
    {
        _freezeMovement = true;
    }


    public void UnfreezeMovement()
    {
        _freezeMovement = false;
    }


    private void CameraRotation()
    {
        // if there is an input and camera position is not fixed
        if (_input.look.sqrMagnitude >= _threshold && !LockCameraPosition)
        {
            _cinemachineTargetYaw += _input.mouseX * Time.deltaTime;
            _cinemachineTargetPitch += _input.mouseY * Time.deltaTime;
        }

        // clamp our rotations so our values are limited 360 degrees
        _cinemachineTargetYaw = ClampAngle(_cinemachineTargetYaw, float.MinValue, float.MaxValue);
        _cinemachineTargetPitch = ClampAngle(_cinemachineTargetPitch, BottomClamp, TopClamp);

        // Cinemachine will follow this target
        _virtualCameraTarget.rotation = Quaternion.Euler(_cinemachineTargetPitch, _cinemachineTargetYaw, 0.0f);
    }


    private static float ClampAngle(float lfAngle, float lfMin, float lfMax)
    {
        if (lfAngle < -360f)
        {
            lfAngle += 360f;
        }

        if (lfAngle > 360f)
        {
            lfAngle -= 360f;
        }

        return Mathf.Clamp(lfAngle, lfMin, lfMax);
    }
}
