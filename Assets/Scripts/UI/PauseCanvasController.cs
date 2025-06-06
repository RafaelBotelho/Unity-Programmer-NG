using UnityEngine;

public class PauseCanvasController : MonoBehaviour
{
    #region Variables / Components

    [SerializeField] private bool _invertPause = false;
    [SerializeField] private GameObject _pausePanel;

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
        GameStateManager.OnGameStateChanged += ChangePanelStatus;
    }
    
    private void UnsubscribeToEvents()
    {
        GameStateManager.OnGameStateChanged -= ChangePanelStatus;
    }

    private void ChangePanelStatus(GameState newState)
    {
        switch (newState)
        {
            case GameState.PAUSE:
                _pausePanel.SetActive(!_invertPause);
                break;
            case GameState.GAMEPLAY:
                _pausePanel.SetActive(_invertPause);
                break;
        }
    }

    #endregion
}