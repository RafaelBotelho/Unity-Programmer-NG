using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputController : MonoBehaviour
{
	#region Variables

    [Header("Character Input Values")]
	private Vector2 _move;
	private Vector2 _look;
	private bool _sprint;

	[Header("Movement Settings")]
	private bool _analogMovement;

	#endregion

	#region Events
	
	public static event Action OnJumpButtonPressed;
	public static event Action OnInteractButtonPressed;
	
	#endregion
	
	#region Properties

	public Vector2 Move => _move;
	public Vector2 Look => _look;
	public bool Sprint => _sprint;
	public bool AnalogMovement => _analogMovement;

	#endregion
	
	#region Methods

	#region Messages

	public void OnMove(InputValue value)
	{
		MoveInput(value.Get<Vector2>());
	}

	public void OnLook(InputValue value)
	{
		LookInput(value.Get<Vector2>());
	}

	public void OnJump(InputValue value)
	{
		JumpInput();
	}

	public void OnSprint(InputValue value)
	{
		SprintInput(value.isPressed);
	}

	public void OnInteract(InputValue value)
	{
		InteractInput();
	}

	public void OnPauseResume(InputValue value)
	{
		PauseResumeInput();
	}
	
	#endregion

	#region Change States

	private void MoveInput(Vector2 newMoveDirection)
	{
		_move = newMoveDirection;
	}

	private void LookInput(Vector2 newLookDirection)
	{
		_look = newLookDirection;
	}

	private void JumpInput()
	{
		OnJumpButtonPressed?.Invoke();
	}

	private void SprintInput(bool newSprintState)
	{
		_sprint = newSprintState;
	}
	
	private void InteractInput()
	{
		OnInteractButtonPressed?.Invoke();
	}

	private void PauseResumeInput()
	{
		switch (GameStateManager.currentGameState)
		{
			case GameState.PAUSE:
				GameStateManager.ChangeGameState(GameState.GAMEPLAY);
				break;
			case GameState.GAMEPLAY:
				GameStateManager.ChangeGameState(GameState.PAUSE);
				break;
		}
		SetCursorState();
	}
	
	private void OnApplicationFocus(bool hasFocus)
	{
		SetCursorState();
	}

	private void SetCursorState()
	{
		Cursor.lockState = GameStateManager.currentGameState == GameState.GAMEPLAY ? CursorLockMode.Locked : CursorLockMode.None;
	}

    #endregion

    #endregion
}
