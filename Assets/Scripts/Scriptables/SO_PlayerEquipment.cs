using UnityEngine;

[CreateAssetMenu(fileName = "New Player Equipments Preset", menuName = "Player/New Player Equipments Preset")]
public class SO_PlayerEquipment : ScriptableObject
{
    #region Variables

    [SerializeField] private SO_EquipableItem _belt;
    [SerializeField] private SO_EquipableItem _bodyCloth;
    [SerializeField] private SO_EquipableItem _glove;
    [SerializeField] private SO_EquipableItem _hat;
    [SerializeField] private SO_EquipableItem _shoe;
    [SerializeField] private SO_WeaponItem _weapon;

    #endregion

    #region Properties

    public SO_EquipableItem Belt => _belt;
    public SO_EquipableItem BodyCloth => _bodyCloth;
    public SO_EquipableItem Glove => _glove;
    public SO_EquipableItem Hat => _hat;
    public SO_EquipableItem Shoe => _shoe;
    public SO_WeaponItem Weapon => _weapon;

    #endregion

    #region Methods

    public void SetNewEquipment(SO_EquipableItem equipment)
    {
        switch (equipment.EquipmentType)
        {
            case EquipmentType.BELT:
                _belt = equipment;
                break;
            case EquipmentType.BODYCLOTH:
                _bodyCloth = equipment;
                break;
            case EquipmentType.GLOVE:
                _glove = equipment;
                break;
            case EquipmentType.HAT:
                _hat = equipment;
                break;
            case EquipmentType.SHOE:
                _shoe = equipment;
                break;
        }
    }

    public void SetNewWeapon(SO_WeaponItem newWeapon)
    {
        _weapon = newWeapon;
    }

    public void RemoveEquipment(EquipmentType equipmentType)
    {
        switch (equipmentType)
        {
            case EquipmentType.BELT:
                _belt = null;
                break;
            case EquipmentType.BODYCLOTH:
                _bodyCloth = null;
                break;
            case EquipmentType.GLOVE:
                _glove = null;
                break;
            case EquipmentType.HAT:
                _hat = null;
                break;
            case EquipmentType.SHOE:
                _shoe = null;
                break;
            case EquipmentType.WEAPON:
                _weapon = null;
                break;
        }
    }

    #endregion
}