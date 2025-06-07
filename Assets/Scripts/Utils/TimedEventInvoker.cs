using System;
using UnityEngine;
using UnityEngine.Events;

public class TimedEventInvoker : MonoBehaviour
{
    #region Variables

    [SerializeField] private float _delay;

    #endregion

    #region Events

    public UnityEvent OnEvent;

    #endregion

    #region Monobehaviour

    private void OnEnable()
    {
        Invoke(nameof(FireEvent), _delay);
    }

    private void OnDisable()
    {
        CancelInvoke();
    }

    #endregion

    #region Methods

    private void FireEvent()
    {
        OnEvent?.Invoke();
    }

    #endregion
}
