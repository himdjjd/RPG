using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class BagButton : MonoBehaviour, IPointerClickHandler
{
    private static BagButton instance;

    public static BagButton MyInstance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<BagButton>();
            }

            return instance;
        }
    }
    /// <summary>
    /// A reference to the bag item
    /// </summary>
    private Bag bag;

    [SerializeField]
    private GameObject closeBtn;
    /// <summary>
    /// Sprites to indicate if the bag is full or empty
    /// </summary>
    //[SerializeField]
    //private Sprite full, empty;

    //[SerializeField]
    //private int bagIndex;

    /// <summary>
    /// A property for accessing the specific bag
    /// </summary>
    public Bag MyBag
    {
        get
        {
            return bag;
        }

        set
        {
            //if (value != null)
            //{
            //    GetComponent<Image>().sprite = full;
            //}
            //else
            //{
            //    GetComponent<Image>().sprite = empty;
            //}

            bag = value;
        }
    }

    //public int MyBagIndex
    //{
    //    get
    //    {
    //        return bagIndex;
    //    }

    //    set
    //    {
    //        bagIndex = value;
    //    }
    //}

    /// <summary>
    /// if we click the specific bag button
    /// </summary>
    /// <param name="eventData"></param>
    [System.Obsolete]
    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            //if (InventoryScript.MyInstance.FromSlot != null && HandScript.MyInstance.MyMoveable != null && HandScript.MyInstance.MyMoveable is Bag)
            //{
            //    if (MyBag != null)
            //    {
            //        InventoryScript.MyInstance.SwapBags(MyBag, HandScript.MyInstance.MyMoveable as Bag);
            //    }
            //    else
            //    {
            //        Bag tmp = (Bag)HandScript.MyInstance.MyMoveable;
            //        tmp.MyBagButton = this;
            //        tmp.Use();
            //        MyBag = tmp;
            //        HandScript.MyInstance.Drop();
            //        InventoryScript.MyInstance.FromSlot = null;
            //    }

            //else if (Input.GetKey(KeyCode.LeftShift))
            //{
            //    HandScript.MyInstance.TakeMoveable(MyBag);
            //}
            //else 
            if (bag != null)//If we have a bag equipped
            {
                //Open or close the bag
                bag.MyBagScript.OpenClose();
                if (closeBtn.active==true)
                {
                    closeBtn.SetActive(false);
                }
                else
                {
                    closeBtn.SetActive(true);
                }
            }
        }
        }

  
    

/// <summary>
/// Removes the bag from the bagbar
/// </summary>
//public void RemoveBag()
//{
//    InventoryScript.MyInstance.RemoveBag(MyBag);
//    MyBag.MyBagButton = null;

//    foreach (Item item in MyBag.MyBagScript.GetItems())
//    {
//        InventoryScript.MyInstance.AddItem(item);
//    }

//    MyBag = null;
//}

    public void Close()
    {
        closeBtn.SetActive(false);
        bag.MyBagScript.OpenClose();
    }
}
