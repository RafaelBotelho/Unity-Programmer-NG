using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class DragContainerUI : MonoBehaviour
{
    #region Varriables

    [SerializeField] private InputAction _mousePositionAction;

    #endregion
    
    #region MonoBehaviours

    private void OnEnable()
    {
        EnableInput();
    }

    private void OnDisable()
    {
        DisableInput();
    }

    private void Update()
    {
        UpdatePosition();
    }

    #endregion
    
    #region Methods

    private void EnableInput()
    {
        _mousePositionAction.Enable();
    }
    
    private void DisableInput()
    {
        _mousePositionAction.Disable();
    }
    
    private void UpdatePosition()
    {
        transform.position = _mousePositionAction.ReadValue<Vector2>();
    }

    #endregion
}
