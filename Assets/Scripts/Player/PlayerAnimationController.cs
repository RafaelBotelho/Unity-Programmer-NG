using UnityEngine;

public class PlayerAnimationController : MonoBehaviour
{
    #region Variables

    [SerializeField] private float _fallTimeout;

    private Animator _animator;
    private PlayerInputController _playerInput;
    private PlayerMovementController _movementController;
    private PlayerGroundDetector _groundDetector;

    private float _fallTimeoutDelta;
    private float _animationBlend;

    private int _animIDSpeed;
    private int _animIDHorizontal;
    private int _animIDVertical;
    private int _animIDGrounded;
    private int _animIDJump;
    private int _animIDDoubleJump;
    private int _animIDFreeFall;
    private int _animIDMotionSpeed;

    #endregion

    #region Monobehaviour

    private void Awake()
    {
        GetReferences();
    }

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

    private void Update()
    {
        GravityAnimator();
        GroundedCheckAnimator();
        MovementAnimator();
    }

    #endregion

    #region Methods

    #region Initialization

    private void GetReferences()
    {
        _animator = GetComponent<Animator>();
        _movementController = GetComponent<PlayerMovementController>();
        _groundDetector = GetComponent<PlayerGroundDetector>();
        _playerInput = GetComponent<PlayerInputController>();
    }

    private void Initialize()
    {
        AssignAnimationIDs();
    }

    private void SubscribeToEvents()
    {
        PlayerMovementController.OnPlayerJumped += JumpAnimator;
        PlayerMovementController.OnPlayerDoubleJumped += DoubleJumpAnimator;
    }

    private void UnsubscribeToEvents()
    {
        PlayerMovementController.OnPlayerJumped += JumpAnimator;
        PlayerMovementController.OnPlayerDoubleJumped += DoubleJumpAnimator;
    }

    private void AssignAnimationIDs()
    {
        _animIDSpeed = Animator.StringToHash("Speed");
        _animIDHorizontal = Animator.StringToHash("Horizontal");
        _animIDVertical = Animator.StringToHash("Vertical");
        _animIDGrounded = Animator.StringToHash("Grounded");
        _animIDJump = Animator.StringToHash("Jump");
        _animIDFreeFall = Animator.StringToHash("FreeFall");
        _animIDMotionSpeed = Animator.StringToHash("MotionSpeed");
        _animIDDoubleJump = Animator.StringToHash("Double Jump");
    }

    #endregion

    #region Update

    private void GravityAnimator()
    {
        if (_groundDetector.isGrounded)
        {
            _fallTimeoutDelta = _fallTimeout;

            _animator.SetBool(_animIDFreeFall, false);
        }
        else
        {
            if (_fallTimeoutDelta >= 0.0f)
                _fallTimeoutDelta -= Time.deltaTime;

            if (_fallTimeoutDelta < 0.0f)
                _animator.SetBool(_animIDFreeFall, true);
        }
    }
    
    private void GroundedCheckAnimator()
    {
        _animator.SetBool(_animIDGrounded, _groundDetector.isGrounded);
    }

    private void MovementAnimator()
    {
        _animationBlend = Mathf.Lerp(_animationBlend, _movementController.TargetSpeed, Time.deltaTime * _movementController.SpeedChangeRate);

        _animator.SetFloat(_animIDSpeed, _animationBlend);
        _animator.SetFloat(_animIDMotionSpeed, _movementController.InputMagnitude());

        _animator.SetFloat(_animIDHorizontal,
            Mathf.Lerp(_animator.GetFloat(_animIDHorizontal), _playerInput.Move.x, Time.deltaTime));
        _animator.SetFloat(_animIDVertical,
            Mathf.Lerp(_animator.GetFloat(_animIDVertical), _playerInput.Move.y, Time.deltaTime));
    }

    #endregion

    #region Jump

    private void JumpAnimator()
    {
        _animator.SetTrigger(_animIDJump);
    }

    private void DoubleJumpAnimator()
    {
        _animator.SetTrigger(_animIDDoubleJump);
    }

    #endregion

    #endregion
}