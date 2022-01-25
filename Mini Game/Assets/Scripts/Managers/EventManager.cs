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
    public delegate void States();

    #endregion // Delegates

    #region Events

    public event Buttons OnPressedRestart, OnPressedNextLevel;
    public event States OnStateTapToPlay, OnStateInGame, OnStateLevelEnd;

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

    public void TriggerOnPressedNextLevel()
    {
        if (OnPressedNextLevel != null)
        {
            OnPressedNextLevel();

            Debug.Log("OnPressedNextLevel triggered.");
        }
    }

    public void TriggerOnStateTapToPlay()
    {
        if (OnStateTapToPlay != null)
        {
            OnStateTapToPlay();

            Debug.Log("OnTapToPlay triggered.");
        }
    }

    public void TriggerOnStateInGame()
    {
        if (OnStateInGame != null)
        {
            OnStateInGame();
            Debug.Log("OnInGame triggered.");
        }
    }

    public void TriggerOnStateLevelEnd()
    {
        if (OnStateLevelEnd != null)
        {
            OnStateLevelEnd();
            Debug.Log("OnLevelEnd triggered.");
        }
    }

    #endregion // Methods
}
