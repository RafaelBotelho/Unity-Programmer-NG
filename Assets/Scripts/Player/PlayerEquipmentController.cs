using System;
using UnityEngine;

public class PlayerEquipmentController : MonoBehaviour
{
    #region Variables

    [SerializeField] private SO_PlayerEquipment _playerEquipmentBase;
    [SerializeField] private SO_PlayerEquipment _playerEquipmentRuntime;

    #endregion

    #region Properties

    public SO_PlayerEquipment PlayerEquipment => _playerEquipmentRuntime;

    #endregion

    #region Events
    
    public static event Action<SO_WeaponItem> OnPlayerEquippedWeapon;
    public static event Action<SO_EquipableItem> OnPlayerEquippedEquipment;
    public static event Action<EquipmentType> OnPlayerRemovedEquipment;
    
    #endregion
    
    #region Monobehaviour

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
        ResetEquipment();
    }

    private void SubscribeToEvents()
    {
        EquipmentSlotUI.OnEquipItem += CheckItem;
        EquipmentSlotUI.OnRemoveItem += HandleItemRemoved;
    }

    private void UnsubscribeToEvents()
    {
        EquipmentSlotUI.OnEquipItem -= CheckItem;
        EquipmentSlotUI.OnRemoveItem -= HandleItemRemoved;
    }

    private void ResetEquipment()
    {
        if (_playerEquipmentBase.Belt)
            ChangeEquipment(_playerEquipmentBase.Belt);
        else
            RemoveEquipment(EquipmentType.BELT);

        if (_playerEquipmentBase.BodyCloth)
            ChangeEquipment(_playerEquipmentBase.BodyCloth);
        else
            RemoveEquipment(EquipmentType.BODYCLOTH);

        if (_playerEquipmentBase.Glove)
            ChangeEquipment(_playerEquipmentBase.Glove);
        else
            RemoveEquipment(EquipmentType.GLOVE);

        if (_playerEquipmentBase.Hat)
            ChangeEquipment(_playerEquipmentBase.Hat);
        else
            RemoveEquipment(EquipmentType.HAT);

        if (_playerEquipmentBase.Shoe)
            ChangeEquipment(_playerEquipmentBase.Shoe);
        else
            RemoveEquipment(EquipmentType.SHOE);

        if (_playerEquipmentBase.Weapon)
            ChangeWeapon(_playerEquipmentBase.Weapon);
        else
            RemoveEquipment(EquipmentType.WEAPON);
    }

    private void HandleItemRemoved(SO_EquipableItem item)
    {
        switch (item.EquipmentType)
        {
            case EquipmentType.BELT:
                RemoveEquipment(EquipmentType.BELT);
                break;
            case EquipmentType.BODYCLOTH:
                ChangeEquipment(_playerEquipmentBase.BodyCloth);
                break;
            case EquipmentType.GLOVE:
                ChangeEquipment(_playerEquipmentBase.Glove);
                break;
            case EquipmentType.HAT:
                RemoveEquipment(EquipmentType.HAT);
                break;
            case EquipmentType.SHOE:
                ChangeEquipment(_playerEquipmentBase.Shoe);
                break;
            case EquipmentType.WEAPON:
                ChangeEquipment(_playerEquipmentBase.Weapon);
                break;
        }
    }
    
    private void CheckItem(SO_ItemBase item)
    {
        ChangeEquipment(item as SO_EquipableItem);
        ChangeWeapon(item as SO_WeaponItem);
    }

    private void ChangeWeapon(SO_WeaponItem newWeapon)
    {
        if (!newWeapon) return;

        _playerEquipmentRuntime.SetNewWeapon(newWeapon);

        OnPlayerEquippedWeapon?.Invoke(newWeapon);
    }

    private void ChangeEquipment(SO_EquipableItem equipment)
    {
        if (!equipment) return;
        
        _playerEquipmentRuntime.SetNewEquipment(equipment);

        OnPlayerEquippedEquipment?.Invoke(equipment);
    }

    private void RemoveEquipment(EquipmentType equipmentType)
    {
        _playerEquipmentRuntime.RemoveEquipment(equipmentType);

        OnPlayerRemovedEquipment?.Invoke(equipmentType);
    }

    #endregion
}
