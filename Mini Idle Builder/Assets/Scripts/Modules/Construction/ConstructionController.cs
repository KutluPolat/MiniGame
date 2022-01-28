using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ArvisGames.MiniIdleBuilder.Enums;

public class ConstructionController : MonoBehaviour
{
    #region Variables
    public GameObject CurrentConstruction { get; private set; } 
    public bool IsUnderConstruction { get { return _currentConstructionState == ConstructionState.UnderConstruction; } }

    private List<ConstructionTile> _tilesUnderConstruction;
    private ConstructionState _currentConstructionState;

    private bool IsAllChildTilesAvailableToConstruction
    {
        get
        {
            for (int i = 0; i < _tilesUnderConstruction.Count; i++)
            {
                Vector2Int coordinates = _tilesUnderConstruction[i].IndexOfTileBelow;

                int xLengthOfGrid = GameManager.Instance.GridController.Grid.GetLength(0);
                int yLengthOfGrid = GameManager.Instance.GridController.Grid.GetLength(1);

                bool isCoordinatesOutsideOfGrid = coordinates.x < 0 || coordinates.y < 0 || coordinates.x >= xLengthOfGrid || coordinates.y >= yLengthOfGrid;

                if (isCoordinatesOutsideOfGrid)
                    return false;

                if (GameManager.Instance.GridController.Grid[coordinates.x, coordinates.y].IsGridEmpty)
                {
                    // Do nothing
                }
                else
                {
                    return false;
                }
            }

            return true;
        }
    }

    [SerializeField]
    private GameObject _house, _hobbySpace, _rentACar, _trainStation, _marina, _historicalSite, _governorsBuilding;

    private Transform _mapTransform { get { return GameManager.Instance.GridController.MapObject.transform; } }

    #endregion // Variables

    #region Methods

    #region OnPointerDown

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

            InitializeConstructionTiles();
        }
    }

    #endregion // OnPointerDown

    private void ConcludeConstruction()
    {
        if (IsUnderConstruction && IsAllChildTilesAvailableToConstruction)
        {
            Debug.Log("Construction Sucessfull");
            ConstructionSuccessful();
        }
        else
        {
            Debug.Log("Construction failed");
            ConstructionFailed();
        }
    }

    private void ConstructionSuccessful()
    {
        GameManager.Instance.GridController.SetGridStatesToOccupied(_tilesUnderConstruction);
        ResetChildTiles();
        ReleaseCurrentConstruction();
    }

    private void ConstructionFailed()
    {
        DestroyCurrentConstruction();

    }

    #region Construction Visual Controls

    // Destroys the building under consturction
    private void DestroyCurrentConstruction() => Destroy(CurrentConstruction);


    // Releases the building under construction to it's place
    private void ReleaseCurrentConstruction() => CurrentConstruction = null;

    private void ResetChildTiles()
    {
        for(int i= 0; i< CurrentConstruction.transform.childCount; i++)
        {
            ConstructionTileHandler scriptAttachedToConstructedTile = CurrentConstruction.transform.GetChild(i).GetComponent<ConstructionTileHandler>();
            ConstructionTile constructedTile = scriptAttachedToConstructedTile.ConstructionTile;
            Color defaultColorOfTile = constructedTile.DefaultColor;

            constructedTile.SetImageColorTo(defaultColorOfTile);
            Destroy(scriptAttachedToConstructedTile);
        }
    }

    #endregion // Construction Visual Controls

    #region ConstructionState Controls

    private void SetConstructionStateTo(ConstructionState state) => _currentConstructionState = state;

    #endregion // ConstructionState Controls

    #region Tile Controls

    private void InitializeConstructionTiles()
    {
        _tilesUnderConstruction = new List<ConstructionTile>();

        foreach (ConstructionTileHandler childTile in CurrentConstruction.GetComponentsInChildren<ConstructionTileHandler>())
        {
            _tilesUnderConstruction.Add(childTile.ConstructionTile);
        }
    }

    #endregion // Tile Controls

    #region Events

    public void SubscribeEvents()
    {
        EventManager.Instance.LeftMouseButtonReleased += ConcludeConstruction;
        EventManager.Instance.LeftMouseButtonReleased += () => SetConstructionStateTo(ConstructionState.Null);
    }

    private void UnsubscribeEvents()
    {
        EventManager.Instance.LeftMouseButtonReleased -= ConcludeConstruction;
        EventManager.Instance.LeftMouseButtonReleased -= () => SetConstructionStateTo(ConstructionState.Null);
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
