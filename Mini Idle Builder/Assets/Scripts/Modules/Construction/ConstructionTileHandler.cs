using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ConstructionTileHandler : MonoBehaviour
{
    private GridController _gridController;
    public ConstructionTile ConstructionTile { get; private set; }

    private void Awake()
    {
        ConstructionTile = new ConstructionTile(GetComponent<Image>());
    }

    private void Start()
    {
        _gridController = GameManager.Instance.GridController;
    }

    void Update()
    {
        if (transform.hasChanged)
        {
            HandleTile();
        }
    }

    public ConstructionTile GetTileProperties()
    {
        return ConstructionTile;
    }

    // This method and GridController.IsGridUnderSpecifiedTileEmpty is not optimized yet.
    private void HandleTile()
    {
        Grid gridObject = _gridController.IsGridUnderSpecifiedTileEmpty(gameObject);

        ConstructionTile.UpdateIndexOfTileBelow(gridObject.Coordinates);

        if (gridObject.IsGridEmpty)
        {
            ConstructionTile.SetImageColorTo(ConstructionTile.Green);
        }
        else
        {
            ConstructionTile.SetImageColorTo(ConstructionTile.Red);
        }
    }
}
