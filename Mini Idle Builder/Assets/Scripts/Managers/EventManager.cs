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
    public delegate void MouseAction();
    public delegate void ResourceChange();
    public delegate void ConstructionStart();
    public delegate void ConstructionOnGo(Building constructedBuilding);
    public delegate void ConstructionComplete();
    public delegate void Save();

    #endregion // Delegates

    #region Events

    public event Buttons PressedRestart;
    public event CreateMapTiles MapTilesCreated;
    public event MouseAction LeftMouseButtonReleased;
    public event ResourceChange ResourceChanged;
    public event ConstructionStart ConstructionStarted;
    public event ConstructionOnGo ConstructionOnGoing; 
    public event ConstructionComplete ConstructionCompleted;
    public event Save Saved;

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

    public void OnLeftMouseButtonReleased()
    {
        if(LeftMouseButtonReleased != null)
        {
            LeftMouseButtonReleased();

            Debug.Log("LeftMouseButtonReleased triggered.");
        }
    }

    public void OnResourceChanged()
    {
        if (ResourceChanged != null)
        {
            ResourceChanged();

            Debug.Log("OnResourceChanged triggered.");
        }
    }

    public void OnConstructionStarted()
    {
        if(ConstructionStarted != null)
        {
            ConstructionStarted();

            Debug.Log("OnConstructionStarted triggered.");
        }
    }

    public void OnConstructionOnGoing(Building constructedObject)
    {
        if(ConstructionOnGoing != null)
        {
            ConstructionOnGoing(constructedObject);

            Debug.Log("OnConstructionOnGoing triggered.");
        }
    }

    public void OnConstructionCompleted()
    {
        if(ConstructionCompleted != null)
        {
            ConstructionCompleted();

            Debug.Log("OnConstructionCompleted triggered.");
        }
    }
    
    public void OnSaved()
    {
        if(Saved != null)
        {
            Saved();

            Debug.Log("OnSaved triggered.");
        }
    }
    
    #endregion // Methods
}
