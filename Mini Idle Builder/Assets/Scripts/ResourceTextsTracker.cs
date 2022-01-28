using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ResourceTextsTracker : MonoBehaviour
{
    public TextMeshProUGUI GoldUGUI, GemUGUI;

    private void Start()
    {
        UpdateTexts();
    }

    private void UpdateTexts()
    {
        GoldUGUI.text = GameManager.Instance.Resource.Gold.ToString();
        GemUGUI.text = GameManager.Instance.Resource.Gem.ToString();
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
