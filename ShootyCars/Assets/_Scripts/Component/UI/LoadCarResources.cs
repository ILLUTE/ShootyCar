using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadCarResources : MonoBehaviour
{
    [SerializeField]
    private CarSelectionButton[] buttons;

    private CarBaseData[] carBaseDatas;

    [SerializeField]
    private GameObject m_CarButtonsHolder;

    private void Awake()
    {
        carBaseDatas = Resources.LoadAll<CarBaseData>("ScriptableObjects/Cars");

        CustomizationManager.OnStateChanged += StateChanged;
    }

    private void StateChanged(SelectionState state)
    {
        if (state == SelectionState.Car_Selection)
        {
            m_CarButtonsHolder.SetActive(true);
        }
        else
        {
            m_CarButtonsHolder.SetActive(false);
        }
    }

    private void Start()
    {
        UpdateUIButtons();
    }

    private void UpdateUIButtons()
    {
        int carsLength = carBaseDatas.Length;
        int buttonsLength = buttons.Length;

        for (int i = 0; i < buttonsLength; i++)
        {
            buttons[i].gameObject.SetActive(false);

            if (i < carsLength)
            {
                buttons[i].gameObject.SetActive(true);
                buttons[i].UpdateUI(carBaseDatas[i]);
            }
        }

        buttons[0].LoadCar();
    }
}
