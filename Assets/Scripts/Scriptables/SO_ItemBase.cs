using UnityEngine;

public class SO_ItemBase : ScriptableObject
{
    #region Variables
    
    [SerializeField] private string _itemName;
    [SerializeField] private string _interactionDescription;
    [SerializeField] private string _description;
    [SerializeField] private Sprite _icon;

    #endregion

    #region Properties
    
    public string ItemName => _itemName;
    public string InteractionDescription => _interactionDescription;
    public string Description => _description;
    public Sprite Icon => _icon;

    #endregion
}
