using UnityEngine;
using Cinemachine;

public class PanCamera : MonoBehaviour
{
    [SerializeField]
    private CinemachineVirtualCamera m_Camera;

    [SerializeField]
    private SelectionState cameraState;

    private void Awake()
    {
        if(m_Camera == null)
        {
            m_Camera = GetComponent<CinemachineVirtualCamera>();
        }    
        CustomizationManager.OnStateChanged += OnStateChanged;
    }

    private void OnStateChanged(SelectionState state)
    {
        m_Camera.Priority = 0;

        if(state == cameraState)
        {
            m_Camera.Priority = 10; 
        }
    }
}
