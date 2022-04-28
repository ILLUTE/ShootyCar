using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    private static InputManager instance;

    public static InputManager Instance
    {
        get
        {
            if(instance == null)
            {
                instance = FindObjectOfType<InputManager>();
            }

            return instance;

        }
    }

    public FixedJoystick m_FixedJoystic;
    public static event Action<Vector2> OnMovement;
    public static event Action<ButtonType> OnButtonPressed;
    public static event Action<ButtonType> OnButtonReleased;

    private Vector2 m_Movement;

    private void Update()
    {
        m_Movement = new Vector2(m_FixedJoystic.Horizontal, m_FixedJoystic.Vertical);

        m_Movement = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));


        if (Input.GetKeyDown(KeyCode.Space))
        {
            ButtonPressed(ButtonType.Accelerate);
        }
        else if (Input.GetKeyUp(KeyCode.Space))
        {
            ButtonReleased(ButtonType.Accelerate);
        }

        OnMovement?.Invoke(m_Movement);
    }

    public void ButtonPressed(ButtonType button)
    {
        OnButtonPressed?.Invoke(button);
    }

    public void ButtonReleased(ButtonType button)
    {
        OnButtonReleased?.Invoke(button);
    }
}
