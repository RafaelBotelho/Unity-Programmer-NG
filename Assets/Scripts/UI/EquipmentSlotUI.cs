using System;
using UnityEngine;
using UnityEngine.UI;

public class EquipmentSlotUI : MonoBehaviour, IUiDraggable
{
    #region Variables

    [SerializeField] private EquipmentType _equipmentType;
    [SerializeField] private Image _icon;
    [SerializeField] private InventoryControllerUI _inventoryControllerUI;
    [SerializeField] private ItemInspectorUI _itemInspectorUI;
    [SerializeField] private Sprite _startIcon;
    
    private SO_EquipableItem _equipableItem;

    #endregion
    
    #region Properties

    public EquipmentType EquipmentType => _equipmentType;
    public SO_EquipableItem EquipableItem => _equipableItem;

    #endregion

    #region Events

    public static event Action<SO_ItemBase> OnEquipItem;
    public static event Action<SO_EquipableItem> OnRemoveItem;

    #endregion

    #region Methods
    
    public void SetEquipment(SO_EquipableItem item)
    {
        _equipableItem = item;
        
        _icon.sprite = _equipableItem.Icon;
        _icon.enabled = true;

        OnEquipItem?.Invoke(_equipableItem);
    }

    public void InspectItem()
    {
        if (!_equipableItem) return;
        
        _itemInspectorUI.SetItem(this);
    }
    
    public void RemoveItem()
    {
        if (!_startIcon)
        {
            _icon.enabled = false;
            _icon.sprite = null;
        }
        else
            _icon.sprite = _startIcon;
        
        OnRemoveItem?.Invoke(_equipableItem);
    }
    
    public void StartDragging()
    {
        if (!_equipableItem) return;
        
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
