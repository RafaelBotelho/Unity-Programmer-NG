using System;
using UnityEngine;

public class PlayerGroundDetector : MonoBehaviour
{
    #region Variables / Components

    [Header("Player Grounded")]
    [SerializeField] private float _groundedOffset;
    [SerializeField] private float _groundedRadius;
    [SerializeField] private float _coyoteTime;
    [SerializeField] private LayerMask _groundLayers;

    private bool _isGrounded;
    private float _coyoteTimeCounter;
    
    private PlayerGroundedState _currentGroundedState;

    #endregion

    #region Properties

    public bool isGrounded => _isGrounded;
    public float coyoteTimeCounter => _coyoteTimeCounter;

    #endregion

    #region Events

    public static event Action<PlayerGroundedState> OnPlayerChangedGroundedState;
    
    #endregion
    
    #region Monobehaviour
    
    private void Update()
    {
        CheckGrounded();
        GravityState();
    }

    private void OnDrawGizmosSelected()
    {
        DrawGroundedGizmos();
    }

    #endregion

    #region Methods
    
    private void CheckGrounded()
    {
        var checkPosition = transform.position;
        Vector3 spherePosition = new Vector3(checkPosition.x, checkPosition.y - _groundedOffset, checkPosition.z);
        _isGrounded = Physics.CheckSphere(spherePosition, _groundedRadius, _groundLayers, QueryTriggerInteraction.Ignore);

        if (_isGrounded)
            _coyoteTimeCounter = _coyoteTime;
        else
            _coyoteTimeCounter -= Time.deltaTime;
    }

    private void GravityState()
    {
        switch (_currentGroundedState)
        {
            case PlayerGroundedState.GROUNDED:
                if (!isGrounded)
                    ChangeGroundedState(PlayerGroundedState.INAIR);
                break;
            case PlayerGroundedState.INAIR:
                if (isGrounded)
                    ChangeGroundedState(PlayerGroundedState.GROUNDED);
                break;
        }
    }
    
    private void DrawGroundedGizmos()
    {
        var checkPosition = transform.position;
        var transparentGreen = new Color(0.0f, 1.0f, 0.0f, 0.35f);
        var transparentRed = new Color(1.0f, 0.0f, 0.0f, 0.35f);

        Gizmos.color = _isGrounded ? transparentGreen : transparentRed;
        Gizmos.DrawSphere(new Vector3(checkPosition.x, checkPosition.y - _groundedOffset, checkPosition.z), _groundedRadius);
    }

    private void ChangeGroundedState(PlayerGroundedState newGroundedState)
    {
        if(newGroundedState == _currentGroundedState) return;
        
        _currentGroundedState = newGroundedState;

        OnPlayerChangedGroundedState?.Invoke(newGroundedState);
    }
    
    #endregion
}