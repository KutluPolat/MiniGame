using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : MonoBehaviour
{
    #region Singleton

    public static EventManager Instance { get; private set; }

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    #endregion // Singleton

    #region Delegates

    public delegate void Buttons();
    public delegate void CreateMapTiles();

    #endregion // Delegates

    #region Events

    public event Buttons PressedRestart;
    public event CreateMapTiles MapTilesCreated;

    #endregion // Events

    #region Methods

    public void OnPressedRestart()
    {
        if (PressedRestart != null)
        {
            PressedRestart();

            Debug.Log("OnPressedRestart triggered.");
        }
    }

    public void OnMapTilesCreated()
    {
        if(MapTilesCreated != null)
        {
            MapTilesCreated();

            Debug.Log("OnMapTilesCreated triggered.");
        }
    }

    #endregion // Methods
}