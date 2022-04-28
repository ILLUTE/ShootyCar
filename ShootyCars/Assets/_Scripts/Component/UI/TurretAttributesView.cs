using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TurretAttributesView : MonoBehaviour
{
    [SerializeField]
    private GameObject m_TurretPanel;

    [SerializeField]
    private Image m_Text_MaxAmmo, m_Text_FireRate, m_Text_Damage, m_Text_CritRate;

    private void Awake()
    {
        CustomizationManager.OnStateChanged += StateChanged;
        CustomizationManager.OnTurretChanged += TurretChanged;
    }

    private void TurretChanged(TurretBaseData data)
    {
        UpdateTexts(data.maxAmmo, data.FireRate, data.Damage, data.critRate);
    }

    private void StateChanged(SelectionState state)
    {
        if (state == SelectionState.Turret_Selection)
        {
            m_TurretPanel.SetActive(true);

            TurretBaseData temp = CustomizationManager.Instance.GetCurrentTurret();

            if (temp != null)
            {

                UpdateTexts(temp.maxAmmo, temp.FireRate, temp.Damage, temp.critRate);
            }
        }
        else
        {
            m_TurretPanel.SetActive(false);
        }
    }

    private void UpdateTexts(float ammo, float fireRate, float damage, float critRate)
    {
        m_Text_MaxAmmo.fillAmount = ammo / ammo;
        m_Text_FireRate.fillAmount = fireRate / 60;
        m_Text_CritRate.fillAmount = critRate / 100; ;
        m_Text_Damage.fillAmount = damage / 30;
    }
}
