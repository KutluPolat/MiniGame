using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ResourceTextsTracker : MonoBehaviour
{
    #region Variables

    public TextMeshProUGUI GoldUGUI, GemUGUI;

    #endregion // Variables

    #region Start

    private void Start()
    {
        UpdateTexts();
    }

    #endregion // Start

    #region Methods

    private void UpdateTexts()
    {
        GoldUGUI.text = GameManager.Instance.Resource.Gold.ToString();
        GemUGUI.text = GameManager.Instance.Resource.Gem.ToString();
    }

    #region Events

    public void SubscribeEvents()
    {
        EventManager.Instance.ResourceChanged += UpdateTexts;
    }

    private void UnsubscribeEvents()
    {
        EventManager.Instance.ResourceChanged -= UpdateTexts;
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
