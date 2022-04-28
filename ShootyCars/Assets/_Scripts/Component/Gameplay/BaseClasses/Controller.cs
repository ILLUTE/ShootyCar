using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Controller : MonoBehaviour
{
    public abstract void HealthUpdate(float damage);

    public abstract void OnDead();
}
