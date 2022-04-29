using UnityEngine;
using UnityEngine.InputSystem;

public class UIActivator : MonoBehaviour
{
    [SerializeField] private InputActionReference _activateUIButton;
    [SerializeField] private GameObject UIPanel;

    private void Awake()
    {
        _activateUIButton.action.performed += ActivateUI;
        Debug.Log("UI Activator is wake up1");
    }

    private void ActivateUI(InputAction.CallbackContext obj)
    {
        Debug.Log("Panel is activated!");
        UIPanel.SetActive(true);
    }
}
