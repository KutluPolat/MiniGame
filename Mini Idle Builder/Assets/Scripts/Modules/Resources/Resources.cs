using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Resources
{
    public int Gold { get; private set; } = 10;
    public int Gem { get; private set; } = 10;

    public bool SpendResource(int gold, int gem)
    {
        if(Gold > gold && Gem > gem)
        {
            Gold -= gold;
            Gem -= gem;

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
    }
}
