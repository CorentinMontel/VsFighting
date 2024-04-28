using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarManager : MonoBehaviour
{
    public Slider Slider;

    // Update is called once per frame
    void Update()
    {
        //
    }

    public void UpdateHealth(float currentHealth, float maxHealth)
    {
        Slider.value = (currentHealth / maxHealth) * 100;
    }
}
