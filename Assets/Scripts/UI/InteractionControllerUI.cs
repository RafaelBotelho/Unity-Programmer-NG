using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InteractionControllerUI : MonoBehaviour
{
    #region Variables / Components

    [Header("References")]
    [SerializeField] private GameObject _interactionPanel = default;
    [SerializeField] private Image _interactionKeyImage = default;
    [SerializeField] private TextMeshProUGUI _interactionText = default;

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
        PlayerInteractionController.OnLookedToInteractable += ShowInteraction;
        PlayerInteractionController.OnLookedAwayFromInteractable += HideInteraction;
    }

    private void UnsubscribeToEvents()
    {
        PlayerInteractionController.OnLookedToInteractable -= ShowInteraction;
        PlayerInteractionController.OnLookedAwayFromInteractable -= HideInteraction;
    }

    private void ShowInteraction(IInteractable interactable)
    {
        _interactionText.text = interactable.GetDescription();
        _interactionPanel.SetActive(true);
    }

    private void HideInteraction(IInteractable interactable)
    {
        _interactionText.text = "";
        _interactionPanel.SetActive(false);
    }
    
    #endregion
}