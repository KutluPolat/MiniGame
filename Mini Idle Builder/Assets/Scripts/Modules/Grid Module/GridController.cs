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

    public bool IsGridEmpty(int x, int y)
    {
        return Grid[x, y].GridState == GridState.Empty;
    }

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
