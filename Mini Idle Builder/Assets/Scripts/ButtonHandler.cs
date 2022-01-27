using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonHandler : MonoBehaviour
{
    [SerializeField]
    private Building _building;

    private float _timer;
    private Button _button;

    private void Start()
    {
        _button = GetComponent<Button>();
    }

    private void DisableOrEnableButtonAccordingToResources()
    {
        if (GameManager.Instance.Resource.IsResourcesEnoughFor(_building.GoldCost, _building.GemCost))
        {
            _button.enabled = true;
        }
        else
        {
            _button.enabled = false;
        }
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
