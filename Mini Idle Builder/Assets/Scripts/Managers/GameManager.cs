using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

public class GameManager : MonoBehaviour
{
    #region Singleton

    public static GameManager Instance { get; private set; }

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

        InitializeLevelElements();
    }

    private void Start()
    {
        SubscribeEvents();
    }

    #endregion // Singleton

    #region Variables

    #region Level Elements

    [TabGroup("Level Elements", "Buildings"), InlineEditor(InlineEditorModes.LargePreview), SceneObjectsOnly]
    public GameObject Building1, Building2;

    #endregion // Level Elements

    #region Script Holders

    [BoxGroup("Script Holders"), SceneObjectsOnly, SerializeField]
    private GameObject _managerHolder, _controllerHolder, _handlerHolder;

    #endregion // Script Holders

    #region Manager Prefabs

    /* How to add a new manager?
     * 1-) Create the manager script
     * 2-) Create an empty game object and assign the script to it
     * 3-) Prefabricate the created object
     * 4-) Drag and drop the list below in the editor.
     * 
     * Order in the editor also represents order in the hierarchy
     */
    [BoxGroup("Managers"), AssetsOnly, SerializeField]
    private List<GameObject> _managers;

    #endregion // Manager Prefabs

    #region Classes

    public GridController GridController { get; private set; }

    #endregion // Classes

    #region Handler Prefabs

    [BoxGroup("Handlers"), AssetsOnly, SerializeField]
    private List<GameObject> _handlers;

    #endregion // Handler Prefabs

    #region Controllers

    public ConstructionController BuildingController;
    public DragController DragController;

    #endregion // Controllers

    #endregion // Variables

    #region Methods

    #region Initializations

    private void InitializeLevelElements()
    {
        InitializeManagers();
        SaveSystem.SubscribeEvents();
        InitializeClasses();
        InitializeHandlers();
        InitializeControllers();

        EventManager.Instance.OnMapTilesCreated();
    }

    private void InitializeManagers()
    {
        foreach(GameObject manager in _managers)
        {
            Instantiate(manager, _managerHolder.transform);
        }
    }

    private void InitializeClasses()
    {
        this.GridController = new GridController();
    }

    private void InitializeHandlers()
    {
        foreach(GameObject handler in _handlers)
        {
            Instantiate(handler, _handlerHolder.transform);
        }
    }

    private void InitializeControllers()
    {
        BuildingController.SubscribeEvents();
    }

    #endregion // Initializations.

    #region Events

    private void SubscribeEvents()
    {

    }

    private void UnsubscribeEvents()
    {

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
