using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GridBuildingController : MonoBehaviour
{
    #region Variables

    [SerializeField]
    private GameObject _emptyTilePrefab;

    private GridController gridController;

    #endregion // Variables

    #region Methods

    private void CreateMap()
    {
        gridController = GameManager.Instance.GridController;

        for (int x= 0; x < gridController.Grid.GetLength(0); x++)
        {
            for(int y = 0; y < gridController.Grid.GetLength(1); y++)
            {
                CreateGrid(x, y, InstantiateTile(x, y));
            }
        }
    }

    private void CreateGrid(int x, int y, GameObject tile) => gridController.Grid[x, y] = new Grid(x, y, tile);

    private GameObject InstantiateTile(int x, int y)
    {
        Vector3 targetPosition = new Vector3(x, y) * gridController.CellSize;
        Transform parent = gridController.MapObject.transform;

        GameObject tile = Instantiate(_emptyTilePrefab, parent);

        RectTransform rectTransform = tile.GetComponent<RectTransform>();
        rectTransform.anchorMin = Vector2.zero;
        rectTransform.anchorMax = Vector2.zero;
        rectTransform.pivot = Vector2.zero;

        tile.transform.localPosition = targetPosition;

        return tile;
    }

    #region Events

    public void SubscribeEvents()
    {
        EventManager.Instance.MapTilesCreated += CreateMap;
    }

    private void UnsubscribeEvents()
    {
        EventManager.Instance.MapTilesCreated -= CreateMap;
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
