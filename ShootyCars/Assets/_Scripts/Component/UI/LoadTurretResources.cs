using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadTurretResources : MonoBehaviour
{
    private TurretBaseData[] turretData;

    [SerializeField]
    private TurretSelectionButton[] buttons;

    [SerializeField]
    private GameObject m_TurretButtonsHolder;

    private void Awake()
    {
        turretData = Resources.LoadAll<TurretBaseData>("ScriptableObjects/Turrets");

        CustomizationManager.OnStateChanged += StateChanged;
    }

    private void StateChanged(SelectionState state)
    {
        if (state == SelectionState.Turret_Selection)
        {
            m_TurretButtonsHolder.SetActive(true);
        }
        else
        {
            m_TurretButtonsHolder.SetActive(false);
        }
    }

    private void Start()
    {
        UpdateUIButtons();
    }

    private void UpdateUIButtons()
    {
        int turretLength = turretData.Length;
        int buttonsLength = buttons.Length;

        for (int i = 0; i < buttonsLength; i++)
        {
            buttons[i].gameObject.SetActive(false);

            if (i < turretLength)
            {
                buttons[i].gameObject.SetActive(true);
                buttons[i].UpdateUI(turretData[i]);
            }
        }

        buttons[0].LoadTurret();
    }
}