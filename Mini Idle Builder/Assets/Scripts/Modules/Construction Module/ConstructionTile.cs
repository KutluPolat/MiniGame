using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ConstructionTile
{
    public Image Image { get; private set; }

    public Vector2Int IndexOfTileBelow { get; private set; }

    public int X { get { return IndexOfTileBelow.x; } }
    public int Y { get { return IndexOfTileBelow.y; } }

    public Color Red { get { return new Color(1, 0, 0, 0.5f); } }
    public Color Green { get { return new Color(0, 1, 0, 0.5f); } }

    public ConstructionTile(Image image)
    {
        Image = image;
    }

    public void SetImageColorTo(Color color) => Image.color = color;
    public void UpdateIndexOfTileBelow(Vector2Int index) => IndexOfTileBelow = index;
}
