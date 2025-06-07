using UnityEngine;

[CreateAssetMenu(fileName = "New Consumable Item", menuName = "Itens/Consumable Item/ New Consumable Item")]
public class SO_ConsumableItem : SO_ItemBase
{
    #region Enum

    public enum TargetStatus { BasicAttack, SpecialAttack, Hp, Defense, MovementSpeed, CriticalRate }

    #endregion

    #region Variables

    [SerializeField] private TargetStatus _targetStatus = TargetStatus.BasicAttack;
    [SerializeField] private float _statsModifierAmount = 0;
    [SerializeField] private int _maxStack = 0;

    #endregion

    #region Properties

    public TargetStatus targetStatus => _targetStatus;
    public float statsModifierAmount => _statsModifierAmount;
    public int maxStack => _maxStack;

    #endregion
}
