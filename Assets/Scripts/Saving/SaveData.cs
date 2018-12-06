using System;
using UnityEngine;

[Serializable]
public class SaveData
{
    public PlayerData MyPlayerData { get; set; }

    public SaveData()
    {

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

    public PlayerData(int level, float xp, float maxXp, float health, float maxHealth,float mana, float maxMana, Vector2 position)
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
