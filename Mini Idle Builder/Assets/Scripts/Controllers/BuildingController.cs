using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ArvisGames.MiniIdleBuilder.Enums;

public class BuildingController : MonoBehaviour
{
    #region Variables

    public GameObject CurrentConstruction { get; private set; } 
    public ConstructionState CurrentConstructionState { get; private set; }


    [SerializeField]
    private GameObject _building1;

    private Transform _mapTransform { get { return GameManager.Instance.GridController.MapObject.transform; } }

    #endregion // Variables

    public void OnPointerDown()
    {
        if (Input.GetMouseButtonDown(0))
        {
            SetConstructionStateTo(ConstructionState.UnderConstruction);
            CurrentConstruction = Instantiate(_building1, Input.mousePosition, Quaternion.identity, _mapTransform);
        }
    }

    private void ClearConstructionOnBuilding()
    {
        Destroy(CurrentConstruction);
    }

    private void SetConstructionStateTo(ConstructionState state) => CurrentConstructionState = state;

    #region Events

    public void SubscribeEvents()
    {
        EventManager.Instance.LeftMouseButtonReleased += () => SetConstructionStateTo(ConstructionState.Null);
        EventManager.Instance.LeftMouseButtonReleased += ClearConstructionOnBuilding;
    }

    private void UnsubscribeEvents()
    {
        EventManager.Instance.LeftMouseButtonReleased -= () => SetConstructionStateTo(ConstructionState.Null);
        EventManager.Instance.LeftMouseButtonReleased -= ClearConstructionOnBuilding;
    }

    #endregion // Events

    #region OnDestroy

    private void OnDestroy()
    {
        UnsubscribeEvents();
    }

    #endregion // OnDestroy
}
