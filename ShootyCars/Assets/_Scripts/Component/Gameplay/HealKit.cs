using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealKit : Powerup
{
    public float healthToAdd;

    public override void SetPowerUp(Controller controller)
    {
        controller.HealthUpdate(healthToAdd);

        Destroy(this.gameObject);
    }
}