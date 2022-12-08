using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class HandScript : MonoBehaviour
{
    /// <summary>
    /// Singleton instance of the handscript
    /// </summary>
    private static HandScript instance;

    public static HandScript MyInstance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<HandScript>();
            }

            return instance;
        }
    }

    /// <summary>
    /// The current moveable
    /// </summary>
    public IMoveable MyMoveable { get; set; }

    /// <summary>
    /// The icon of the item, that we acre moving around atm.
    /// </summary>
    private Image icon;

    /// <summary>
    /// An offset to move the icon away from the mouse
    /// </summary>
    [SerializeField]
    private Vector3 offset;

    [SerializeField]
    private GameObject dialogue;

    [SerializeField]
    private Text dialogueText;

    // Use this for initialization
    void Start ()
    {
        //Creates a reference to the image on the hand
        icon = GetComponent<Image>();	
	}
	
	// Update is called once per frame
	void Update ()
    {
        //Makes sure that the icon follows the hand
        icon.transform.position = Input.mousePosition+offset;

        if (Input.GetMouseButton(0) && !EventSystem.current.IsPointerOverGameObject() && MyInstance.MyMoveable != null)
        {
            DeleteItem();
        }

      
	}

    /// <summary>
    /// Take a moveable in the hand, so that we can move it around
    /// </summary>
    /// <param name="moveable">The moveable to pick up</param>
    string moveableText = null;
    public void TakeMoveable(IMoveable moveable,Item item)
    {
        this.MyMoveable = moveable;
        icon.sprite = moveable.MyIcon;
        icon.enabled = true;
        moveableText = item.MyTitle;
    }

    public void SpellTakeMoveable(IMoveable moveable)
    {
        this.MyMoveable = moveable;
        icon.sprite = moveable.MyIcon;
        icon.enabled = true;
    }

    public IMoveable Put()
    {
        IMoveable tmp = MyMoveable;
        MyMoveable = null;
        icon.enabled = false;
        return tmp;
    }

    public void Drop()
    {
        MyMoveable = null;
        icon.enabled = false;
        InventoryScript.MyInstance.FromSlot = null;
        CloseDialogue();
    }

    public void DropTrue()
    {
        if (MyMoveable is Item)
        {
            Item item = (Item)MyMoveable;
            if (item.MySlot != null)
            {
                item.MySlot.Clear();
            }
            else if (item.MyCharButton != null)
            {
                item.MyCharButton.DequipArmor();
            }

        }

        InventoryScript.MyInstance.FromSlot = null;

        MyMoveable = null;
        icon.enabled = false;
        InventoryScript.MyInstance.FromSlot = null;
        CloseDialogue();
    }

    public void DropFalse()
    {
        CloseDialogue();
      
    }

    /// <summary>
    /// Deletes an item from the inventory
    /// </summary>
    public void DeleteItem()
    {


        ShowDialogue(moveableText);


    }

    public void ShowDialogue(string text)
    {
        if (text == null)
        {
            dialogueText.text = "取消操作？";
        }
        else
        {
            dialogueText.text = "你要丢弃" + text + "吗？";
        }

       

        dialogue.SetActive(true);
    }

    public void CloseDialogue()
    {
        moveableText = null;
        dialogue.SetActive(false);
    }
}
