using UnityEngine;
using UnityEngine.EventSystems;

public class ImprovedFrostbolt : Talent, IPointerEnterHandler, IPointerExitHandler, IDescribable
{
    public override bool Click()
    {
        if (base.Click())
        {
            //Give the player the talent's ability
            SpellBook.MyInstance.GetSpell("Frostbolt").MyRange += 1;
            return true;
        }

        return false;

    }

    public string GetDescription()
    {
        return string.Format("Improved Frostbolt\n<color=#ffd100>Increases the range\nof your Frostbolt by 1. </color>");
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
