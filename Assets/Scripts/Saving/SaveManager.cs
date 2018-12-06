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

    // Use this for initialization
    void Awake()
    {
        chests = FindObjectsOfType<Chest>();

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

            SavePlayer(data);

            SaveChests(data);

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


    private void Load()
    {
        try
        {
            BinaryFormatter bf = new BinaryFormatter();

            FileStream file = File.Open(Application.persistentDataPath + "/" + "SaveTest.dat", FileMode.Open);

            SaveData data = (SaveData)bf.Deserialize(file);

            file.Close();

            LoadPlayer(data);

            LoadChests(data);


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
}
