using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;

public class FloatingTextController : MonoBehaviour
{
    #region Variables

    [SerializeField]
    private GameObject _floatingTextForBuildingsPrefab, _floatingTextForPanelPrefab, _canvas;

    [SerializeField, Range(0.01f, 5f)]
    private float _feedbackDuration = 2;
    
    [SerializeField]
    private float _feedbackFloatingDistance = 0.2f;

    [SerializeField]
    private Vector3 _feedbackOffsetForBuildings = new Vector3(100, 75), _feedbackOffsetForPanel = new Vector3(250, -50);

    private GameObject TMProUGUI => GameManager.Instance.ResourceTextsTracker.GoldUGUI.gameObject;

    #endregion // Variables

    #region Methods

    public void OnConstructionFeedback(Building building)
    {
        GameObject constructionShape = GameManager.Instance.ConstructionController.CurrentConstructionGameObject;

        Feedback(building.GoldCost * -1, building.GemCost * -1, 
            constructionShape.transform.position, _canvas.transform,
            _floatingTextForBuildingsPrefab, _feedbackOffsetForBuildings, Color.red);

        Feedback(building.GoldCost * -1, building.GemCost * -1, 
            TMProUGUI.transform.position, _canvas.transform,
            _floatingTextForPanelPrefab, _feedbackOffsetForPanel, Color.red);
    }

    public void OnProductionFeedback(GameObject buildingOnGrid)
    {
        Building building = buildingOnGrid.GetComponent<BuildingOnGrid>().Building;

        Feedback(building.GoldProduction, building.GemProduction, 
            buildingOnGrid.transform.position, _canvas.transform,
            _floatingTextForBuildingsPrefab, _feedbackOffsetForBuildings, Color.green);

        Feedback(building.GoldProduction, building.GemProduction, 
            TMProUGUI.transform.position, _canvas.transform,
            _floatingTextForPanelPrefab, _feedbackOffsetForPanel, Color.green);
    }

    private void Feedback(int goldValue, int gemValue, Vector3 spawnPosition, Transform parent, GameObject prefab, Vector3 spawnOffset, Color color)
    {
        GameObject feedback = Instantiate(prefab, spawnPosition + spawnOffset, Quaternion.identity, parent);

        FloatingTextHandler textHandler = feedback.GetComponent<FloatingTextHandler>();

        if(goldValue != 0)
        {
            textHandler.Gold.text = goldValue.ToString();
            textHandler.Gold.DOFade(0, _feedbackDuration);
            textHandler.GoldIcon.DOFade(0, _feedbackDuration);
            textHandler.Gold.color = color;
        }
        else
        {
            Destroy(textHandler.Gold);
            Destroy(textHandler.GoldIcon);
        }

        if(gemValue != 0)
        {
            textHandler.Gem.text = gemValue.ToString();
            textHandler.Gem.DOFade(0, _feedbackDuration);
            textHandler.GemIcon.DOFade(0, _feedbackDuration);
            textHandler.Gem.color = color;
        }
        else
        {
            Destroy(textHandler.Gem);
            Destroy(textHandler.GemIcon);
        }

        feedback.transform
            .DOMoveY(spawnPosition.y + _feedbackFloatingDistance, _feedbackDuration)
            .OnComplete(() => Destroy(feedback));
    }

    #endregion // Methods
}
