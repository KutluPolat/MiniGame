using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SaveSystem : MonoBehaviour
{
    #region Variables

    public static int Gold
    {
        get { return PlayerPrefs.GetInt("Gold", _defaultGoldValue); }
        set { PlayerPrefs.SetInt("Gold", value); }
    }

    public static int Gem
    {
        get { return PlayerPrefs.GetInt("Gem", _defaultGemValue); }
        set { PlayerPrefs.SetInt("Gem", value); }
    }

    private static int _defaultGoldValue = 10, _defaultGemValue = 10;

    #endregion // Variables

    #region Methods

    #region Events

    public static void SubscribeEvents()
    {

    }

    private static void UnsubscribeEvents()
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
