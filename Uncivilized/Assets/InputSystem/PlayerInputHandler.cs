using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInputHandler : MonoBehaviour
{
    public Vector2 move;
    public Vector2 look;

    public float horizontal;
    public float vertical;
    public float mouseX;
    public float mouseY;
    public bool interact;
    public bool drop;
    public bool jump;
    public bool sprint;
    public bool attack;

    public float threshold = 0.1f;

    InputActions inputActions;

    private void Awake()
    {
        if (inputActions == null) { 
            inputActions = new InputActions();
        }
    }

    private void OnEnable()
    {
        //movement delegates
        inputActions.Player.Move.started += ctx => Move(ctx.ReadValue<Vector2>());
        inputActions.Player.Move.performed += ctx => Move(ctx.ReadValue<Vector2>());
        inputActions.Player.Move.canceled += ctx => Move(ctx.ReadValue<Vector2>());

        //camera look delegates
        inputActions.Player.Look.started += ctx => Look(ctx.ReadValue<Vector2>());
        inputActions.Player.Look.performed += ctx => Look(ctx.ReadValue<Vector2>());
        inputActions.Player.Look.canceled += ctx => Look(ctx.ReadValue<Vector2>());

        //interaction delegates
        inputActions.Player.Interact.started += ctx => Interact(ctx.ReadValue<float>());
        inputActions.Player.Interact.performed += ctx => Interact(ctx.ReadValue<float>());
        inputActions.Player.Interact.canceled += ctx => Interact(ctx.ReadValue<float>());

        //drop delegates
        inputActions.Player.Drop.started += ctx => Drop(ctx.ReadValue<float>());
        inputActions.Player.Drop.performed += ctx => Drop(ctx.ReadValue<float>());
        inputActions.Player.Drop.canceled += ctx => Drop(ctx.ReadValue<float>());

        //jump delegates
        inputActions.Player.Jump.started += ctx => Jump(ctx.ReadValue<float>());
        inputActions.Player.Jump.performed += ctx => Jump(ctx.ReadValue<float>());
        inputActions.Player.Jump.canceled += ctx => Jump(ctx.ReadValue<float>());

        //sprint delegates
        inputActions.Player.Sprint.started += ctx => Sprint(ctx.ReadValue<float>());
        inputActions.Player.Sprint.performed += ctx => Sprint(ctx.ReadValue<float>());
        inputActions.Player.Sprint.canceled += ctx => Sprint(ctx.ReadValue<float>());

        //attack delegates
        inputActions.Player.Attack.started += ctx => Attack(ctx.ReadValue<float>());
        inputActions.Player.Attack.performed += ctx => Attack(ctx.ReadValue<float>());
        inputActions.Player.Attack.canceled += ctx => Attack(ctx.ReadValue<float>());

        //enable all actions
        inputActions.Enable();
    }

    private void OnDisable()
    {
        inputActions.Disable();
    }

    private void Move(Vector2 ctx)
    {
        move = ctx;
        horizontal = ctx.x;
        vertical = ctx.y;
    }

    private void Look(Vector2 ctx)
    {
        look = ctx;
        mouseX = ctx.x;
        mouseY = ctx.y;
    }

    private void Interact(float ctx)
    {
        if(ctx > threshold)
        {
            interact = true;
        }
        else
        {
            interact = false;
        }
    }

    private void Drop(float ctx)
    {
        if (ctx > threshold)
        {
            drop = true;
        }
        else
        {
            drop = false;
        }
    }

    private void Jump(float ctx)
    {
        if (ctx > threshold)
        {
            jump = true;
        }
        else
        {
            jump = false;
        }
    }

    private void Sprint(float ctx)
    {
        sprint = ctx > threshold ? true : false;
    }

    private void Attack(float ctx)
    {
        attack = ctx > threshold ? true : false;
    }
}
