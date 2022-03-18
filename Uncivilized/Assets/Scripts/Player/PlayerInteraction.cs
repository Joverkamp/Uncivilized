using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    //components
    private Interactable _currTarget;
    private Transform _lookPoint;
    private Camera _camera;
    private PlayerInputHandler _input;


    // Start is called before the first frame update
    void Start()
    {
        //get reference to look point to cast ray from and main camera
        _lookPoint = GameObject.Find("Look Point").transform;
        _camera = Camera.main;
        _input = GetComponent<PlayerInputHandler>();

    }


    // Update is called once per frame
    void Update()
    {
        //interaction
        CastRayForInteractable();
        //check for input
        if (_input.interact)
        {
            //interact with target
            if(_currTarget != null){
                _currTarget.OnInteract();
            }
        }
        //dropping items
        if(_input.drop)
        {
            PlayerInventory.instance.Drop(transform);
        }
    }


    private void CastRayForInteractable()
    {
        //cast ray in direction of cameras rotation from look point
        _lookPoint.rotation = _camera.transform.rotation;
        RaycastHit hit;
        if(Physics.Raycast(_lookPoint.position, _lookPoint.forward * 100.0f, out hit, Mathf.Infinity))
        {
            Interactable interactable = hit.collider.GetComponent<Interactable>();
            //when looking at a target that is interactable
            if(interactable != null)
            {
                //interactable is in range
                if (hit.distance <= interactable.range)
                {
                    //target has not changed
                    if (_currTarget == interactable)
                    {
                        return;
                    }
                    //if target has changed from an old target
                    else if (_currTarget != null)
                    {
                        _currTarget.OnHoverExit();
                        _currTarget = interactable;
                        _currTarget.OnHoverEnter();
                    }
                    //new target from previously null
                    else
                    {
                        _currTarget = interactable;
                        _currTarget.OnHoverEnter();
                    }
                }
                //target is out of range
                else
                {
                    //tear down previous target
                    if(_currTarget != null)
                    {
                        _currTarget.OnHoverExit();
                        _currTarget = null;
                    }
                }
            }
            //target is not interactable
            else
            {
                //tear down previous target
                if (_currTarget != null)
                {
                    _currTarget.OnHoverExit();
                    _currTarget = null;
                }
            }
        }
        //not looking at anything
        else
        {
            //tear down previous target
            if (_currTarget != null)
            {
                _currTarget.OnHoverExit();
                _currTarget = null;
            }
        }

    }
}
