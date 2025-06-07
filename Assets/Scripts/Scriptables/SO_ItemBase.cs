using UnityEngine;

public class SO_ItemBase : ScriptableObject
{
    #region Variables
    
    [SerializeField] private string _itemName;
    [SerializeField] private string _interactionDescription;
    [SerializeField] private Sprite _icon;

    #endregion

    #region Properties
    
    public string ItemName => _itemName;
    public string InteractionDescription => _interactionDescription;
    public Sprite Icon => _icon;

    #endregion
}
