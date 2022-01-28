using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ArvisGames.MiniIdleBuilder.Enums;
using UnityEngine.UI;

public class GridController
{
    #region Variables

    public float CellSize { get; private set; }
    public GameObject MapObject { get; private set; }

    public Grid[,] Grid;

    [Range(0, Mathf.Infinity)]
    private const int DEFAULT_WIDTH = 10, DEFAULT_HEIGHT = 10;

    #endregion // Variables

    #region Constructor

    public GridController(int width = DEFAULT_WIDTH, int height = DEFAULT_HEIGHT)
    {
        SetMapObject();
        SetGridSize(width, height);
        SetCellSize();
    }

    #endregion // Constructor

    #region Methods

    #region Is Grid Empty

    public Grid IsGridUnderSpecifiedTileEmpty(GameObject tileUnderConstruction)
    {
        Vector3 localPositionAccordingToParent = GetLocalPositionAccordingToParent(tileUnderConstruction);

        int x = Mathf.FloorToInt(localPositionAccordingToParent.x / CellSize);
        int y = Mathf.FloorToInt(localPositionAccordingToParent.y / CellSize);

        bool isAnyIndexOutsideOfBoundsOfArray = x < 0 || y < 0 || x >= Grid.GetLength(0) || y >= Grid.GetLength(1);

        if (isAnyIndexOutsideOfBoundsOfArray)
        {
            return new Grid(x, y, null);
        }
        else
        {
            return this.Grid[x,y];
        }
    }

    private Vector3 GetLocalPositionAccordingToParent(GameObject childOfDraggingObject)
    {
        return childOfDraggingObject.transform.parent.localPosition + childOfDraggingObject.transform.localPosition;
    }

    #endregion // Is Grid Empty

    #region Construction

    public void HandleGridsUnderConstruction()
    {
        SetGridStatesToOccupied(GameManager.Instance.ConstructionController.TilesUnderConstruction);
    }

    private void SetGridStatesToOccupied(List<ConstructionTile> constructedTiles)
    {
        foreach (ConstructionTile tile in constructedTiles)
        {
            Grid[tile.X, tile.Y].SetGridStateTo(GridState.Occupied);
        }
    }

    #endregion // Construction

    #region Setting Fields

    private void SetGridSize(int width, int height) => Grid = new Grid[width, height];
    
    private void SetCellSize()
    {
        RectTransform rectTransformOfMap = MapObject.GetComponent<RectTransform>();


        int possibleWidth = Mathf.FloorToInt(rectTransformOfMap.sizeDelta.x) / Grid.GetLength(0) / 10 * 10;
        int possibleHeight = Mathf.FloorToInt(rectTransformOfMap.sizeDelta.y) / Grid.GetLength(1) / 10 * 10;

        CellSize = possibleHeight < possibleWidth ? possibleHeight : possibleWidth;
    }

    private void SetMapObject() => MapObject = GameObject.FindGameObjectWithTag("Map");

    #endregion // Setting Fields

    #endregion // Methods
}
