using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PanelTextsTracker : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI _gold, _gem;

    private void Start()
    {
        UpdateTexts();
    }

    private void UpdateTexts()
    {
        _gold.text = GameManager.Instance.Resource.Gold.ToString();
        _gem.text = GameManager.Instance.Resource.Gem.ToString();
    }

    public void SubscribeEvents()
    {
        EventManager.Instance.ResourceChanged += UpdateTexts;
    }

    private void UnsubscribeEvents()
    {
        EventManager.Instance.ResourceChanged -= UpdateTexts;
    }

    private void OnDestroy()
    {
        UnsubscribeEvents();
    }
}
