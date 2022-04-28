using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="ScriptableObjects/Turrets")]
public class TurretBaseData : ScriptableObject
{
    public int turretId;

    public float FireRate;
    public float maxAmmo;
    public float Damage;
    public float critRate;

    public TurretController turret;
    public Sprite turretIcon;
}
