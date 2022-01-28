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

    [BoxGroup("Controllers")]
    public FloatingTextController FloatingTextController;

    [BoxGroup("Controllers"), SerializeField]
    private DragController _dragController;

    [BoxGroup("Controllers"), SerializeField]
    private InputController _inputController;

    [BoxGroup("Controllers"), SerializeField]
    private GridBuildingController _gridBuilderController;

    #endregion // Controllers

    #region Others

    [BoxGroup("Others")]
    public ResourceTextsTracker ResourceTextsTracker;

    [BoxGroup("Others")]
    public GameObject MapSection;

    #endregion // Others

    #endregion // Variables

    #region Methods

    private void OnApplicationQuit()
    {
        SaveSystem.SaveData();
    }

    #region Initializations

    private void InitializeLevelElements()
    {
        InitializeManagers();
        InitializeClasses();
        InitializeHandlers();
        InitializeControllers();
        InitializeOthers();

        EventManager.Instance.OnMapTilesCreated();

        InitializeSaveSystem();
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
        foreach(GameObject button in GameObject.FindGameObjectsWithTag("BuildingCard"))
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

    private void InitializeSaveSystem()
    {
        SaveSystem.SubscribeEvents();
        SaveSystem.SavedDatas = new SaveData();
        SaveSystem.LoadData();
    }

    private void InitializeOthers()
    {
        ResourceTextsTracker.SubscribeEvents();
    }

    #endregion // Initializations.

    #region Events

    private void SubscribeEvents()
    {
        EventManager.Instance.ConstructionStarted += GridController.HandleGridsUnderConstruction;
        EventManager.Instance.ConstructionOnGoing += (Building constructedBuilding) => Resource.SpendResource(constructedBuilding.GoldCost, constructedBuilding.GemCost);
        EventManager.Instance.ConstructionOnGoing += FloatingTextController.OnConstructionFeedback;
    }

    private void UnsubscribeEvents()
    {
        EventManager.Instance.ConstructionStarted -= GridController.HandleGridsUnderConstruction;
        EventManager.Instance.ConstructionOnGoing -= (Building constructedBuilding) => Resource.SpendResource(constructedBuilding.GoldCost, constructedBuilding.GemCost);
        EventManager.Instance.ConstructionOnGoing -= FloatingTextController.OnConstructionFeedback;
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
