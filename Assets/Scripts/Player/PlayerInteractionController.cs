using System;
using UnityEngine;

public class PlayerInteractionController : MonoBehaviour
{
    #region Variables

    [SerializeField] private float _raySize;
    [SerializeField] private float _yOffset;

    private Transform _myTransform;
    private IInteractable _currentInteractable;

    #endregion

    #region Events

    public static event Action<IInteractable> OnLookedToInteractable;
    public static event Action<IInteractable> OnLookedAwayFromInteractable;
    public static event Action OnPlayerInteracted;

    #endregion
    
    #region Monobehaviour

    private void Awake()
    {
        GetReferences();
    }

    private void OnEnable()
    {
        SubscribeToEvents();
    }

    private void OnDisable()
    {
        UnsubscribeToEvents();
    }

    private void Update()
    {
        FindInteractables();
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(transform.position + Vector3.up * _yOffset, transform.forward * _raySize);
    }

    #endregion

    #region Methods

    #region Initialization

    private void GetReferences()
    {
        _myTransform = transform;
    }
    
    private void SubscribeToEvents()
    {
        PlayerInputController.OnInteractButtonPressed += Interact;
    }

    private void UnsubscribeToEvents()
    {
        PlayerInputController.OnInteractButtonPressed -= Interact;
    }

    #endregion

    private void FindInteractables()
    {
        if (Physics.Raycast(_myTransform.position + Vector3.up * _yOffset, _myTransform.forward, out RaycastHit hit,
                _raySize))
        {
            if (hit.collider.TryGetComponent<IInteractable>(out var interactable))
                SetInteractable(interactable);
            else if (_currentInteractable != null)
                RemoveInteractable(_currentInteractable);
        }
        else if (_currentInteractable != null)
            RemoveInteractable(_currentInteractable);
    }
    
    private void SetInteractable(IInteractable newInteractable)
    {
        _currentInteractable = newInteractable;
        OnLookedToInteractable?.Invoke(_currentInteractable);
    }

    private void RemoveInteractable(IInteractable oldInteractable)
    {
        if (_currentInteractable == oldInteractable)
            _currentInteractable = null;
        
        OnLookedAwayFromInteractable?.Invoke(_currentInteractable);
    }

    private void Interact()
    {
        if(_currentInteractable == null) return;
        
        _currentInteractable.Interact();
        RemoveInteractable(_currentInteractable);
        
        OnPlayerInteracted?.Invoke();
    }
    
    #endregion
}