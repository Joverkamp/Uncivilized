using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthBarUI : MonoBehaviour
{
    public Image foreground;
    public Image background;

    public void SetHealthBarPercentage(float percentage)
    {
        foreground.fillAmount = percentage;
    }
}
