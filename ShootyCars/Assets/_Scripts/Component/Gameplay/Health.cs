using System;
using UnityEngine;

[Serializable]
public class Health
{
    public float maxHealth;
    public float currentHealth;

    public void UpdateCurrentHealth(float dmg)
    {
        currentHealth += dmg;

        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
    }
}
