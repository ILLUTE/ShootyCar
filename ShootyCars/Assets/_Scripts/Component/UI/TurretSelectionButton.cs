using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TurretSelectionButton : MonoBehaviour
{
    public TurretBaseData m_TurretData;

    public Image m_IconImage;

    public void UpdateUI(TurretBaseData baseData)
    {
        m_TurretData = baseData;

        m_IconImage.sprite = baseData.turretIcon;
    }

    public void LoadTurret()
    {
        CustomizationManager.Instance.LoadTurret(m_TurretData);
    }
}
