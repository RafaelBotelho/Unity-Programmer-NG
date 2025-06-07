using UnityEngine;

public class Billboard : MonoBehaviour
{
    #region Variables
    
    private Transform _camera;
    
    #endregion

    #region Monobehaviours

    private void Awake()
    {
        GetReferences();
    }

    private void Update()
    {
        UpdateBillboard();
    }

    #endregion
    
    #region Methods

    private void GetReferences()
    {
        _camera = Camera.main.transform;
    }

    private void UpdateBillboard()
    {
        transform.LookAt(_camera);
    }
    
    #endregion
}
