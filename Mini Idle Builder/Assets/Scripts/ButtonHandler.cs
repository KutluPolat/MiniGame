using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Sirenix.OdinInspector;
using UnityEngine.EventSystems;

public class ButtonHandler : MonoBehaviour
{
    #region Variables

    public Building Building;

    [SerializeField]
    private Image _buildingIcon;

    private Button _button;
    private EventTrigger _eventTrigger;

    [BoxGroup("Texts"), SerializeField]
    private TextMeshProUGUI _goldCost, _goldProduction, _gemCost, _gemProduction, _productionTime, _name;

    #endregion // Variables

    #region Start

    private void Start()
    {
        _button = GetComponent<Button>();
        _eventTrigger = GetComponent<EventTrigger>();
        InitializeCard();
        DisableOrEnableButtonAccordingToResources();
    }

    #endregion // Start

    #region Methods

    private void DisableOrEnableButtonAccordingToResources()
    {
        if (GameManager.Instance.Resource.IsResourcesEnoughFor(Building.GoldCost, Building.GemCost))
        {
            _button.interactable = true;
            _eventTrigger.enabled = true;
        }
        else
        {
            _button.interactable = false;
            _eventTrigger.enabled = false;
        }
    }

    private void InitializeCard()
    {
        _goldCost.text = Building.GoldCost.ToString();
        _gemCost.text = Building.GemCost.ToString();

        _goldProduction.text = Building.GoldProduction.ToString();
        _gemProduction.text = Building.GemProduction.ToString();

        _productionTime.text = Building.ProductionTime.ToString() + "s";

        _name.text = Building.BuildingName;

        _buildingIcon.sprite = Building.BuildingIcon;
    }

    #region Events

    public void SubscribeEvents()
    {
        EventManager.Instance.ResourceChanged += DisableOrEnableButtonAccordingToResources;
    }

    private void UnsubscribeEvents()
    {
        EventManager.Instance.ResourceChanged -= DisableOrEnableButtonAccordingToResources;
    }

    #endregion // Events

    #region OnDestroy

    private void OnDestroy()
    {
        UnsubscribeEvents();
    }

    #endregion // OnDestroy

    #endregion // Methods
}
