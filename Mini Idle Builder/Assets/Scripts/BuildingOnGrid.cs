using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BuildingOnGrid : MonoBehaviour
{
    #region Variables

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

    #endregion // Variables

    #region Start & Updates

    private void Start()
    {
        InitializeProgressBar();
    }


    private void Update()
    {
        HandleTimer();
        HandleProgressBar();
    }

    #endregion // Start & Updates

    #region Methods

    #region Handlers

    private void HandleTimer()
    {
        Timer += Time.deltaTime;

        if (IsProductionCompleted)
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

    #endregion // Handlers

    #region Initializations

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

    #endregion // Initializations

    #endregion // Methods
}
