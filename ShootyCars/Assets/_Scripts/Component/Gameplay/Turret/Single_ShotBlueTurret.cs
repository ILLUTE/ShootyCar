using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Single_ShotBlueTurret : TurretShoot
{
    public override void Shoot(Transform target)
    {
        if (target != null)
        {
            bool IsHit = false;

            Vector3 distance = (target.position - (m_FiringSpawn.position));

            if (distance.magnitude <= m_TurretAttributes.attackRadius)
            {
                if (Time.time > m_LastShotTime + (1 / (m_TurretAttributes.m_FiringRate)))
                {
                    TrailRenderer bullet = Instantiate(m_BulletTrail);

                    bullet.AddPosition(m_FiringSpawn.position);

                    if (Physics.Raycast(m_FiringSpawn.position, distance.normalized * m_TurretAttributes.attackRadius, out RaycastHit hit))
                    {
                        if (hit.collider.CompareTag(m_CompareTag))
                        {
                            hit.collider.GetComponent<Controller>().HealthUpdate(((IsCrit() ? 2 : 1) * -m_TurretAttributes.m_Damage));
                            IsHit = true;
                        }
                        bullet.transform.position = hit.point;
                    }

                    if (IsHit)
                    {
                        m_LastShotTime = Time.time;
                        // gunShot.Play();
                    }
                }
            }
        }
    }
}
