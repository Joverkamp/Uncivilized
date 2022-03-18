using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu]
public class PlayerStats : ScriptableObject
{
    [SerializeField] private float _healthLvl;
    [SerializeField] private float _staminaLvl;
    [SerializeField] private float _damageLvl;
    [SerializeField] private float _survivalPoints;


    public float GetHealthLvl()
    {
        return _healthLvl;
    }


    public void LevelUpHealth(float levels)
    {
        _healthLvl += levels;
    }


    public float GetStaminaLvl()
    {
        return _staminaLvl;
    }


    public void LevelUpStamina(float levels)
    {
        _staminaLvl += levels;
    }


    public float GetDamageLvl()
    {
        return _damageLvl;
    }
    public void LevelUpDamage(float levels)
    {
        _damageLvl += levels;
    }


    public float GetSurvialPoints()
    {
        return _survivalPoints;
    }


    public void IncreaseSurvivalPoints(float toIncrease)
    {
        _survivalPoints += toIncrease;
    }


    public void DecreaseSurvivalPoints(float toDecrease)
    {
        _survivalPoints -= toDecrease;
        if(_survivalPoints < 0.0f)
        {
            _survivalPoints = 0.0f;
        }
    }
}

