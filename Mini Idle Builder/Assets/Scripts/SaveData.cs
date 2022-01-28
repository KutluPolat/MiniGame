using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ArvisGames.MiniIdleBuilder.Enums;

public class SaveData
{
    public List<Vector2Int> OccupiedGrids;
    public List<Vector3> ConstructedBuilding_Positions;
    public List<Building> ConstructedBuilding_BuildingDatas;
    public List<float> ConstructedBuilding_CurrentTimerValues;

    public SaveData()
    {
        OccupiedGrids = new List<Vector2Int>();

        ConstructedBuilding_Positions = new List<Vector3>();
        ConstructedBuilding_BuildingDatas = new List<Building>();
        ConstructedBuilding_CurrentTimerValues = new List<float>();
    }
}