using System;
using UnityEngine;

public class PlayerMovementController : MonoBehaviour
{
    #region Variables

    #region Inspector

    [Header("Movement")]
	[SerializeField] private float _moveSpeed;
	[SerializeField] private float _sprintSpeed;
	[SerializeField] private float _airSpeed;
	[SerializeField] private float _dodgeSpeed;
	[Range(0.0f, 0.3f)]
	[SerializeField] private float _rotationSmoothTime;
	[SerializeField] private float _speedChangeRate;

	[Header("Jump")]
	[Space(10)]
	[SerializeField] private float _jumpHeight;
	[SerializeField] private float _gravity;
	[SerializeField] private float _airAttackGravity;
	[SerializeField] private float _jumpBuffer;

	#endregion

	#region Private
	
	// player
	private float _speed;
	private float _targetRotation;
	private float _rotationVelocity;
	private float _verticalVelocity;
	private float _targetSpeed;

	//Jump
	private bool _doubleJumped;
	private float _jumpButtonPressedTime;

	//Dodge
	private bool _dodgeMovement;

	private GameObject _mainCamera;
	private CharacterController _characterController;

	//References
	private PlayerInputController _inputController;
	private PlayerGroundDetector _groundDetector;

	private PlayerGroundedState _currentGroundedState;
	
	private const float _terminalVelocity = 53.0f;
	
	#endregion

	#endregion

	#region Properties

	public float SpeedChangeRate => _speedChangeRate;
	public float TargetSpeed => _targetSpeed;

	#endregion

	#region Events

	public static event Action OnPlayerJumped;
	public static event Action OnPlayerDoubleJumped;

	#endregion
	
	#region Monobehaviour

	private void Awake()
	{
		GetReferences();
	}

    private void OnEnable()
    {
		SubscribeToEvents();
    }

    private void OnDisable()
    {
		UnsubscribeToEvents();
    }

    private void Update()
	{
		TryToMove();
		TryToJump();
		Gravity();
	}

    #endregion

    #region Methods

    #region Initialization

    private void GetReferences()
    {
		_inputController = GetComponent<PlayerInputController>();
		_groundDetector = GetComponent<PlayerGroundDetector>();
		_characterController = GetComponent<CharacterController>();
		_mainCamera = Camera.main.gameObject;
    }

	private void SubscribeToEvents()
	{
		PlayerInputController.OnJumpButtonPressed += SetJumpInputTime;
	    PlayerGroundDetector.OnPlayerChangedGroundedState += SetCurrentGroundedState;
    }

	private void UnsubscribeToEvents()
    {
	    PlayerInputController.OnJumpButtonPressed -= SetJumpInputTime;
	    PlayerGroundDetector.OnPlayerChangedGroundedState -= SetCurrentGroundedState;
    }

    #endregion

    #region Input Movement

    private void TryToMove()
	{
		if(GameStateManager.currentGameState != GameState.GAMEPLAY) return;
		
		_targetSpeed = _inputController.Sprint? _sprintSpeed : _moveSpeed;

		if (_currentGroundedState == PlayerGroundedState.INAIR)
			_targetSpeed = _inputController.Sprint ? _airSpeed : _moveSpeed;

		if (_inputController.Move == Vector2.zero) _targetSpeed = 0.0f;

		if (_inputController.Move != Vector2.zero)
			_targetRotation = Mathf.Atan2(InputDirection().x, InputDirection().z) * Mathf.Rad2Deg +
			                  _mainCamera.transform.eulerAngles.y;

		MoveCharacter(_targetSpeed * InputMagnitude());
	}

    #endregion
    
    #region Jump

    private void SetJumpInputTime()
    {
	    if(GameStateManager.currentGameState != GameState.GAMEPLAY) return;

		_jumpButtonPressedTime = _jumpBuffer;
    }

	private void TryToJump()
    {
		_jumpButtonPressedTime -= Time.deltaTime;

		if (_groundDetector.coyoteTimeCounter > 0f && _jumpButtonPressedTime > 0f && _verticalVelocity <= 0)
		{
			_verticalVelocity = Mathf.Sqrt(_jumpHeight * -2f * _gravity);
			_jumpButtonPressedTime = 0;

			OnPlayerJumped?.Invoke();
		}
		else if (!_doubleJumped && _jumpButtonPressedTime > 0f && _groundDetector.coyoteTimeCounter <= 0f)
        {
	        _verticalVelocity = Mathf.Sqrt(_jumpHeight * -2f * _gravity);
			_doubleJumped = true;

			OnPlayerDoubleJumped?.Invoke();
		}
	}

    #endregion

    #region Gravity
    
    private void Gravity()
    {
	    if(GameStateManager.currentGameState != GameState.GAMEPLAY) return;
	    
	    if (_groundDetector.isGrounded)
	    {
		    _doubleJumped = false;

		    if (_verticalVelocity < 0.0f)
			    _verticalVelocity = -4f;
	    }

	    if (_verticalVelocity < _terminalVelocity)
		    _verticalVelocity += _gravity * Time.deltaTime;
    }

    #endregion
    
	#region General

	private void MoveCharacter(float targetSpeed)
	{
		var currentHorizontalSpeed = new Vector3(_characterController.velocity.x, 0.0f, _characterController.velocity.z).magnitude;
		const float speedOffset = 0.1f;

		if (currentHorizontalSpeed < targetSpeed - speedOffset || currentHorizontalSpeed > targetSpeed + speedOffset)
		{
			_speed = Mathf.Lerp(currentHorizontalSpeed, targetSpeed, Time.deltaTime * _speedChangeRate);
			_speed = Mathf.Round(_speed * 1000f) / 1000f;
		}
		else
			_speed = targetSpeed;

		var rotation = Mathf.SmoothDampAngle(transform.eulerAngles.y, _targetRotation, ref _rotationVelocity, _rotationSmoothTime);

		transform.rotation = Quaternion.Euler(0.0f, rotation, 0.0f);

		var targetDirection = Quaternion.Euler(0.0f, _targetRotation, 0.0f) * Vector3.forward;

		_characterController.Move(targetDirection.normalized * (_speed * Time.deltaTime) + new Vector3(0.0f, _verticalVelocity, 0.0f) * Time.deltaTime);
	}

	private void SetCurrentGroundedState(PlayerGroundedState newState)
	{
		_currentGroundedState = newState;
	}
	
	public float InputMagnitude()
    {
		return _inputController.AnalogMovement ? _inputController.Move.magnitude : 1f;
	}

	private Vector3 InputDirection()
    {
		return new Vector3(_inputController.Move.x, 0.0f, _inputController.Move.y).normalized;
	}

	#endregion

	#endregion
}