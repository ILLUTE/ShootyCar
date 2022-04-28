using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TurretController : Controller
{
    [SerializeField]
    private Sensor m_Sensor;

    [SerializeField]
    private Transform m_Transform;

    private Collider m_EnemyCollider;

    private Vector3 enemyLookDirection;

    private float turnVelocity, turnSmoothTime = 0.05f;

    private TurretShoot m_TS;

    public bool IsTurretSearching = true;

    public bool IsEnemy;

    private bool canShoot;

    [SerializeField]
    private HealthUI m_Health;

    private void Awake()
    {
        if (m_Sensor == null)
        {
            m_Sensor = GetComponent<Sensor>();
        }

        if (m_TS == null)
        {
            m_TS = GetComponent<TurretShoot>();
        }
        if (m_Transform == null)
        {
            m_Transform = transform;
        }

        LoadAccordingly();
    }

    private void LoadAccordingly()
    {
        canShoot = true;

        if (!IsEnemy)
        {
            canShoot = false;

            InputManager.OnButtonPressed += ButtonPressed;
            InputManager.OnButtonReleased += ButtonReleased;
        }
        else
        {
            m_Health.SetHealth(100, 100,this);
        }
    }

    private void ButtonReleased(ButtonType button)
    {
        if (button == ButtonType.Turret)
        {
            canShoot = false;
        }
    }

    private void ButtonPressed(ButtonType button)
    {
        if (button == ButtonType.Turret)
        {
            canShoot = true;
        }
    }

    private void Update()
    {
        if (!IsTurretSearching)
        {
            return;
        }

        m_EnemyCollider = m_Sensor.m_TargetBody;

        if (m_EnemyCollider != null)
        {
            enemyLookDirection = ((m_EnemyCollider.transform.position) - (m_Transform.position));

            float turnAngle = Mathf.Atan2(enemyLookDirection.normalized.x, enemyLookDirection.normalized.z) * Mathf.Rad2Deg;

            float angle = Mathf.SmoothDampAngle(m_Transform.eulerAngles.y, turnAngle, ref turnVelocity, turnSmoothTime);

            m_Transform.rotation = Quaternion.Euler(0, angle, 0);

            if (IsEnemy)
            {
                if (turnVelocity <= 0.1f)
                {
                    canShoot = true;
                }
                else
                {
                    canShoot = false;
                }
            }

            if (canShoot)
            {
                m_TS.Shoot(m_EnemyCollider.transform);
            }
        }
    }

    private void OnDestroy()
    {
        InputManager.OnButtonPressed -= ButtonPressed;
        InputManager.OnButtonReleased -= ButtonReleased;
    }

    public override void HealthUpdate(float damage)
    {
        m_Health.UpdateHealth(damage);
    }

    public override void OnDead()
    {
        LevelManager.Instance.EnemyTurretDestroyed();
        Destroy(this.gameObject);
    }
}
