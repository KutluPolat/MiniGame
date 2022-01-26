using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InputManager : MonoBehaviour
{
    #region Singleton

    public static InputManager Instance { get; private set; }

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

    private void Start()
    {
        SubscribeEvents();
    }

    #endregion // Singleton

    #region Variables

    #endregion // Variables

    #region Updates & Start

    private void Update()
    {
        HandleInputs();
    }

    #endregion // Updates & Start

    #region Methods

    #region Inputs

    private void HandleInputs()
    {
        if (Input.GetMouseButtonUp(0))
        {
            EventManager.Instance.OnLeftMouseButtonReleased();
        }
    }

    #endregion // Inputs

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
