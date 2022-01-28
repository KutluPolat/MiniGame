using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ArvisGames.MiniIdleBuilder.Enums;

public class DragController : MonoBehaviour
{
    private ConstructionController _buildingController;

    private void Start()
    {
        _buildingController = GameManager.Instance.ConstructionController;
    }
    private GameObject CurrentConstruction { get { return _buildingController.CurrentConstructionShape; } }

    private void Update()
    {
        if (_buildingController.IsUnderConstruction)
        {
            MoveCurrentConstructionToMousePosition();
        }
    }

    private void MoveCurrentConstructionToMousePosition()
    {
        CurrentConstruction.transform.position = Input.mousePosition;
    }
}
