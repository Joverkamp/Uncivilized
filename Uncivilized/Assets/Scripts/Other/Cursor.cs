using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cursor : MonoBehaviour
{
    void Start()
    {
        UnityEngine.Cursor.lockState = UnityEngine.CursorLockMode.Locked;
    }
}
