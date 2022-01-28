using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ArvisGames.MiniIdleBuilder.Enums;

public class ConstructionController : MonoBehaviour
{
    #region Variables
    public GameObject CurrentConstructionShape { get; private set; }
    public ButtonHandler CurrentConstruction { get; private set; }
    public bool IsUnderConstruction { get { return _currentConstructionState == ConstructionState.UnderConstruction; } }
    public List<ConstructionTile> TilesUnderConstruction { get; private set; }

    private ConstructionState _currentConstructionState;

    private bool IsAllChildTilesAvailableToConstruction
    {
        get
        {
            for (int i = 0; i < TilesUnderConstruction.Count; i++)
            {
                Vector2Int coordinates = TilesUnderConstruction[i].IndexOfTileBelow;

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
    private ButtonHandler _houseCard, _hobbySpaceCard, _rentACarCard, _trainStationCard, _marinaCard, _historicalSiteCard, _governorsBuildingCard;

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
                    CurrentConstructionShape = Instantiate(_houseCard.Building.BuildingShape, Input.mousePosition, Quaternion.identity, _mapTransform);
                    CurrentConstruction = _houseCard;
                    break;

                case BuildingType.HobbySpace:
                    CurrentConstructionShape = Instantiate(_hobbySpaceCard.Building.BuildingShape, Input.mousePosition, Quaternion.identity, _mapTransform);
                    CurrentConstruction = _hobbySpaceCard;
                    break;

                case BuildingType.RentACar:
                    CurrentConstructionShape = Instantiate(_rentACarCard.Building.BuildingShape, Input.mousePosition, Quaternion.identity, _mapTransform);
                    CurrentConstruction = _rentACarCard;
                    break;

                case BuildingType.TrainStation:
                    CurrentConstructionShape = Instantiate(_trainStationCard.Building.BuildingShape, Input.mousePosition, Quaternion.identity, _mapTransform);
                    CurrentConstruction = _trainStationCard;
                    break;

                case BuildingType.Marina:
                    CurrentConstructionShape = Instantiate(_marinaCard.Building.BuildingShape, Input.mousePosition, Quaternion.identity, _mapTransform);
                    CurrentConstruction = _marinaCard;
                    break;

                case BuildingType.HistoricalSite:
                    CurrentConstructionShape = Instantiate(_historicalSiteCard.Building.BuildingShape, Input.mousePosition, Quaternion.identity, _mapTransform);
                    CurrentConstruction = _historicalSiteCard;
                    break;

                case BuildingType.GovernorsBuilding:
                    CurrentConstructionShape = Instantiate(_governorsBuildingCard.Building.BuildingShape, Input.mousePosition, Quaternion.identity, _mapTransform);
                    CurrentConstruction = _governorsBuildingCard;
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

            EventManager.Instance.OnConstructionStarted();
            EventManager.Instance.OnConstructionOnGoing(CurrentConstruction.Building);
            EventManager.Instance.OnConstructionCompleted();
        }
        else
        {
            Debug.Log("Construction failed");
            ConstructionFailed();
        }
    }

    private void ConstructionFailed()
    {
        DestroyCurrentConstruction();

    }

    private void StartProductionOnConstructedBuilding()
    {
        CurrentConstructionShape.AddComponent<BuildingOnGrid>();
    }

    #region Construction Visual Controls

    // Destroys the building under consturction
    private void DestroyCurrentConstruction() => Destroy(CurrentConstructionShape);


    // Releases the building under construction to it's place
    private void ReleaseCurrentConstruction() => CurrentConstructionShape = null;

    private void ResetChildTiles()
    {
        foreach(ConstructionTileHandler constructionTileHandler in CurrentConstructionShape.transform.GetComponentsInChildren<ConstructionTileHandler>())
        {
            ConstructionTile constructedTile = constructionTileHandler.ConstructionTile;
            Color defaultColorOfTile = constructedTile.DefaultColor;

            constructedTile.SetImageColorTo(defaultColorOfTile);
            Destroy(constructionTileHandler);
        }

        //for(int i= 0; i< CurrentConstructionShape.transform.childCount; i++)
        //{
        //    ConstructionTileHandler scriptAttachedToConstructedTile = CurrentConstructionShape.transform.GetChild(i).GetComponent<ConstructionTileHandler>();
        //    ConstructionTile constructedTile = scriptAttachedToConstructedTile.ConstructionTile;
        //    Color defaultColorOfTile = constructedTile.DefaultColor;

        //    constructedTile.SetImageColorTo(defaultColorOfTile);
        //    Destroy(scriptAttachedToConstructedTile);
        //}
    }

    #endregion // Construction Visual Controls

    #region ConstructionState Controls

    private void SetConstructionStateTo(ConstructionState state) => _currentConstructionState = state;

    #endregion // ConstructionState Controls

    #region Tile Controls

    private void InitializeConstructionTiles()
    {
        TilesUnderConstruction = new List<ConstructionTile>();

        foreach (ConstructionTileHandler childTile in CurrentConstructionShape.GetComponentsInChildren<ConstructionTileHandler>())
        {
            TilesUnderConstruction.Add(childTile.ConstructionTile);
        }
    }

    #endregion // Tile Controls

    #region Events

    public void SubscribeEvents()
    {
        EventManager.Instance.LeftMouseButtonReleased += ConcludeConstruction;
        EventManager.Instance.LeftMouseButtonReleased += () => SetConstructionStateTo(ConstructionState.Null);

        EventManager.Instance.ConstructionStarted += ResetChildTiles;
        EventManager.Instance.ConstructionStarted += StartProductionOnConstructedBuilding;

        EventManager.Instance.ConstructionCompleted += ReleaseCurrentConstruction;
    }

    private void UnsubscribeEvents()
    {
        EventManager.Instance.LeftMouseButtonReleased -= ConcludeConstruction;
        EventManager.Instance.LeftMouseButtonReleased -= () => SetConstructionStateTo(ConstructionState.Null);

        EventManager.Instance.ConstructionStarted -= ResetChildTiles;
        EventManager.Instance.ConstructionStarted -= StartProductionOnConstructedBuilding;

        EventManager.Instance.ConstructionCompleted -= ReleaseCurrentConstruction;
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
