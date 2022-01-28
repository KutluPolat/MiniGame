using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using ArvisGames.MiniIdleBuilder.Enums;
using UnityEngine.SceneManagement;

public abstract class SaveSystem : MonoBehaviour
{
    #region Variables

    public static int Gold
    {
        get { return PlayerPrefs.GetInt("Gold", _defaultGoldValue); }
        set { PlayerPrefs.SetInt("Gold", value); }
    }

    public static int Gem
    {
        get { return PlayerPrefs.GetInt("Gem", _defaultGemValue); }
        set { PlayerPrefs.SetInt("Gem", value); }
    }

    public static SaveData SavedDatas;

    private static int _defaultGoldValue = 10, _defaultGemValue = 10;
    private static string _savePath { get { return Application.persistentDataPath + "/DataSave"; } }

    #endregion // Variables

    #region Methods

    public static void SaveData()
    {
        Grid[,] grid = GameManager.Instance.GridController.Grid;

        for (int x = 0; x < grid.GetLength(0); x++)
        {
            for (int y = 0; y < grid.GetLength(1); y++)
            {
                if (grid[x, y].GridState == GridState.Occupied)
                {
                    SavedDatas.OccupiedGrids.Add(new Vector2Int(x, y));
                }
            }
        }

        string stringData = JsonUtility.ToJson(SavedDatas);
        File.WriteAllText(_savePath, stringData);
    }

    public static void LoadData()
    {
        if(File.Exists(_savePath))
        {
            string stringData = File.ReadAllText(_savePath);
            SavedDatas = JsonUtility.FromJson<SaveData>(stringData);

            Grid[,] grid = GameManager.Instance.GridController.Grid;

            // Handle grid data
            foreach(Vector2Int gridIndexes in SavedDatas.OccupiedGrids)
            {
                grid[gridIndexes.x, gridIndexes.y].GridState = GridState.Occupied;
            }

            // Handle building data
            for (int i = 0; i < SavedDatas.ConstructedBuilding_BuildingDatas.Count; i++)
            {
                Building building = SavedDatas.ConstructedBuilding_BuildingDatas[i];
                Vector3 position = SavedDatas.ConstructedBuilding_Positions[i];

                GameObject constructed = Instantiate(building.BuildingShape, position, Quaternion.identity, GameManager.Instance.MapSection.transform);
                
                constructed.GetComponent<BuildingOnGrid>().Building = building;
                constructed.GetComponent<BuildingOnGrid>().enabled = true;

                constructed.GetComponent<SnapToGridHandler>().SnapToGrid();

                foreach(ConstructionTileHandler constructionTileHandler in constructed.GetComponentsInChildren<ConstructionTileHandler>())
                {
                    constructionTileHandler.enabled = false;
                }
            }
        }
    }

    private static void ResetSaveData()
    {
        string saveData = File.ReadAllText(_savePath);
        SavedDatas = JsonUtility.FromJson<SaveData>(saveData);

        SavedDatas = new SaveData();

        string stringData = JsonUtility.ToJson(SavedDatas);
        File.WriteAllText(_savePath, stringData);

        PlayerPrefs.SetInt("Gold", _defaultGoldValue);
        PlayerPrefs.SetInt("Gem", _defaultGemValue);
    }

    private static void UpdateData(Building building)
    {
        SavedDatas.ConstructedBuilding_Positions.Add(GameManager.Instance.ConstructionController.CurrentConstructionGameObject.transform.position);
        SavedDatas.ConstructedBuilding_BuildingDatas.Add(building);
    }

    private static void RestartScene()
    {
        ResetSaveData();

        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }


    #region Events

    public static void SubscribeEvents()
    {
        EventManager.Instance.ConstructionOnGoing += UpdateData;

        EventManager.Instance.PressedRestart += RestartScene;
    }

    private static void UnsubscribeEvents()
    {
        EventManager.Instance.ConstructionOnGoing -= UpdateData;

        EventManager.Instance.PressedRestart -= RestartScene;
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
