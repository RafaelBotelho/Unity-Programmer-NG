using System;
using UnityEngine;

public class NpcInteractable : MonoBehaviour
{
    #region Variables

    [SerializeField] private SO_ItemBase _item;
    [SerializeField] private bool _isInteractable;

    #endregion

    #region Events

    public static event Action<SO_ItemBase> OnItemInteracted;

    #endregion
    
    #region Properties
    
    public bool isInteractable => _isInteractable;

    #endregion

    #region Methods

    public string GetDescription()
    {
        return _item.InteractionDescription + _item.ItemName;
    }

    public void Interact()
    {
        if(!_isInteractable) return;
        
        OnItemInteracted?.Invoke(_item);
    }

    #endregion
}
