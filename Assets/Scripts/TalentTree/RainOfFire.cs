using Assets.Scripts.Debuffs;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RainOfFire : Talent
{

    public void Start()
    {

    }

    public override bool Click()
    {
        if (base.Click())
        {
            SpellBook.MyInstance.LearnSpell("Rain Of Fire");
            
        }

        return false;

    }

    //public override string GetDescription()
    //{
    //    return $"Ignite<color=#ffd100>\nYour fireball applies a debuff\nto the target that\ndoes {debuff.MyTickDamage*debuff.MyDuration} damage over {debuff.MyDuration} seconds</color>{nextRank}";  
    //}
}
