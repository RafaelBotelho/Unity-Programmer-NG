using System;
using UnityEngine;
using UnityEngine.UI;

public class InventoryControllerUI : MonoBehaviour
{
    #region Variables

    [SerializeField] private InventorySlotUI[]  _inventorySlots;
    [SerializeField] private Image _dragContainer;
    [SerializeField] private GameObject _inventoryPanel;

    private InventorySlotUI _draggedFrom;
    private InventorySlotUI _draggedTo;

    #endregion

    #region Events

    public static event Action<InventorySlotUI, InventorySlotUI> OnDragPerformed;
    
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
    
    public void StartDragging(InventorySlotUI draggingSlot)
    {
        _draggedFrom = draggingSlot;

        _dragContainer.gameObject.SetActive(true);
        _dragContainer.sprite = draggingSlot.Slot.Item.Icon;
    }
    
    public void EndDragging(InventorySlotUI draggingSlot)
    {
        _draggedTo = draggingSlot;

        OnDragPerformed?.Invoke(_draggedFrom, _draggedTo);
        
        _dragContainer.gameObject.SetActive(false);
        _draggedFrom = null;
        _draggedTo = null;
    }
    
    public void CancelDrag()
    {
        UpdateInventorySlot(_draggedFrom.Slot);
        
        _dragContainer.gameObject.SetActive(false);
        _draggedFrom = null;
        _draggedTo = null;
    }
    
    #endregion
}
