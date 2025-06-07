using UnityEngine;

[CreateAssetMenu(fileName = "New Equippable Item", menuName = "Itens/Equippable Item/New Equippable Item")]
public class SO_EquipableItem : SO_ItemBase
{
    #region Variables
    
    [SerializeField] private EquipmentType equipmentType = EquipmentType.BELT;
    [SerializeField] private int _visualIndex;

    #endregion

    #region Properties

    public EquipmentType EquipmentType => equipmentType;
    public int VisualIndex => _visualIndex;

    #endregion
}
