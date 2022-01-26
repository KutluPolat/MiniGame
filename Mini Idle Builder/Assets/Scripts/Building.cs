using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "New Building", menuName ="Building")]
public class Building : ScriptableObject
{
    [BoxGroup("Base Properties")]
    public string BuildingName;

    [BoxGroup("Base Properties")]
    public Sprite Sprite;

    [BoxGroup("Base Properties")]
    public float ProductionTime;

    [BoxGroup("Costs")]
    public int GemCost, GoldCost;

    [BoxGroup("Production")]
    public int GemProduction, GoldProduction;
}
