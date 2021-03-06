using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using Sirenix.OdinInspector;
using ArvisGames.MiniIdleBuilder.Enums;

public class SFXManager : MonoBehaviour
{
    #region Singleton

    public static SFXManager Instance { get; private set; }

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

    [BoxGroup("Audio Mixer Controller"), InlineEditor(InlineEditorModes.LargePreview), AssetsOnly, ShowInInspector]
    private AudioMixer _mixer;

    [BoxGroup("Audio Clips"), InlineEditor(InlineEditorModes.SmallPreview), AssetsOnly, ShowInInspector]
    [InlineButton("PlayOneShotAudio", "Test")]
    private AudioClip _buttonClick, _actionOne, _actionTwo, _actionThree;

    [BoxGroup("Audio Sources"), SceneObjectsOnly, ShowInInspector]
    private AudioSource _generalAudioSource;

    #endregion // Variables

    #region Methods

    #region Audio

    public void PlayOneShotAudio(AudioClip audio)
    {
        if (_generalAudioSource != null && _generalAudioSource.isPlaying == false)
            _generalAudioSource.PlayOneShot(audio);
    }

    public void PlayOneShotAudioImmidiately(AudioClip audio)
    {
        if (_generalAudioSource != null)
            _generalAudioSource.PlayOneShot(audio);
    }

    #endregion // Audio

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
