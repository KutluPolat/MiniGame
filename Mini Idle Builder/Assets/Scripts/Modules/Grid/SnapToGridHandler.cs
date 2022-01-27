using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnapToGridHandler : MonoBehaviour
{
    private float CellSize { get { return GameManager.Instance.GridController.CellSize; } }

    private void Update()
    {
        if (transform.hasChanged)
            SnapToGrid();
    }

    private void SnapToGrid()
    {
        Vector3 snappedPosition = new Vector3(
            Mathf.Round(transform.localPosition.x / CellSize) * CellSize,
            Mathf.Round(transform.localPosition.y / CellSize) * CellSize,
            Mathf.Round(transform.localPosition.z / CellSize) * CellSize
            );

        transform.localPosition = snappedPosition;
    }
}
