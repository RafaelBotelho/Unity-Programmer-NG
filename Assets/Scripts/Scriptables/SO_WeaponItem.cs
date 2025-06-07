using UnityEngine;

[CreateAssetMenu(fileName = "New Weapon", menuName = "Itens/Weapon/New Weapon")]
public class SO_WeaponItem : SO_EquipableItem
{
    #region Variables

    [Header("Weapon")]
    [SerializeField] private int _leftHandIndex;
    [SerializeField] private int _rightHandIndex;

    #endregion

    #region Properties

    public int LeftHandIndex => _leftHandIndex;
    public int RightHandIndex => _rightHandIndex;

    #endregion
}
