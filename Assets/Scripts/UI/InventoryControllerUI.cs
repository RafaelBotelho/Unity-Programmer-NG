using System;
using UnityEngine;
using UnityEngine.UI;

public class InventoryControllerUI : MonoBehaviour
{
    #region Variables

    [SerializeField] private InventorySlotUI[]  _inventorySlots;
    [SerializeField] private Image _dragContainer;
    [SerializeField] private GameObject _inventoryPanel;

    private IUiDraggable _draggedFrom;
    private IUiDraggable _draggedTo;

    #endregion

    #region Events

    public static event Action<InventorySlotUI, InventorySlotUI> OnDragInventoryToInventory;
    public static event Action<InventorySlotUI> OnDragInventoryToEquipment;
    public static event Action<EquipmentSlotUI, InventorySlotUI> OnDragEquipmentToInventory;
    
    #endregion
    
    #region MonoBehaviours

    private void OnEnable()
    {
        SubscribeToEvents();
    }

    private void OnDisable()
    {
        UnsubscribeToEvents();
    }

    #endregion
    
    #region Methods

    private void SubscribeToEvents()
    {
        PlayerInventoryController.OnLoadInventory += InitializeInventory;
        PlayerInventoryController.OnAddItem += UpdateInventorySlot;
        PlayerInventoryController.OnRemoveItem += UpdateInventorySlot;
        GameStateManager.OnGameStateChanged += ChangePanelStatus;
    }

    private void UnsubscribeToEvents()
    {
        PlayerInventoryController.OnLoadInventory -= InitializeInventory;
        PlayerInventoryController.OnAddItem -= UpdateInventorySlot;
        PlayerInventoryController.OnRemoveItem -= UpdateInventorySlot;
        GameStateManager.OnGameStateChanged -= ChangePanelStatus;
    }
    
    private void InitializeInventory(Grid<InventorySlot> inventory)
    {
        var count = 0;
        
        for (int y = 0; y < inventory.height; y++)
        {
            for (int x = 0; x < inventory.width; x++)
            {
                _inventorySlots[count].SetSlot(inventory.GetValue(x, y));
                count++;
            }
        }
    }

    private void ChangePanelStatus(GameState newState)
    {
        switch (newState)
        {
            case GameState.PAUSE:
                _inventoryPanel.SetActive(false);
                break;
            case GameState.GAMEPLAY:
                _inventoryPanel.SetActive(false);
                break;
            case GameState.INVENTORY:
                _inventoryPanel.SetActive(true);
                break;
        }
    }
    
    private void UpdateInventorySlot(InventorySlot slot)
    {
        foreach (var inventorySlot in _inventorySlots)
        {
            if (inventorySlot.Slot.Position != slot.Position) continue;
            
            inventorySlot.SetSlot(slot);
            break;
        }
    }
    
    public void StartDragging(IUiDraggable draggingSlot)
    {
        var inventory = draggingSlot as InventorySlotUI;
        var equipment = draggingSlot as EquipmentSlotUI;
        
        _draggedFrom = draggingSlot;
        _dragContainer.gameObject.SetActive(true);
        _dragContainer.sprite = inventory ? inventory.Slot.Item.Icon : equipment.EquipableItem.Icon;
    }
    
    public void EndDragging(IUiDraggable draggingSlot)
    {
        var draggedToInventory = draggingSlot as InventorySlotUI;
        var draggedToEquipment = draggingSlot as EquipmentSlotUI;
        var draggedFromInventory = _draggedFrom as InventorySlotUI;
        var draggedFromEquipment = _draggedFrom as EquipmentSlotUI;
        
        _draggedTo = draggingSlot;

        if (draggedToInventory && draggedFromInventory)
        {
            OnDragInventoryToInventory?.Invoke(draggedToInventory, draggedFromInventory);
        
            DisableDrag();
        }

        if (draggedToInventory && draggedFromEquipment)
        {
            draggedFromEquipment.RemoveItem();
            
            OnDragEquipmentToInventory?.Invoke(draggedFromEquipment, draggedToInventory);
        
            DisableDrag();
        }

        if (draggedFromInventory && draggedToEquipment)
        {
            var inventoryItem = draggedFromInventory.Slot.Item as SO_EquipableItem;
            
            if (!inventoryItem || inventoryItem.EquipmentType != draggedToEquipment.EquipmentType)
            {
                UpdateInventorySlot(draggedFromInventory.Slot);
                DisableDrag();
                
                return;
            }
            
            draggedToEquipment.SetEquipment(draggedFromInventory.Slot.Item as SO_EquipableItem);
            OnDragInventoryToEquipment?.Invoke(draggedFromInventory);
        
            DisableDrag();
        }
        
        if (draggedToEquipment && draggedFromEquipment)
        {
            draggedFromEquipment.SetEquipment(draggedFromEquipment.EquipableItem);
            DisableDrag();
        }
    }
    
    public void CancelDrag()
    {
        var uiInventorySlot = _draggedFrom as InventorySlotUI;
        var uiEquipmentSlot = _draggedFrom as EquipmentSlotUI;

        if (uiInventorySlot)
            UpdateInventorySlot(uiInventorySlot.Slot);
        
        if (uiEquipmentSlot)
            uiEquipmentSlot.SetEquipment(uiEquipmentSlot.EquipableItem);
        
        DisableDrag();
    }

    private void DisableDrag()
    {
        _dragContainer.gameObject.SetActive(false);
        _draggedFrom = null;
        _draggedTo = null;
    }
    
    #endregion
}
