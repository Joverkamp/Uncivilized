using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Interactable : MonoBehaviour
{
    public float range = 3.0f;
    public abstract void OnHoverEnter();
    public abstract void OnHoverExit();
    public abstract void OnInteract();

}
