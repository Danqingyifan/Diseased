using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HealthBar : MonoBehaviour
{
    public Slider slider;
    public TextMeshProUGUI healthText;
    public void SetMaxHealth(float maxHealth)
    {
        slider.maxValue = maxHealth;
        slider.value = maxHealth;
        healthText.text = maxHealth.ToString();
    }

    public void SetHealth(float health)
    {
        slider.value = health;
        healthText.text = health.ToString();
    }

}
