using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Resources
{
    public int Gold 
    {
        get { return SaveSystem.Gold; }
        set { SaveSystem.Gold = value; }
    }
    public int Gem
    {
        get { return SaveSystem.Gem; }
        set { SaveSystem.Gem = value; }
    }

    public Resources()
    {
        EventManager.Instance.OnResourceChanged();
    }

    public bool SpendResource(int gold, int gem)
    {
        if(Gold > gold && Gem > gem)
        {
            Gold -= gold;
            Gem -= gem;

            EventManager.Instance.OnResourceChanged();

            return true;
        }
        else
        {
            return false;
        }
    }

    public void GainResource(int gold, int gem)
    {
        Gold += gold;
        Gem += gem;

        EventManager.Instance.OnResourceChanged();
    }

    public bool IsResourcesEnoughFor(int goldCost, int gemCost)
    {
        if (Gold >= goldCost && Gem >= gemCost)
            return true;

        else
            return false;
    }
}
