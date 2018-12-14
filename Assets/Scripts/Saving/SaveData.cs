using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class SaveData
{
    public PlayerData MyPlayerData { get; set; }

    public List<ChestData> MyChestData { get; set; }

    public List<EquipmentData> MyEquipmentData { get; set; }

    public InventoryData MyInventoryData { get; set; }

    public SaveData()
    {
        MyInventoryData = new InventoryData();
        MyChestData = new List<ChestData>();
        MyEquipmentData = new List<EquipmentData>();
    }
}

[Serializable]
public class PlayerData
{
    public int MyLevel { get; set; }

    public float MyXp { get; set; }

    public float MyMaxXP { get; set; }

    public float MyHealth { get; set; }

    public float MyMaxHealth { get; set; }

    public float MyMana { get; set; }

    public float MyMaxMana { get; set; }

    public float MyX { get; set; }

    public float MyY { get; set; }

    public PlayerData(int level, float xp, float maxXp, float health, float maxHealth, float mana, float maxMana, Vector2 position)
    {
        this.MyLevel = level;
        this.MyXp = xp;
        this.MyMaxXP = maxXp;
        this.MyHealth = health;
        this.MyMaxHealth = maxHealth;
        this.MyMana = mana;
        this.MyMaxMana = maxMana;
        this.MyX = position.x;
        this.MyY = position.y;

    }
}

[Serializable]
public class ItemData
{
    public string MyTitel { get; set; }

    public int MyStackCount { get; set; }

    public int MySlotIndex { get; set; }

    public ItemData(string titel, int stackCount = 0, int slotIndex = 0)
    {
        MyTitel = titel;
        MyStackCount = stackCount;
        MySlotIndex = slotIndex;
    }
}

[Serializable]
public class ChestData
{
    public string MyName { get; set; }

    public List<ItemData> MyItems { get; set; }

    public ChestData(string name)
    {
        MyName = name;

        MyItems = new List<ItemData>();
    }
}

[Serializable]
public class InventoryData
{
    public List<BagData> MyBags { get; set; }

    public InventoryData()
    {
        MyBags = new List<BagData>();
    }
}

[Serializable]
public class BagData
{
    public int MySlotCount { get; set; }
    public int MyBagIndex { get; set; }

    public BagData(int count, int index)
    {
        MySlotCount = count;
        MyBagIndex = index;

    }
}

[Serializable]
public class EquipmentData
{
    public string  MyTitle { get; set; }

    public string MyType { get; set; }

    public EquipmentData(string title, string type)
    {
        MyTitle = title;
        MyType = type;
    }

}
