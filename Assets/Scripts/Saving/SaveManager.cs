using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class SaveManager : MonoBehaviour
{
    [SerializeField]
    private Item[] items;

    private Chest[] chests;

    private CharButton[] equipment;

    [SerializeField]
    private ActionButton[] actionButtons;

    // Use this for initialization
    void Awake()
    {
        chests = FindObjectsOfType<Chest>();
        equipment = FindObjectsOfType<CharButton>();

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            Save();
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            Load();
        }
    }

    private void Save()
    {
        try
        {
            BinaryFormatter bf = new BinaryFormatter();

            FileStream file = File.Open(Application.persistentDataPath + "/" + "SaveTest.dat", FileMode.Create);

            SaveData data = new SaveData();

            SaveEquipment(data);

            SaveBags(data);

            SavePlayer(data);

            SaveChests(data);

            SaveActionButtons(data);

            bf.Serialize(file, data);

            file.Close();


        }
        catch (System.Exception)
        {
            //This is for handling errors
            throw;
        }
    }

    private void SavePlayer(SaveData data)
    {
        data.MyPlayerData = new PlayerData(Player.MyInstance.MyLevel,
            Player.MyInstance.MyXp.MyCurrentValue, Player.MyInstance.MyXp.MyMaxValue,
            Player.MyInstance.MyHealth.MyCurrentValue, Player.MyInstance.MyHealth.MyMaxValue,
            Player.MyInstance.MyMana.MyCurrentValue, Player.MyInstance.MyMana.MyMaxValue,
            Player.MyInstance.transform.position);
    }

    private void SaveChests(SaveData data)
    {
        for (int i = 0; i < chests.Length; i++)
        {
            data.MyChestData.Add(new ChestData(chests[i].name));

            foreach (Item item in chests[i].MyItems)
            {
                if (chests[i].MyItems.Count > 0)
                {
                    data.MyChestData[i].MyItems.Add(new ItemData(item.MyTitle, item.MySlot.MyItems.Count, item.MySlot.MyIndex));
                }
            }
        }
    }

    public void SaveBags(SaveData data)
    {
        for (int i = 1; i < InventoryScript.MyInstance.MyBags.Count; i++)
        {
            data.MyInventoryData.MyBags.Add(new BagData(InventoryScript.MyInstance.MyBags[i].MySlotCount, InventoryScript.MyInstance.MyBags[i].MyBagButton.MyBagIndex));

        }
    }

    public void SaveEquipment(SaveData data)
    {

        foreach (CharButton charButton in equipment)
        {
            if (charButton.MyEquippedArmor != null)
            {
                data.MyEquipmentData.Add(new EquipmentData(charButton.MyEquippedArmor.MyTitle, charButton.name));
            }
        }
    }

    public void SaveActionButtons(SaveData data)
    {
        for (int i = 0; i < actionButtons.Length; i++)
        {
            if (actionButtons[i].MyUseable != null)
            {
                ActionButtonData action;

                if (actionButtons[i].MyUseable is Spell)
                {
                    action = new ActionButtonData((actionButtons[i].MyUseable as Spell).MyName, false, i);
                }
                else
                {
                    action = new ActionButtonData((actionButtons[i].MyUseable as Item).MyTitle, true, i);
                }

                data.MyActionButtonData.Add(action);
            }
        }
    }


    private void Load()
    {
        try
        {
            BinaryFormatter bf = new BinaryFormatter();

            FileStream file = File.Open(Application.persistentDataPath + "/" + "SaveTest.dat", FileMode.Open);

            SaveData data = (SaveData)bf.Deserialize(file);

            file.Close();

            LoadEquipment(data);

            LoadBags(data);

            LoadPlayer(data);

            LoadChests(data);

            LoadActionButtons(data);

        }
        catch (System.Exception)
        {
            //This is for handling errors

            throw;
        }
    }

    private void LoadPlayer(SaveData data)
    {
        Player.MyInstance.MyLevel = data.MyPlayerData.MyLevel;
        Player.MyInstance.UpdateLevel();
        Player.MyInstance.MyHealth.Initialize(data.MyPlayerData.MyHealth, data.MyPlayerData.MyMaxHealth);
        Player.MyInstance.MyMana.Initialize(data.MyPlayerData.MyMana, data.MyPlayerData.MyMaxMana);
        Player.MyInstance.MyXp.Initialize(data.MyPlayerData.MyXp, data.MyPlayerData.MyMaxXP);
        Player.MyInstance.transform.position = new Vector2(data.MyPlayerData.MyX, data.MyPlayerData.MyY);

    }

    private void LoadChests(SaveData data)
    {
        foreach (ChestData chest in data.MyChestData)
        {
            Chest c = Array.Find(chests, x => x.name == chest.MyName);

            foreach (ItemData itemData in chest.MyItems)
            {
                Item item = Array.Find(items, x => x.MyTitle == itemData.MyTitel);
                item.MySlot = c.MyBag.MySlots.Find(x => x.MyIndex == itemData.MySlotIndex);
                c.MyItems.Add(item);
            }
        }

    }

    public void LoadBags(SaveData data)
    {
        foreach (BagData bagData in data.MyInventoryData.MyBags)
        {
            Bag newBag = (Bag)Instantiate(items[0]);

            newBag.Initialize(bagData.MySlotCount);

            InventoryScript.MyInstance.AddBag(newBag, bagData.MyBagIndex);
        }
    }

    public void LoadEquipment(SaveData data)
    {
        foreach (EquipmentData equipmentData in data.MyEquipmentData)
        {
            CharButton cb = Array.Find(equipment, x => x.name == equipmentData.MyType);

            cb.EquipArmor(Array.Find(items, x => x.MyTitle == equipmentData.MyTitle) as Armor);
        }
    }

    public void LoadActionButtons(SaveData data)
    {
        foreach (ActionButtonData buttonData in data.MyActionButtonData)
        {
            if (buttonData.IsItem)
            {
                actionButtons[buttonData.MyIndex].SetUseable(InventoryScript.MyInstance.GetUseable(buttonData.MyAction));
            }
            else
            {
                actionButtons[buttonData.MyIndex].SetUseable(SpellBook.MyInstance.GetSpell(buttonData.MyAction));
            }
        }
    }
}
