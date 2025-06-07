using System.Collections.Generic;
using UnityEngine;

public class PlayerVisualController : MonoBehaviour
{
    #region Variables

    [Header("Visuals")]
    [SerializeField] private List<GameObject> _belts = new List<GameObject>();
    [SerializeField] private List<GameObject> _bodyCloths = new List<GameObject>();
    [SerializeField] private List<GameObject> _gloves = new List<GameObject>();
    [SerializeField] private List<GameObject> _hairs = new List<GameObject>();
    [SerializeField] private List<GameObject> _hats = new List<GameObject>();
    [SerializeField] private List<GameObject> _shoes = new List<GameObject>();

    [Header("Weapons")]
    [SerializeField] private List<GameObject> _leftHandWeapons = new List<GameObject>();
    [SerializeField] private List<GameObject> _rightHandWeapons = new List<GameObject>();

    [Header("Default Visual")]
    [SerializeField] private SO_PlayerVisual _playerVisual = default;

    #endregion

    #region Monobehaviour

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

    #endregion

    #region Methods

    #region Setup

    private void Initialize()
    {
        UpdateHair();
    }

    private void SubscribeToEvents()
    {
        PlayerEquipmentController.OnPlayerEquippedEquipment += CheckNewEquipmentVisual;
        PlayerEquipmentController.OnPlayerRemovedEquipment += CheckRemovedEquipmentVisual;
        PlayerEquipmentController.OnPlayerEquippedWeapon += UpdateWeapon;
    }

    private void UnsubscribeToEvents()
    {
        PlayerEquipmentController.OnPlayerEquippedEquipment -= CheckNewEquipmentVisual;
        PlayerEquipmentController.OnPlayerRemovedEquipment -= CheckRemovedEquipmentVisual;
        PlayerEquipmentController.OnPlayerEquippedWeapon -= UpdateWeapon;
    }

    #endregion

    private void CheckNewEquipmentVisual(SO_EquipableItem item)
    {
        switch (item.EquipmentType)
        {
            case EquipmentType.BELT:
                UpdateBelt(item.VisualIndex - 1);
                break;
            case EquipmentType.BODYCLOTH:
                UpdateBodyCloth(item.VisualIndex - 1);
                break;
            case EquipmentType.GLOVE:
                UpdateGloves(item.VisualIndex - 1);
                break;
            case EquipmentType.HAT:
                UpdateHat(item.VisualIndex - 1);
                DisableVisuals(_hairs);
                break;
            case EquipmentType.SHOE:
                UpdateShoes(item.VisualIndex - 1);
                break;
        }
    }

    private void CheckRemovedEquipmentVisual(EquipmentType equipmentType)
    {
        switch (equipmentType)
        {
            case EquipmentType.BELT:
                DisableVisuals(_belts);
                break;
            case EquipmentType.BODYCLOTH:
                DisableVisuals(_bodyCloths);
                break;
            case EquipmentType.GLOVE:
                DisableVisuals(_gloves);
                break;
            case EquipmentType.HAT:
                DisableVisuals(_hats);
                UpdateHair();
                break;
            case EquipmentType.SHOE:
                DisableVisuals(_shoes);
                break;
            case EquipmentType.WEAPON:
                DisableVisuals(_leftHandWeapons);
                DisableVisuals(_rightHandWeapons);
                break;
        }
    }

    private void UpdateHair()
    {
        DisableVisuals(_hairs);
        _hairs[_playerVisual.HairIndex - 1].SetActive(true);
    }

    private void UpdateBelt(int index)
    {
        DisableVisuals(_belts);

        _belts[index].SetActive(true);
    }

    private void UpdateBodyCloth(int index)
    {
        DisableVisuals(_bodyCloths);

        _bodyCloths[index].SetActive(true);
    }
    
    private void UpdateGloves(int index)
    {
        DisableVisuals(_gloves);

        _gloves[index].SetActive(true);
    }

    private void UpdateHat(int index)
    {
        DisableVisuals(_hats);

        _hats[index].SetActive(true);
    }

    private void UpdateShoes(int index)
    {
        DisableVisuals(_shoes);

        _shoes[index].SetActive(true);
    }
    
    private void UpdateWeapon(SO_WeaponItem weapon)
    {
        DisableVisuals(_leftHandWeapons);
        DisableVisuals(_rightHandWeapons);

        if (weapon.LeftHandIndex >= 1)
            _leftHandWeapons[weapon.LeftHandIndex - 1].SetActive(true);
        
        if (weapon.RightHandIndex >= 1)
            _rightHandWeapons[weapon.RightHandIndex - 1].SetActive(true);
    }

    private void DisableVisuals(List<GameObject> visuals)
    {
        foreach (var visual in visuals)
            visual.SetActive(false);
    }

    #endregion
}
