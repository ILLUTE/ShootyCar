using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HealthUI : MonoBehaviour
{
    public Image m_FillBar;
    public TextMeshProUGUI m_Percentage;

    private Health m_health = new Health();

    private Controller m_Controller;

    public void SetHealth(float current, float max,Controller controller)
    {
        m_health.currentHealth = current;
        m_health.maxHealth = max;
        m_Controller = controller;
        UpdateHealth(0);
    }

    public void UpdateHealth(float damage)
    {
        m_health.currentHealth += damage;

        m_health.currentHealth = Mathf.Clamp(m_health.currentHealth, 0, m_health.maxHealth);

        if(m_health.currentHealth <= 0)
        {
            m_health.currentHealth = 0;
            m_Controller.OnDead();
        }

       

        m_FillBar.fillAmount = m_health.currentHealth / m_health.maxHealth;

        m_Percentage.text = string.Format("{0}%", (100 * (m_health.currentHealth / m_health.maxHealth)).ToString("0"));
    }
}
