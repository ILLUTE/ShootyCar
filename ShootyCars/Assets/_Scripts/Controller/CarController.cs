using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CarController : Controller
{
    [SerializeField]
    private WheelCollider wheel_fr, wheel_fl, wheel_rr, wheel_rl;

    [SerializeField]
    private Transform t_wheel_fr, t_wheel_fl, t_wheel_rr, t_wheel_rl;

    [SerializeField]
    [Range(0, 3600)]
    private float horsePower, brakingPower, currentBrakeForce;
    [SerializeField]
    private float steeringDirection, steeringAngle, maxSteeringAngle,movingDirection;

    private bool IsBraking, IsAccelerating;

    public CarAttachmentPoints carAttachmentPoint;

    [Header("Particle System")]
    [SerializeField]
    private ParticleSystem m_Exhaust;

    [SerializeField]
    private Rigidbody m_Rigidbody;

    [SerializeField]
    private HealthUI m_HealthFill;

    private float maxSpeed;
    private float controlFactor;
    private float maxHealth;

    private void Awake()
    {
        InputManager.OnMovement += OnMovement;
        InputManager.OnButtonReleased += ButtonReleased;
        InputManager.OnButtonPressed += ButtonPressed;

        if(m_Rigidbody == null)
        {
            m_Rigidbody = GetComponent<Rigidbody>();
        }   
    }

    public void IsKinematic(bool allow)
    {
        m_Rigidbody.isKinematic = allow;

        if(!allow)
        {
            m_HealthFill = GameObject.FindGameObjectWithTag("CarUI").GetComponent<HealthUI>();
        }
    }

    private void ButtonPressed(ButtonType button)
    {
        switch (button)
        {
            case ButtonType.Accelerate:
                IsAccelerating = true;
                break;
            case ButtonType.Brakes:
                IsBraking = true;
                break;
            case ButtonType.Turret:// Shoot Turrets
                break;
            case ButtonType.Missile:
                break;
        }
    }

    private void ButtonReleased(ButtonType button)
    {
        switch (button)
        {
            case ButtonType.Accelerate:
                IsAccelerating = false;
                break;
            case ButtonType.Brakes:
                IsBraking = false;
                break;
            case ButtonType.Turret:
                break;
            case ButtonType.Missile:
                break;
        }
    }

    private void OnMovement(Vector2 movement)
    {
        steeringDirection = movement.x;
        movingDirection = movement.y;
    }

    private void FixedUpdate()
    {
        if (m_Rigidbody.velocity.magnitude >= maxSpeed)
        {
            m_Rigidbody.velocity = m_Rigidbody.velocity.normalized * maxSpeed;
        }
        WheelsControl();
        HandelSteering();
        UpdateWheels();
    }

    private void WheelsControl()
    {
        wheel_fl.motorTorque = movingDirection * ((IsAccelerating ? 1 : 0) * horsePower);
        wheel_fr.motorTorque = movingDirection * ((IsAccelerating ? 1 : 0) * horsePower);
        currentBrakeForce = IsBraking ? brakingPower : 0f;
        if(IsAccelerating)
        {
            m_Exhaust.Emit(1);
        }

        ApplyingBrakeForce();
    }

    private void HandelSteering()
    {
        steeringAngle = maxSteeringAngle * (steeringDirection * controlFactor);
        wheel_fl.steerAngle = steeringAngle;
        wheel_fr.steerAngle = steeringAngle;
    }

    private void ApplyingBrakeForce()
    {
        wheel_fr.brakeTorque = currentBrakeForce;
        wheel_fl.brakeTorque = currentBrakeForce;
        wheel_rr.brakeTorque = currentBrakeForce;
        wheel_rl.brakeTorque = currentBrakeForce;
    }

    private void UpdateWheels()
    {
        UpdateSteeringWheel(wheel_fl, t_wheel_fl);
        UpdateSteeringWheel(wheel_fr, t_wheel_fr);
        UpdateSteeringWheel(wheel_rl, t_wheel_rl);
        UpdateSteeringWheel(wheel_rr, t_wheel_rr);
    }

    private void UpdateSteeringWheel(WheelCollider collider, Transform wheel)
    {
        Vector3 pos;
        Quaternion rot;

        collider.GetWorldPose(out pos, out rot);
        wheel.rotation = rot;
        wheel.position = pos;
    }

    public override void HealthUpdate(float damage)
    {
        m_HealthFill.UpdateHealth(damage);
    }

    public override void OnDead()
    {
        Destroy(this.gameObject);
    }

    public void SetAttributes(CarBaseData data)
    {
        maxSpeed = data.maxSpeed;
        maxHealth = data.maxHealth;
        controlFactor = data.carControls / 100;

        m_HealthFill.SetHealth(maxHealth, maxHealth,this);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Powerup"))
        {
            Powerup power = other.gameObject.GetComponent<Powerup>();
            power.SetPowerUp(this);
        }
    }
}
