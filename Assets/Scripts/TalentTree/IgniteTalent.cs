using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IgniteTalent : Talent
{
    private float tickDamage = 5;

    private float damageIncrease = 2;

    private string nextRank = string.Empty;

    //These variables will be replaced;
    float tmpDuration = 20;
    float tmpDamage = 5;

    public override bool Click()
    {
        if (base.Click())
        {
            tmpDamage = tickDamage;

            if (MyCurrentCount < 3)
            {
                tickDamage += damageIncrease;
                nextRank = $"<color=#ffffff>\n\nNext rank:\n</color><color=#ffd100>Your fireball applies a debuff\nto the target that\ndoes {tickDamage * tmpDuration} damage over {tmpDuration} seconds</color>\n";
            }
            else
            {
                nextRank = string.Empty;
            }
            //Give the player the talent's ability
            UIManager.MyInstance.RefreshTooltip(this);
            return true;
        }

        return false;

    }

    public override string GetDescription()
    {
        return $"Ignite<color=#ffd100>\nYour fireball applies a debuff\nto the target that\ndoes {tmpDamage*tmpDuration} damage over {tmpDuration} seconds</color>{nextRank}";
            
           
    }
}
