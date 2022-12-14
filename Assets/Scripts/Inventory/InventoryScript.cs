using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public delegate void ItemCountChanged(Item item);

public class InventoryScript : MonoBehaviour
{
    public event ItemCountChanged itemCountChangedEvent;

    private static InventoryScript instance;

    public static InventoryScript MyInstance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<InventoryScript>();
            }

            return instance;
        }
    }

    private SlotScript fromSlot;

    private List<Bag> bags = new List<Bag>();

    [SerializeField]
    private BagButton[] bagButtons;

    //For debugging
    [SerializeField]
    private Item[] items;

    //public bool CanAddBag
    //{
    //    get { return MyBags.Count < 5; }
    //}

    public int MyEmptySlotCount
    {
        get
        {
            int count = 0;

            foreach (Bag bag in MyBags)
            {
                count += bag.MyBagScript.MyEmptySlotCount;
            }

            return count;
        }
    }

    public int MyTotalSlotCount
    {
        get
        {
            int count = 0;

            foreach (Bag bag in MyBags)
            {
                count += bag.MyBagScript.MySlots.Count;
            }

            return count;
        }
    }

    public int MyFullSlotCount
    {
        get
        {
            return MyTotalSlotCount - MyEmptySlotCount;
        }
    }

    public SlotScript FromSlot
    {
        get
        {
            return fromSlot;
        }

        set
        {
            fromSlot = value;

            if (value != null)
            {
                fromSlot.MyCover.enabled = true;
            }
        }
    }

    public List<Bag> MyBags
    {
        get
        {
            return bags;
        }
    }

 

    private void Awake()
    {
        Bag bag = (Bag)Instantiate(items[8]);
        bag.Initialize(20);
        bag.Use();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            Bag bag = (Bag)Instantiate(items[8]);
            bag.Initialize(40);
            AddItem(bag);
        }
        if (Input.GetKeyDown(KeyCode.X))//Debugging for adding a bag to the inventory
        {
            //Bag bag = (Bag)Instantiate(items[8]);
            //bag.Initialize(20);
            //AddItem(bag);

        }
        if (Input.GetKeyDown(KeyCode.C))
        {
            HealthPotion potion = (HealthPotion)Instantiate(items[9]);
            AddItem(potion);
        }
        if (Input.GetKeyDown(KeyCode.V))
        {
            GoldNugget nugget = (GoldNugget)Instantiate(items[11]);
            AddItem(nugget);
        }
        if (Input.GetKeyDown(KeyCode.H))
        {

            AddItem((Armor)Instantiate(items[0]));
            AddItem((Armor)Instantiate(items[1]));
            AddItem((Armor)Instantiate(items[2]));
            AddItem((Armor)Instantiate(items[3]));
            AddItem((Armor)Instantiate(items[4]));
            AddItem((Armor)Instantiate(items[5]));
            AddItem((Armor)Instantiate(items[6]));
            AddItem((Armor)Instantiate(items[7]));
            AddItem((Armor)Instantiate(items[10]));
           

        }

    }

    /// <summary>
    /// Equips a bag to the inventory
    /// </summary>
    /// <param name = "bag" ></ param >
    public void AddBag(Bag bag)
    {
        foreach (BagButton bagButton in bagButtons)
        {
            if (bagButton.MyBag == null)
            {
                bagButton.MyBag = bag;
                MyBags.Add(bag);
                bag.MyBagButton = bagButton;
                //bag.MyBagScript.transform.SetSiblingIndex(bagButton.MyBagIndex);
                break;
            }
        }
    }

    //public void AddBag(Bag bag, BagButton bagButton)
    //{
    //    MyBags.Add(bag);
    //    bagButton.MyBag = bag;
    //    //bag.MyBagScript.transform.SetSiblingIndex(bagButton.MyBagIndex);
    //}

    //public void AddBag(Bag bag, int bagIndex)
    //{
    //    bag.SetupScript();
    //    MyBags.Add(bag);
    //    bag.MyBagScript.MyBagIndex = bagIndex;
    //    bag.MyBagButton = bagButtons[bagIndex];
    //    bagButtons[bagIndex].MyBag = bag;
    //}

    ///// <summary>
    ///// Removes the bag from the inventory
    ///// </summary>
    ///// <param name="bag"></param>
    //public void RemoveBag(Bag bag)
    //{
    //    MyBags.Remove(bag);
    //    Destroy(bag.MyBagScript.gameObject);
    //}

    //public void SwapBags(Bag oldBag, Bag newBag)
    //{
    //    int newSlotCount = (MyTotalSlotCount - oldBag.MySlotCount) + newBag.MySlotCount;

    //    if (newSlotCount - MyFullSlotCount >= 0)
    //    {
    //        //Do Swap
    //        List<Item> bagItems = oldBag.MyBagScript.GetItems();

    //        RemoveBag(oldBag);

    //        newBag.MyBagButton = oldBag.MyBagButton;

    //        newBag.Use();

    //        foreach (Item item in bagItems)
    //        {
    //            if (item != newBag) //No duplicates
    //            {
    //                AddItem(item);
    //            }
    //        }

    //        AddItem(oldBag);

    //        HandScript.MyInstance.Drop();

    //        MyInstance.fromSlot = null;

    //    }
    //}

    /// <summary>
    /// Adds an item to the inventory
    /// </summary>
    /// <param name="item">Item to add</param>
    public bool AddItem(Item item)
    {
        if (item.MyStackSize > 0)
        {
            if (PlaceInStack(item))
            {
                return true;
            }
        }

       return PlaceInEmpty(item);
    }

    /// <summary>
    /// Places an item on an empty slot in the game
    /// </summary>
    /// <param name="item">Item we are trying to add</param>
    private bool PlaceInEmpty(Item item)
    {
        foreach (Bag bag in MyBags)//Checks all bags
        {
            if (bag.MyBagScript.AddItem(item)) //Tries to add the item
            {
                OnItemCountChanged(item);
                return true; //It was possible to add the item
            }
        }

        return false;
    }

    /// <summary>
    /// Tries to stack an item on anothe
    /// </summary>
    /// <param name="item">Item we try to stack</param>
    /// <returns></returns>
    private bool PlaceInStack(Item item)
    {
        foreach (Bag bag in MyBags)//Checks all bags
        {
            foreach (SlotScript slots in bag.MyBagScript.MySlots) //Checks all the slots on the current bag
            {
                if (slots.StackItem(item)) //Tries to stack the item
                {
                    OnItemCountChanged(item);
                    return true; //It was possible to stack the item
                }
            }
        }

        return false; //It wasn't possible to stack the item
    }

    public void PlaceInSpecific(Item item, int slotIndex, int bagIndex)
    {
        bags[bagIndex].MyBagScript.MySlots[slotIndex].AddItem(item);

    }

    /// <summary>
    /// Opens and closes all bags
    /// </summary>
    public void OpenClose()
    {
        //Checks if any bags are closed
        bool closedBag = MyBags.Find(x => !x.MyBagScript.IsOpen);

        //If closed bag == true, then open all closed bags
        //If closed bag == false, then close all open bags

        foreach (Bag bag in MyBags)
        {
            if (bag.MyBagScript.IsOpen != closedBag)
            {
                bag.MyBagScript.OpenClose();
            }
        }
    }

    public List<SlotScript> GetAllItems()
    {
        List<SlotScript> slots = new List<SlotScript>();

        foreach (Bag bag in MyBags)
        {
            foreach (SlotScript slot in bag.MyBagScript.MySlots)
            {
                if (!slot.IsEmpty)
                {
                    slots.Add(slot);
                }
            }
        }

        return slots;
    }



    public Stack<IUseable> GetUseables(IUseable type)
    {
        Stack<IUseable> useables = new Stack<IUseable>();

        foreach (Bag bag in MyBags)
        {
            foreach (SlotScript slot in bag.MyBagScript.MySlots)
            {
                if (!slot.IsEmpty && slot.MyItem.GetType() == type.GetType())
                {
                    foreach (Item item in slot.MyItems)
                    {
                        useables.Push(item as IUseable);
                    }
                }
            }
        }

        return useables;
    }

    public IUseable GetUseable(string type)
    {
        Stack<IUseable> useables = new Stack<IUseable>();

        foreach (Bag bag in MyBags)
        {
            foreach (SlotScript slot in bag.MyBagScript.MySlots)
            {
                if (!slot.IsEmpty && slot.MyItem.MyTitle == type)
                {
                    return (slot.MyItem as IUseable);
                }
            }
        }

        return null;
    }

    public int GetItemCount(string type)
    {
        int itemCount = 0;

        foreach (Bag bag in MyBags)
        {
            foreach (SlotScript slot in bag.MyBagScript.MySlots)
            {
                if (!slot.IsEmpty && slot.MyItem.MyTitle == type)
                {
                    itemCount += slot.MyItems.Count;
                }
            }
        }

        return itemCount;

    }

    public Stack<Item> GetItems(string type, int count)
    {
        Stack<Item> items = new Stack<Item>();

        foreach (Bag bag in MyBags)
        {
            foreach (SlotScript slot in bag.MyBagScript.MySlots)
            {
                if (!slot.IsEmpty && slot.MyItem.MyTitle == type)
                {
                    foreach (Item item in slot.MyItems)
                    {
                        items.Push(item);

                        if (items.Count == count)
                        {
                            return items;
                        }
                    }
                }
            }
        }

        return items;

    }

    public void RemoveItem(Item item)
    {
        foreach (Bag bag in MyBags)
        {
            foreach (SlotScript slot in bag.MyBagScript.MySlots)
            {
                if (!slot.IsEmpty && slot.MyItem.MyTitle == item.MyTitle)
                {
                    slot.RemoveItem(item);
                    break;
                }
            }
        }
    }

    public void OnItemCountChanged(Item item)
    {
        if (itemCountChangedEvent != null)
        {
            itemCountChangedEvent.Invoke(item);
        }
    }

  
}
