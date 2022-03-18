using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStaminaBarUI : MonoBehaviour
{
    public Image foreground;
    public Image background;

    public void SetStaminaBarPercentage(float percentage)
    {
        foreground.fillAmount = percentage;
    }
}