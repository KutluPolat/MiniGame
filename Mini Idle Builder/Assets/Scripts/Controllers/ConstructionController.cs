using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ArvisGames.MiniIdleBuilder.Enums;

public class ConstructionController : MonoBehaviour
{
    #region Variables

    public ConstructionState CurrentConstructionState { get; private set; }
    public GameObject CurrentConstruction { get; private set; } 
    public bool IsUnderConstruction { get { return CurrentConstructionState == ConstructionState.UnderConstruction; } }


    [SerializeField]
    private GameObject _house, _hobbySpace, _rentACar, _trainStation, _marina, _historicalSite, _governorsBuilding;

    private Transform _mapTransform { get { return GameManager.Instance.GridController.MapObject.transform; } }

    #endregion // Variables

    public void OnPointerDownForHouse(int building)
    {
        if (Input.GetMouseButtonDown(0) && IsUnderConstruction == false)
        {
            SetConstructionStateTo(ConstructionState.UnderConstruction);

            switch ((BuildingType)building)
            {
                case BuildingType.House:
                    CurrentConstruction = Instantiate(_house, Input.mousePosition, Quaternion.identity, _mapTransform);
                    break;

                case BuildingType.HobbySpace:
                    CurrentConstruction = Instantiate(_hobbySpace, Input.mousePosition, Quaternion.identity, _mapTransform);
                    break;

                case BuildingType.RentACar:
                    CurrentConstruction = Instantiate(_rentACar, Input.mousePosition, Quaternion.identity, _mapTransform);
                    break;

                case BuildingType.TrainStation:
                    CurrentConstruction = Instantiate(_trainStation, Input.mousePosition, Quaternion.identity, _mapTransform);
                    break;

                case BuildingType.Marina:
                    CurrentConstruction = Instantiate(_marina, Input.mousePosition, Quaternion.identity, _mapTransform);
                    break;

                case BuildingType.HistoricalSite:
                    CurrentConstruction = Instantiate(_historicalSite, Input.mousePosition, Quaternion.identity, _mapTransform);
                    break;

                case BuildingType.GovernorsBuilding:
                    CurrentConstruction = Instantiate(_governorsBuilding, Input.mousePosition, Quaternion.identity, _mapTransform);
                    break;

                default:
                    Debug.LogError("You have to choose a building type on button.");
                    break;
            }
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
