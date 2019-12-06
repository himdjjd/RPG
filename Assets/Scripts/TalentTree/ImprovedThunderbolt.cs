using UnityEngine;
using UnityEngine.EventSystems;

public class ImprovedThunderbolt : Talent, IPointerEnterHandler, IPointerExitHandler, IDescribable
{
    private int percent = 5;

    public override bool Click()
    {
        if (base.Click())
        {
            Spell thunderBolt = SpellBook.MyInstance.GetSpell("Thunderbolt");

            //Give the player the talent's ability
            thunderBolt.MyDamage += (thunderBolt.MyDamage / 100) * percent;
            return true;
        }

        return false;

    }

    public string GetDescription()
    {
        return string.Format($"Improved Thunderbolt\n<color=#ffd100>Increas the damge\nof your Thunderbolt by {percent}%. </color>");
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
