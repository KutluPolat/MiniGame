using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Sirenix.OdinInspector;
using UnityEngine.EventSystems;

public class ButtonHandler : MonoBehaviour
{
    [SerializeField]
    private Building _building;

    private Button _button;
    private EventTrigger _eventTrigger;

    [BoxGroup("Texts"), SerializeField]
    private TextMeshProUGUI _goldCost, _goldProduction, _gemCost, _gemProduction, _productionTime, _name;

    private void Start()
    {
        _button = GetComponent<Button>();
        _eventTrigger = GetComponent<EventTrigger>();
        SetTexts();
        DisableOrEnableButtonAccordingToResources();
    }

    private void DisableOrEnableButtonAccordingToResources()
    {
        if (GameManager.Instance.Resource.IsResourcesEnoughFor(_building.GoldCost, _building.GemCost))
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

    private void SetTexts()
    {
        _goldCost.text = _building.GoldCost.ToString();
        _gemCost.text = _building.GemCost.ToString();

        _goldProduction.text = _building.GoldProduction.ToString();
        _gemProduction.text = _building.GemProduction.ToString();

        _productionTime.text = _building.ProductionTime.ToString() + "s";

        _name.text = _building.BuildingName;
    }

    public void SubscribeEvents()
    {
        EventManager.Instance.ResourceChanged += DisableOrEnableButtonAccordingToResources;
    }

    private void UnsubscribeEvents()
    {
        EventManager.Instance.ResourceChanged -= DisableOrEnableButtonAccordingToResources;
    }

    private void OnDestroy()
    {
        UnsubscribeEvents();
    }
}
