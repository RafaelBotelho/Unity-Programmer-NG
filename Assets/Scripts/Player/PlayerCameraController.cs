using UnityEngine;

public class PlayerCameraController : MonoBehaviour
{
    #region Variables

    [Header("References")]
    [SerializeField] private GameObject _cinemachineCameraTarget;
    
    [Header("Settings")]
    [SerializeField] private float _topClamp;
    [SerializeField] private float _bottomClamp;
    [SerializeField] private float _cameraAngleOverride;
    [SerializeField] private bool _lockCameraPosition;
    [SerializeField] private float _sensibility;

    private float _cinemachineTargetYaw;
    private float _cinemachineTargetPitch;
    private PlayerInputController _inputController;
    
    private const float _threshold = 0.01f;

    #endregion

    #region Monobehaviour

    private void Awake()
    {
        GetReferences();
    }

    private void LateUpdate()
    {
        CameraRotation();
    }

    #endregion

    #region Methods

    #region Initialization

    private void GetReferences()
    {
        _inputController = GetComponent<PlayerInputController>();
    }

    #endregion

    #region Update

    private void CameraRotation()
    {
        if(GameStateManager.currentGameState != GameState.GAMEPLAY) return;

        if (_inputController.Look.sqrMagnitude >= _threshold && !_lockCameraPosition)
        {
            _cinemachineTargetYaw += _inputController.Look.x * Time.deltaTime * _sensibility;
            _cinemachineTargetPitch += _inputController.Look.y * Time.deltaTime * _sensibility;
        }

        _cinemachineTargetYaw = ClampAngle(_cinemachineTargetYaw, float.MinValue, float.MaxValue);
        _cinemachineTargetPitch = ClampAngle(_cinemachineTargetPitch, _bottomClamp, _topClamp);

        _cinemachineCameraTarget.transform.rotation = Quaternion.Euler(_cinemachineTargetPitch + _cameraAngleOverride, _cinemachineTargetYaw, 0.0f);
    }

    private float ClampAngle(float lfAngle, float lfMin, float lfMax)
    {
        if (lfAngle < -360f) lfAngle += 360f;
        if (lfAngle > 360f) lfAngle -= 360f;
        return Mathf.Clamp(lfAngle, lfMin, lfMax);
    }

    #endregion
    
    #endregion
}
