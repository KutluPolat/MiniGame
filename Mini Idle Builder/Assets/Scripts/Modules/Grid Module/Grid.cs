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

    public Grid(int widthIndex, int heightIndex, GameObject tile)
    {
        this.x = widthIndex;
        this.y = heightIndex;
        this.Tile = tile;
    }
}
