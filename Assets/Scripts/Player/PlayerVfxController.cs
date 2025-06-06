using UnityEngine;

public class PlayerVfxController : MonoBehaviour
{
    #region Variables / Components

    [SerializeField] private VfxController _walkVfxController;
    [SerializeField] private VfxController _jumpVfxController;
    [SerializeField] private VfxController _doubleJumpVfxController;
    [SerializeField] private VfxController _landVfxController;
    
    private PlayerGroundedState _currentGroundedState = default;

    #endregion

    #region Monobehaviour

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
        PlayerGroundDetector.OnPlayerChangedGroundedState += SetCurrentGroundedState;
        PlayerMovementController.OnPlayerJumped += Jump;
        PlayerMovementController.OnPlayerDoubleJumped += DoubleJump;
    }

    private void UnsubscribeToEvents()
    {
        PlayerGroundDetector.OnPlayerChangedGroundedState -= SetCurrentGroundedState;
        PlayerMovementController.OnPlayerJumped -= Jump;
        PlayerMovementController.OnPlayerDoubleJumped -= DoubleJump;
    }
    
    private void SetCurrentGroundedState(PlayerGroundedState newState)
    {
        _currentGroundedState = newState;
        
        if(_currentGroundedState == PlayerGroundedState.GROUNDED)
            Land();
    }
    
    public void Walk()
    {
        _walkVfxController.SpawnVfx();
    }
    
    private void Jump()
    {
        _jumpVfxController.SpawnVfx();
    }
    private void DoubleJump()
    {
        _doubleJumpVfxController.SpawnVfx();
    }
    
    private void Land()
    {
        _landVfxController.SpawnVfx();
    }

    #endregion
}
