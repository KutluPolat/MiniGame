using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using UnityEngine.UI;

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
    public Resources Resource { get; private set; }

    #endregion // Classes

    #region Controllers
    [BoxGroup("Controllers")]
    public ConstructionController ConstructionController;

    [BoxGroup("Controllers"), SerializeField]
    private DragController _dragController;

    [BoxGroup("Controllers"), SerializeField]
    private InputController _inputController;

    [BoxGroup("Controllers"), SerializeField]
    private GridBuildingController _gridBuilderController;

    #endregion // Controllers

    #region Others

    [BoxGroup("Others"), SerializeField]
    private ResourceTextsTracker _resourceTextsTracker;

    #endregion // Others

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
        InitializeOthers();

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
        this.Resource = new Resources();
    }

    private void InitializeHandlers()
    {
        foreach(Button button in GameObject.FindObjectsOfType<Button>())
        {
            button.GetComponent<ButtonHandler>().SubscribeEvents();
        }
    }

    private void InitializeControllers()
    {
        ConstructionController.SubscribeEvents();
        // _dragController don't have subscribtions.
        _inputController.SubscribeEvents();
        _gridBuilderController.SubscribeEvents();
    }

    private void InitializeOthers()
    {
        _resourceTextsTracker.SubscribeEvents();
    }

    #endregion // Initializations.

    #region Events

    private void SubscribeEvents()
    {
        EventManager.Instance.ConstructionStarted += GridController.HandleGridsUnderConstruction;
        EventManager.Instance.ConstructionOnGoing += (Building constructedBuilding) => Resource.SpendResource(constructedBuilding.GoldCost, constructedBuilding.GemCost);
    }

    private void UnsubscribeEvents()
    {
        EventManager.Instance.ConstructionStarted -= GridController.HandleGridsUnderConstruction;
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
