using System;
using UnityEngine;

public class PlayerInventoryController : MonoBehaviour
{
    #region Variables

    [SerializeField] private int _invertoryWidth;
    [SerializeField] private int _invertoryHeight;

    private Grid<InventorySlot> _inventory;

    #endregion

    #region Properties

    

    #endregion

    #region Events

    public static event Action<Grid<InventorySlot>> OnLoadInventory;
    public static event Action<InventorySlot> OnAddItem;
    public static event Action<InventorySlot> OnRemoveItem;

    #endregion

    #region MonoBehaviours

    private void Start()
    {
        Initialize();
    }

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

    private void Initialize()
    {
        if (PlayerPrefs.HasKey("Inventory"))
        {
            
        }
        else
        {
            _inventory = new Grid<InventorySlot>(_invertoryWidth, _invertoryHeight);
            
            for (int x = 0; x < _inventory.width; x++)
            {
                for (int y = 0; y < _inventory.height; y++)
                {
                    var newInventorySlot = new InventorySlot(new Vector2Int(x, y));

                    _inventory.SetValue(x, y, newInventorySlot);
                }
            }
        }
        
        OnLoadInventory?.Invoke(_inventory);
    }
    
    private void SubscribeToEvents()
    {
        ItemPickUpInteractable.OnItemInteracted += CheckItem;
        InventoryControllerUI.OnDragInventoryToInventory += HandleInventoryDrag;
        InventoryControllerUI.OnDragInventoryToEquipment += HandleInventoryEquipmentDrag;
        InventoryControllerUI.OnDragEquipmentToInventory += HandleEquipmentInventoryDrag;
        InventorySlotUI.OnRemoveItem += HandleInventoryEquipmentDrag;
    }

    private void UnsubscribeToEvents()
    {
        ItemPickUpInteractable.OnItemInteracted -= CheckItem;
        InventoryControllerUI.OnDragInventoryToInventory -= HandleInventoryDrag;
        InventoryControllerUI.OnDragInventoryToEquipment -= HandleInventoryEquipmentDrag;
        InventoryControllerUI.OnDragEquipmentToInventory -= HandleEquipmentInventoryDrag;
        InventorySlotUI.OnRemoveItem -= HandleInventoryEquipmentDrag;
    }

    private void CheckItem(SO_ItemBase item)
    {
        for (int y = 0; y < _inventory.height; y++)
        {
            for (int x = 0; x < _inventory.width; x++)
            {
                if (_inventory.GetValue(x, y).Item) continue;

                AddItemToInventory(x, y, item);
                
                return;
            }
        }
    }

    private void HandleInventoryDrag(InventorySlotUI draggedFrom, InventorySlotUI draggedTo)
    {
        if (!draggedFrom) return;
        
        var draggedFromItem =  draggedFrom.Slot.Item;
        var draggedToItem =  draggedTo.Slot.Item;
        
        RemoveItemToInventory(draggedFrom.Slot.Position.x, draggedFrom.Slot.Position.y);
        RemoveItemToInventory(draggedTo.Slot.Position.x, draggedTo.Slot.Position.y);

        AddItemToInventory(draggedFrom.Slot.Position.x, draggedFrom.Slot.Position.y, draggedToItem);
        AddItemToInventory(draggedTo.Slot.Position.x, draggedTo.Slot.Position.y, draggedFromItem);
    }
    
    private void HandleInventoryEquipmentDrag(InventorySlotUI draggedFrom)
    {
        if (!draggedFrom) return;
        
        RemoveItemToInventory(draggedFrom.Slot.Position.x, draggedFrom.Slot.Position.y);
    }
    
    private void HandleEquipmentInventoryDrag(EquipmentSlotUI draggedFrom, InventorySlotUI draggedTo)
    {
        if (!draggedFrom) return;

        AddItemToInventory(draggedTo.Slot.Position.x, draggedTo.Slot.Position.y, draggedFrom.EquipableItem);
    }
    
    private void AddItemToInventory(int xPosition, int yPosition, SO_ItemBase item)
    {
        if (!item) return;
        
        _inventory.GetValue(xPosition, yPosition).AddItem(item);

        OnAddItem?.Invoke(_inventory.GetValue(xPosition, yPosition));
    }

    private void RemoveItemToInventory(int xPosition, int yPosition)
    {
        _inventory.GetValue(xPosition, yPosition).RemoveItem();
        
        OnRemoveItem?.Invoke(_inventory.GetValue(xPosition, yPosition));
    }
    
    #endregion
}

public class InventorySlot
{
    #region Variables
    
    private SO_ItemBase _item;
    private Vector2Int _position;
    
    #endregion
    
    #region Properties
    
    public SO_ItemBase Item => _item;
    public Vector2Int Position => _position;
    
    #endregion

    #region Constructor

    public InventorySlot(Vector2Int position)
    {
        _position = position;
    }

    #endregion
    
    #region Methods

    public void AddItem(SO_ItemBase item)
    {
        _item = item;
    }
    
    public void RemoveItem()
    {
        _item = null;
    }
    
    #endregion
}