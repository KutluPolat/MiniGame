using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InputController : MonoBehaviour
{
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
        if (Input.GetMouseButtonUp(0) && GameManager.Instance.ConstructionController.IsUnderConstruction)
        {
            EventManager.Instance.OnLeftMouseButtonReleased();
        }
    }

    #endregion // Inputs

    #region Events

    public void SubscribeEvents()
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
