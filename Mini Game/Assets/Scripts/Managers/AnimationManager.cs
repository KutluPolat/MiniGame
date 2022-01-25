using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CompanyName.GameName.Enums;

public class AnimationManager : MonoBehaviour
{
    #region Singleton

    public static AnimationManager Instance { get; private set; }

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

        SubscribeEvents();
    }

    #endregion // Singleton

    #region Variables

    #region Animations


    #endregion // Animations

    #region ParticleObjects

    [SerializeField]
    private GameObject _particleTypeOne, _particleTypeTwo, _particleTypeThree;

    #endregion // ParticleObjects

    #endregion // Variables

    #region Methods

    #region ActivateAnimation

    //public void ActivateAnim_Anim1()
    //{
    //    GameManager.Instance.PlayerAnimator.SetBool("Anim1", true);
    //}
    //public void ActivateAnim_Anim2()
    //{
    //    GameManager.Instance.PlayerAnimator.SetBool("Anim2", true);
    //}

    #endregion // ActivateAnimation

    #region Particles

    /// <summary>
    /// Instantiates a game object attached with particle system
    /// </summary>
    /// <param name="particleType">Particle type that will spawn.</param>
    /// <param name="spawnPosition">Spawn position of particle</param>
    /// <param name="lifetime">Lifetime of gameobject of particle system</param>
    public void ActivateParticles(ParticleType particleType, Vector3 spawnPosition, float lifetime = 1f)
    {
        GameObject spawnedParticleObject = Instantiate(GetParticlePrefabFor(particleType), spawnPosition, Quaternion.identity);

        Destroy(spawnedParticleObject, lifetime);
    }

    private GameObject GetParticlePrefabFor(ParticleType particle)
    {
        GameObject particleGameObject;

        switch (particle)
        {
            case ParticleType.TypeOne:
                particleGameObject = _particleTypeOne;
                break;

            case ParticleType.TypeTwo:
                particleGameObject = _particleTypeTwo;
                break;

            case ParticleType.TypeThree:
                particleGameObject = _particleTypeThree;
                break;

            default:
                particleGameObject = null;
                Debug.LogError($"Particle type of {particle} is not integrated!");
                break;
        }

        return particleGameObject;
    }

    #endregion

    #region Camera Animations

    private void CameraConditionOne() => GameManager.Instance.CameraAnimator.Play("ConditionOne");
    private void CameraConditionTwo() => GameManager.Instance.CameraAnimator.Play("ConditionTwo");

    #endregion // Camera Animations

    #region Events

    private void SubscribeEvents()
    {
        EventManager.Instance.OnStateTapToPlay += CameraConditionOne;
        EventManager.Instance.OnStateLevelEnd += CameraConditionTwo;
    }

    private void UnsubscribeEvents()
    {
        EventManager.Instance.OnStateTapToPlay -= CameraConditionOne;
        EventManager.Instance.OnStateLevelEnd -= CameraConditionTwo;
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
