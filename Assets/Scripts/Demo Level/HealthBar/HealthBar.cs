using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Slider slider;
    public Gradient gradient;
    public Image fill;

    private int maxHealth;

    public void SetMaxHealth(int health)
    {
        maxHealth = health;
        slider.maxValue = health;
        slider.value = health;

        fill.color = gradient.Evaluate(1f);
    }

    public void SetHealth(int health)
    {
        slider.value = health;

        fill.color = gradient.Evaluate(slider.normalizedValue);
    }

    public void IncreaseHealth(int amount)
    {

        slider.value += amount;
        // Ensure the health does not exceed the maximum value
        if (slider.value < maxHealth)
        {
            slider.value = maxHealth;
        }
        fill.color = gradient.Evaluate(slider.normalizedValue);
    }
}
