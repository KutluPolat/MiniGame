using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ArvisGames.MiniIdleBuilder.Enums;
using UnityEngine.UI;

public class Grid
{
    public int x, y;
    public GridState GridState = GridState.Empty;
    public GameObject Tile;

    public Vector2Int Coordinates { get { return new Vector2Int(x, y); } }
    public bool IsGridEmpty { get { return Tile != null && GridState == GridState.Empty; } }

    public Grid(int widthIndex, int heightIndex, GameObject tile)
    {
        this.x = widthIndex;
        this.y = heightIndex;
        this.Tile = tile;
    }

    public void SetGridStateTo(GridState state) => this.GridState = state;
}
