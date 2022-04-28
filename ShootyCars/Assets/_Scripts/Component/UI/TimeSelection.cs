using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TimeSelection : MonoBehaviour
{
    [SerializeField]
    private TMP_Dropdown m_DropDown;

    List<string> m_AllowedTime = new List<string>() { "30", "60" };

    private void Awake()
    {
        m_DropDown.AddOptions(m_AllowedTime);
    }

    public void OnValueChanged(int option)
    {
        CustomizationManager.Instance.SetGameTime(float.Parse(m_AllowedTime[option]));
    }
}
