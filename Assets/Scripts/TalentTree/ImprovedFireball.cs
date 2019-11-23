using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ImprovedFireball : Talent, IPointerEnterHandler, IPointerExitHandler, IDescribable
{
    public override bool Click()
    {
        if (base.Click())
        {
            //Give the player the talent's ability
            SpellBook.MyInstance.GetSpell("Fireball").MyCastTime -= 0.1f;
            return true;
        }

        return false;
     
    }

    public string GetDescription()
    {
        return string.Format("Improved Fireball\n<color=#ffd100>Reduces the casting time\nof your Fireball by 0.1 sec. </color>");
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        UIManager.MyInstance.ShowTooltip(new Vector2(1, 0), transform.position, this);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        UIManager.MyInstance.HideTooltip();
    }
}
