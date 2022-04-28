using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class TurretShoot : MonoBehaviour
{
    public Transform m_FiringSpawn;

    public Transform m_Transform;

    public float m_LastShotTime;

    public Turret m_TurretAttributes;

    public string m_CompareTag;

    public TrailRenderer m_BulletTrail;

    public ParticleSystem hitParticle;

    public abstract void Shoot(Transform target);

    public virtual bool IsCrit()
    {
        int x = UnityEngine.Random.Range(0, 100);

        return x <= m_TurretAttributes.m_CriticalRate;
    }
}
