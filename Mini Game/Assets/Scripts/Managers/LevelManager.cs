using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    #region Singleton

    public static LevelManager Instance { get; private set; }

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

    #region Methods

    #region Scene Management

    public void InitializeLevel()
    {
        if (SceneManager.GetActiveScene().buildIndex != SaveSystem.IndexOfLevelInSceneBuild)
            ReloadActiveScene();
    }

    private void NextLevel()
    {
        SaveSystem.Level++;

        if (SaveSystem.Level > SceneManager.sceneCountInBuildSettings)
            SaveSystem.Level = 1;

        SceneManager.LoadScene(SaveSystem.IndexOfLevelInSceneBuild);
    }

    private void ReloadActiveScene() => SceneManager.LoadScene(SaveSystem.IndexOfLevelInSceneBuild);

    #endregion // Scene Management

    #region Sub & Unsub Events

    private void SubscribeEvents()
    {
        EventManager.Instance.OnPressedRestart += ReloadActiveScene;
        EventManager.Instance.OnPressedNextLevel += NextLevel;
    }
    private void UnsubscribeEvents()
    {
        EventManager.Instance.OnPressedRestart -= ReloadActiveScene;
        EventManager.Instance.OnPressedNextLevel -= NextLevel;
    }

    #endregion // Sub & Unsub Events

    #region OnDestroy

    private void OnDestroy()
    {
        UnsubscribeEvents();
    }

    #endregion // OnDestroy

    #endregion // Methods
}
