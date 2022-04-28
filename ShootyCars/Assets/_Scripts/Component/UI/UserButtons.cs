using DG.Tweening;
using UnityEngine;

public class UserButtons : MonoBehaviour
{
    public ButtonType button;

    [SerializeField]
    private RectTransform m_RectTransform;

    private void Awake()
    {
        if (m_RectTransform == null)
        {
            m_RectTransform = GetComponent<RectTransform>();
        }
    }
    
    public void ButtonPressed()
    {
        m_RectTransform.DOScale(Vector2.one * 0.45f, 0.1f);

        InputManager.Instance.ButtonPressed(button);
    }

    public void ButtonReleased()
    {
        m_RectTransform.DOScale(Vector2.one, 0.1f);

        InputManager.Instance.ButtonReleased(button);
    }
}
