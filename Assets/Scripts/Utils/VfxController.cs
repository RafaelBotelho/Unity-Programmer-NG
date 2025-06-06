using System.Collections.Generic;
using UnityEngine;

public class VfxController : MonoBehaviour
{
    #region Variables / Components

    [SerializeField] private int _poolSize;
    [SerializeField] private GameObject _Vfx;

    private List<GameObject> _pool = new List<GameObject>();

    #endregion
    
    #region MonoBehaviours

    private void Start()
    {
        InitializePool();
    }

    #endregion
    
    #region Methods

    private void InitializePool()
    {
        for (int i = 0; i < _poolSize; i++)
            _pool.Add(Instantiate(_Vfx, transform.position, transform.rotation));
    }
    
    public void SpawnVfx()
    {
        foreach (var poolObject in _pool)
        {
            if (poolObject.activeInHierarchy) continue;
            
            poolObject.transform.position = transform.position;
            poolObject.transform.rotation = transform.rotation;
            poolObject.SetActive(true);
            
            return;
        }
        
        var newVfx = Instantiate(_Vfx, transform.position, transform.rotation);

        newVfx.SetActive(true);
        _pool.Add(newVfx);
    }

    #endregion
}
