using UnityEngine;
using UnityEngine.Events;

public class NpcInteractable : MonoBehaviour, IInteractable
{
    #region Variables

    [SerializeField] private string _InteractionDescription;
    [SerializeField] private bool _isInteractable;

    #endregion

    #region Events

    public UnityEvent OnInteract;

    #endregion

    #region Properties

    public bool isInteractable => _isInteractable;

    #endregion

    #region Methods

    public string GetDescription()
    {
        return _InteractionDescription;
    }

    public void Interact()
    {
        if (!_isInteractable) return;

        OnInteract?.Invoke();
    }

    #endregion
}
