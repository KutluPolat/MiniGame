using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ArvisGames.MiniIdleBuilder.Enums;

public class DragController : MonoBehaviour
{
    private BuildingController _buildingController;

    private void Start()
    {
        _buildingController = GameManager.Instance.BuildingController;
    }

    private bool IsUnderConstruction { get { return _buildingController.CurrentConstructionState == ConstructionState.UnderConstruction; } }
    private GameObject CurrentConstruction { get { return _buildingController.CurrentConstruction; } }

    private void Update()
    {
        if (IsUnderConstruction)
        {
            MoveCurrentConstructionToMousePosition();
        }
    }

    private void MoveCurrentConstructionToMousePosition()
    {
        CurrentConstruction.transform.position = Input.mousePosition;
    }
}
