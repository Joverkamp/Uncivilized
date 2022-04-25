using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cursor : MonoBehaviour
{
    #region Singleton
    public static Cursor instance;

    void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("More than one instance of storage inventory found!");
            return;
        }
        instance = this;
    }
    #endregion

    void Start()
    {
        LockCursor();
        //UnityEngine.Cursor.lockState = UnityEngine.CursorLockMode.Locked;
    }


    public void LockCursor()
    {
        UnityEngine.Cursor.lockState = UnityEngine.CursorLockMode.Locked;
    }

    public void UnlockCursor()
    {
        UnityEngine.Cursor.lockState = UnityEngine.CursorLockMode.None;
    }
}
