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

    #endregion // Delegates

    #region Events

    public event Buttons OnPressedRestart;

    #endregion // Events

    #region Methods

    public void TriggerOnPressedRestart()
    {
        if (OnPressedRestart != null)
        {
            OnPressedRestart();

            Debug.Log("OnPressedRestart triggered.");
        }
    }

    #endregion // Methods
}
