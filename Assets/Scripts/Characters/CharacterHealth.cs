using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterHealth : MonoBehaviour
{
    public float maxHealth = 100;
    public float startHealth = 100;
    public float currenthealth = 0;

    private void Start()
    {
        currenthealth = startHealth;
    }

    public void ReduceHealth(float amount)
    {
        currenthealth -= Mathf.Min(amount, currenthealth);
    }

    public void AddHealth(float amount)
    {
        currenthealth += Mathf.Min(amount, maxHealth - currenthealth);
    }
}
