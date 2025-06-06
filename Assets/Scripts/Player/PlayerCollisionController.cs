using UnityEngine;

public class PlayerCollisionController : MonoBehaviour
{
    #region Monobehaviour

    private void OnTriggerEnter(Collider other)
    {
        HandleTriggerEnter(other);
    }

    private void OnTriggerExit(Collider other)
    {
        HandleTriggerExit(other);
    }

    #endregion

    #region Methods

    private void HandleTriggerEnter(Collider other)
    {
        
    }

    private void HandleTriggerExit(Collider other)
    {
        
    }

    #endregion
}
