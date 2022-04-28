using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CarAttributesView : MonoBehaviour
{

    [SerializeField]
    private GameObject m_CarAttributePanel;

    [SerializeField]
    private Image m_MaxHealth, m_Control, m_MaxSpeed;

    private void Awake()
    {
        CustomizationManager.OnStateChanged += StateChanged;
        CustomizationManager.OnCarChanged += CarChanged;
    }

    private void CarChanged(CarBaseData data)
    {
        UpdateTexts(data.maxHealth, data.maxSpeed, data.carControls);
    }

    private void StateChanged(SelectionState state)
    {
        if (state == SelectionState.Car_Selection)
        {
            m_CarAttributePanel.SetActive(true);

            CarBaseData temp = CustomizationManager.Instance.GetCurrentCar();

            if (temp != null)
            {

                UpdateTexts(temp.maxHealth, temp.maxSpeed, temp.carControls);
            }
        }
        else
        {
            m_CarAttributePanel.SetActive(false);
        }
    }

    private void UpdateTexts(float health, float speed, float carControls)
    {
        m_MaxHealth.fillAmount = health / 800;
        m_Control.fillAmount = speed / 300;
        m_MaxSpeed.fillAmount = carControls / 100; ;
    }
}
