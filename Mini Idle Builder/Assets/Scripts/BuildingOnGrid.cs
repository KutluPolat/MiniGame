using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BuildingOnGrid : MonoBehaviour
{
    public Building Building;

    [HideInInspector]
    public float Timer;

    private TextMeshProUGUI _timerCounter;
    private Slider _progressBar;

    private bool IsProductionCompleted { get { return Timer >= Building.ProductionTime; } }
    private float Countdown { get { return Building.ProductionTime - Timer; } }

    private void OnEnable()
    {
        if(Building == null)
        {
            InitializeBuilding(GameManager.Instance.ConstructionController.CurrentConstruction.Building);
        }
    }

    private void Start()
    {
        InitializeProgressBar();
        SubscribeEvents();
    }


    private void Update()
    {
        HandleTimer();
        HandleProgressBar();
    }

    #region Resource Controls

    private void HandleTimer()
    {
        Timer += Time.deltaTime;

        if(IsProductionCompleted)
        {
            ProduceResource();
            Timer = 0;
        }

        _timerCounter.text = string.Format("{0:0.0}", Countdown);
    }

    private void HandleProgressBar()
    {
        _progressBar.value = Timer;
    }

    private void ProduceResource()
    {
        GameManager.Instance.Resource.GainResource(Building.GoldProduction, Building.GemProduction);
        GameManager.Instance.FloatingTextController.OnProductionFeedback(gameObject);
    }

    #endregion // Resource Controls

    #region Timer Control

    private void TransferTimerValueToBuildingType()
    {
        Building.SavedTimerValue = Timer;
    }

    #endregion // Timer Control

    #region Initialization

    private void InitializeBuilding(Building building) => Building = building;

    private void InitializeProgressBar()
    {
        _progressBar = GetComponentInChildren<Slider>();
        _progressBar.maxValue = Building.ProductionTime;
        MakeProgressBarVisible();

        _timerCounter = GetComponentInChildren<TextMeshProUGUI>();
    }

    private void MakeProgressBarVisible()
    {
        foreach (Image image in _progressBar.GetComponentsInChildren<Image>())
            image.enabled = true;
    }

    #endregion // Initialization

    #region Events

    private void SubscribeEvents()
    {
        EventManager.Instance.Saved += TransferTimerValueToBuildingType;
    }

    private void UnsubscribeEvents()
    {
        EventManager.Instance.Saved -= TransferTimerValueToBuildingType;
    }

    #endregion // Events

    #region OnDestroy

    private void OnDestroy()
    {
        UnsubscribeEvents();
    }

    #endregion // OnDestroy
}
