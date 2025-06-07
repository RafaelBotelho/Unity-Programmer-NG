using UnityEngine;
using UnityEngine.UI;

public class InventorySlotUI : MonoBehaviour
{
    #region Variables

    [SerializeField] private Image _icon;
    [SerializeField] private InventoryControllerUI _inventoryControllerUI;
    
    private InventorySlot _slot;

    #endregion
    
    #region Properties

    public InventorySlot Slot => _slot;

    #endregion
    
    #region Methods

    public void SetSlot(InventorySlot slot)
    {
        _slot  = slot;

        if (slot.Item)
        {
            _icon.sprite = _slot.Item.Icon;
            _icon.enabled = true;
        }
            
    }

    public void StartDragging()
    {
        if (!_slot.Item) return;
        
        _inventoryControllerUI.StartDragging(this);
        _icon.enabled = false;
        _icon.sprite = null;
    }

    public void EndDragging()
    {
        _inventoryControllerUI.EndDragging(this);
    }
    
    #endregion
}
