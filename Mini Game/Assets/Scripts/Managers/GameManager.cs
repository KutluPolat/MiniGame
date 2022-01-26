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

    [TabGroup("Level Elements", "Animators"), SceneObjectsOnly]
    public Animator CameraAnimator;

    #endregion // Level Elements

    #region Script Holders

    [BoxGroup("Script Holders"), SceneObjectsOnly, SerializeField]
    private GameObject _managerHolder, _controllerHolder;

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

    #region Controller Prefabs

    [BoxGroup("Controllers"), AssetsOnly, SerializeField]
    private GameObject _gridControllerPrefab;

    #endregion // Controller Prefabs

    #region Controller Properties

    public GridController GridController { get; private set; }

    #endregion // Controller Properties

    #endregion // Variables

    #region Methods

    #region Initializations

    private void InitializeLevelElements()
    {
        InitializeManagers();
        SaveSystem.SubscribeEvents();
        InitializeControllers();
    }

    private void InitializeManagers()
    {
        foreach(GameObject manager in _managers)
        {
            Instantiate(manager, _managerHolder.transform);
        }
    }

    private void InitializeControllers()
    {
        GridController = Instantiate(_gridControllerPrefab, _controllerHolder.transform).GetComponent<GridController>();
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
