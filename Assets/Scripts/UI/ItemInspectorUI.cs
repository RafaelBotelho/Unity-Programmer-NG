using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemInspectorUI : MonoBehaviour
{
    #region Variables

    [SerializeField] private GameObject _inspectorPanel;
    [SerializeField] private Image _icon;
    [SerializeField] private TextMeshProUGUI _description;
    
    private IUiDraggable _item;

    #endregion
    
    #region Properties

    

    #endregion
    
    #region MonoBehaviours

    

    #endregion
    
    #region Methods

    public void SetItem(IUiDraggable item)
    {
        if (item == _item)
        {
            _inspectorPanel.SetActive(false);
            _icon.enabled = false;
            _item = null;
            
            return;
        }
        
        _inspectorPanel.SetActive(true);
        _item = item;
        
        var inventoryItem = _item as InventorySlotUI;
        var equipmentItem = _item as EquipmentSlotUI;

        if (inventoryItem)
        {
            _icon.sprite = inventoryItem.Slot.Item.Icon;
            _description.text = inventoryItem.Slot.Item.Description;
        }
        if (equipmentItem)
        {
            _icon.sprite = equipmentItem.EquipableItem.Icon;
            _description.text = equipmentItem.EquipableItem.Description;
        }
        
        _icon.enabled = true;
    }
    
    public void DiscardItem()
    {
        _item.RemoveItem();
        _inspectorPanel.SetActive(false);
        _icon.enabled = false;
        _item = null;
    }

    #endregion
}
