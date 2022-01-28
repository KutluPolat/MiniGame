using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BuildingOnGrid : MonoBehaviour
{
    private Building _building;
    private float _timer;
    private TextMeshProUGUI _timerCounter;
    private Slider _progressBar;

    private bool IsProductionCompleted { get { return _timer >= _building.ProductionTime; } }
    private float Countdown { get { return _building.ProductionTime - _timer; } }


    private void Awake()
    {
        InitializeBuilding(GameManager.Instance.ConstructionController.CurrentConstruction.Building);
    }

    private void Start()
    {
        InitializeProgressBar();
    }


    private void Update()
    {
        HandleTimer();
        HandleProgressBar();
    }

    #region Resource Controls

    private void HandleTimer()
    {
        _timer += Time.deltaTime;

        if(IsProductionCompleted)
        {
            ProduceResource();
            _timer = 0;
        }

        _timerCounter.text = string.Format("{0:0.0}", Countdown);
    }

    private void HandleProgressBar()
    {
        _progressBar.value = _timer;
    }

    private void ProduceResource()
    {
        GameManager.Instance.Resource.GainResource(_building.GoldProduction, _building.GemProduction);
    }

    #endregion // Resource Controls

    #region Initialization

    private void InitializeBuilding(Building building) => _building = building;

    private void InitializeProgressBar()
    {
        _progressBar = GetComponentInChildren<Slider>();
        _progressBar.maxValue = _building.ProductionTime;
        MakeProgressBarVisible();

        _timerCounter = GetComponentInChildren<TextMeshProUGUI>();
    }

    private void MakeProgressBarVisible()
    {
        foreach (Image image in _progressBar.GetComponentsInChildren<Image>())
            image.enabled = true;
    }

    #endregion // Initialization
}
