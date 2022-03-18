using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStamina : MonoBehaviour
{
    [Header("Stamina Values")]
    public PlayerStats stats;
    public float maxStamina;
    public float stamina;
    public float recoveryTime;

    public float _recoveryTimer;

    [Header("UI")]
    public PlayerStaminaBarUI staminaBarUI;


    private void Awake()
    {
        //calculate max stamina from player stats
        maxStamina = 100 + (stats.GetStaminaLvl() * 10);
    }

    void Start()
    {
        //initialize stamina to max stamina
        stamina = maxStamina;

        //initialize stamina recovery timer
        _recoveryTimer = recoveryTime;

        //update stamina bar 
        UpdateStaminaBar();
    }

    private void Update()
    {
        GainStamina();
    }

    void UpdateStaminaBar()
    {
        staminaBarUI.SetStaminaBarPercentage(stamina / maxStamina);
    }


    public void GainStamina()
    {
        if(_recoveryTimer <= 0.0f)
        {
            if(stamina < maxStamina)
            {
                stamina += 20 * Time.deltaTime;
                UpdateStaminaBar();
            }
        }
        else
        {
            _recoveryTimer -= Time.deltaTime;
        }
    }

    public void LoseStamina(float val)
    {
        stamina -= val;
        if(stamina < 0.0f)
        {
            stamina = 0.0f;
        }
        _recoveryTimer = recoveryTime;
        UpdateStaminaBar();
    }

}
