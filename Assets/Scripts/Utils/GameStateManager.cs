using System;

public static class GameStateManager
{
    #region Variables / Components

    private static GameState _currentSate = GameState.GAMEPLAY;

    #endregion

    #region Properties

    public static GameState currentGameState => _currentSate;

    #endregion

    #region Events

    public static event Action<GameState> OnGameStateChanged;

    #endregion
    
    #region Methods

    public static void ChangeGameState(GameState newState)
    {
        _currentSate = newState;
        
        OnGameStateChanged?.Invoke(newState);
    }

    #endregion
}