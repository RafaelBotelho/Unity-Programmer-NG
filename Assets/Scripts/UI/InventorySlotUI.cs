using UnityEngine;
using UnityEngine.UI;

public class InventorySlotUI : MonoBehaviour, IUiDraggable
{
    #region Variables

    [SerializeField] private Image _icon;
    [SerializeField] private InventoryControllerUI _inventoryControllerUI;
    [SerializeField] private Sprite _startIcon;
    
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

        if (!_startIcon)
        {
            _icon.enabled = false;
            _icon.sprite = null;
        }
        else
            _icon.sprite = _startIcon;
    }

    public void EndDragging()
    {
        _inventoryControllerUI.EndDragging(this);
    }
    
    #endregion
}
