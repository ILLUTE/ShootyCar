using System;

[Serializable]
public class Turret
{
    public float m_FiringRate;
    public float m_CriticalRate;
    public float m_Damage;
    public float m_MaxAmmo;

    public float attackRadius;

    public Turret()
    {
        m_FiringRate = 0;
        m_CriticalRate = 0;
        m_Damage = 0;
        m_MaxAmmo = 0;
    }

    public Turret(float fireRate, float critRate, float damage, float maxAmmo)
    {
        m_FiringRate = fireRate;
        m_CriticalRate = critRate;
        m_Damage = damage;
        m_MaxAmmo = maxAmmo;
    }
}
