using System;
using TMPro;
using UnityEngine;

public class SwitchStateButton : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI m_ButtonText;

    private void Awake()
    {
        CustomizationManager.OnStateChanged += StateChanged;
    }

    private void StateChanged(SelectionState state)
    {
        m_ButtonText.text = state == SelectionState.Car_Selection ? "Car Selection" : "Gun Selection";
    }

    public void ChangeState()
    {
        CustomizationManager.Instance.ToggleSelection();
    }
}
