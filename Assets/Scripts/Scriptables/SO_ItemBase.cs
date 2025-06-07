using UnityEngine;

public class SO_ItemBase : ScriptableObject
{
    #region Variables
    
    [SerializeField] private string _itemName;
    [SerializeField] private string _interactionDescription;

    #endregion

    #region Properties
    
    public string ItemName => _itemName;
    public string InteractionDescription => _interactionDescription;

    #endregion
}
