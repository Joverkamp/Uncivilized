using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinLossListener : MonoBehaviour
{
    public string nextLevel;

    private NavigateMenu _navigateMenu;

    private void Awake()
    {
        //subscribe to delegate
        PlayerHealth.playerDeathCallback += Lose;
        Storage.requirementsMetCallback += Win;
    }
    void Start()
    {
        _navigateMenu = GetComponent<NavigateMenu>();
    }

    void Win()
    {
        Cursor.instance.UnlockCursor();
        _navigateMenu.SwitchScene(nextLevel);
    }

    void Lose()
    {
        Cursor.instance.UnlockCursor();
        _navigateMenu.SwitchScene("GameOver");
    }
}
