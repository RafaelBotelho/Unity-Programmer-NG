using UnityEngine;

[CreateAssetMenu(fileName = "New Player Visual", menuName = "Player/New Player Visual")]
public class SO_PlayerVisual : ScriptableObject
{
    #region Variables
    
    [SerializeField] private int _hairIndex;

    #endregion

    #region Properties
    
    public int HairIndex => _hairIndex;

    #endregion
}
