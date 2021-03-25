using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Slider slider;

    void Awake() {
        slider = GetComponent<Slider>();
    }

    public void SetMaxHealth(int health)  // set max health val
    {
        slider.maxValue = health;
        slider.value = health;
    }

    public void SetHealth(int health)  // set health to change slider fill
    {
        slider.value = health;
    }
}
