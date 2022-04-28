using UnityEngine;
using UnityEngine.UI;

public class CarSelectionButton : MonoBehaviour
{
    public CarBaseData m_CarData;

    public Image m_IconImage;

    public void UpdateUI(CarBaseData baseData)
    {
        m_CarData = baseData;

        m_IconImage.sprite = baseData.carIcon;
    }

    public void LoadCar()
    {
        CustomizationManager.Instance.LoadCar(m_CarData);
    }
}
