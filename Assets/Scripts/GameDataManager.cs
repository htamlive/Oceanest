using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Xml.Serialization;
using UnityEngine.TextCore.Text;
using UnityEngine;
using System;
using UnityEditor;


[System.Serializable]
public class ItemsShopData
{
    public List<int> purchasedItemsIndexes = new();
}
[System.Serializable]
public class PlayerData
{
    public int coins = 0;
    public int health = 100;
    public int maxHealth = 100;
    public bool hasDoubleMissiles = false;
    public bool hasTrackingMissiles = false;
    public float[] position = { 0, 0, 0 };
    public float[] rotation = { 0, 0, 0 };
}

[System.Serializable]
public class GameData
{
    public ItemsShopData shopData = new();
    public PlayerData playerData = new();
}

public static class GameDataManager
{
    static GameData gameData = new();

    static readonly string GameDataPath = "GameData.txt";

    static public void EnterTutorialMode()
    {
        gameData = new();
    }
    public static int GetCoins()
    {
        return gameData.playerData.coins;
    }

    public static void AddCoins(int amount)
    {

        gameData.playerData.coins += amount;
        SaveData();
    }

    public static bool CanSpendCoins(int amount)
    {
        return (GetCoins() >= amount);
    }


    public static void SpendCoins(int amount, int playerIndex = 0)
    {
        gameData.playerData.coins -= amount;
        SaveData();
    }

    public static void AddHealth(int amount, int playerIndex = 0)
    {
        int health = gameData.playerData.health;

        health = Mathf.Clamp(health + amount, 0, 100);
        gameData.playerData.health = health;
        //Debug.Log("Health: " + PlayersGamePlayData[playerIndex].health);
        SaveData();
    }


    public static void HealPlayer(int amount, int playerIndex = 0)
    {
        AddHealth(amount, playerIndex);
        //PlayerControl.GetInstanceAtIndex(playerIndex).effects.HealEffect();

    }

    public static void SetHealth(int amount)
    {
        gameData.playerData.health = Mathf.Clamp(amount, 0, 100);
        SaveData();
    }

    public static int GetHealth()
    {
        return gameData.playerData.health;
    }

    public static PlayerData GetPlayerData()
    {
        return gameData.playerData;
    }

    public static bool DoubleMissilesStatus
    {
        set { gameData.playerData.hasDoubleMissiles = value; SaveData(); }
        get { return gameData.playerData.hasDoubleMissiles; }
    }

    public static bool TrackingMissilesStatus
    {
        set { gameData.playerData.hasTrackingMissiles = value; SaveData(); }
        get { return gameData.playerData.hasTrackingMissiles; }
    }

    static public void ResetData()
    {
        gameData = new();
        SaveData();
    }

    static void SaveData()
    {
        Utilities.SaveSerializedObject(GameDataPath, gameData);
        Debug.Log("<color=magenta>[GameData] Saved.</color>");
    }

    public static void AddPurchasedCharacter(int itemIndex)
    {
        gameData.shopData.purchasedItemsIndexes.Add(itemIndex);
        SaveData();
    }

    public static List<int> GetAllPurchasedItem()
    {
        return gameData.shopData.purchasedItemsIndexes;
    }

    public static int GetPurchasedItem(int index)
    {
        return gameData.shopData.purchasedItemsIndexes[index];
    }

    //    static bool LoadItemsShopData()
    //    {
    //        bool loadShoppingDataSuccess = false;
    //        int playerCount = PlayerPrefs.GetInt("PlayerCount", 1);
    //        if (Utilities.TryLoadSerializedObject(GetShopDataFileName(playerCount), out object obj))
    //        {
    //            itemsShopData = obj as ItemsShopData;
    //            loadShoppingDataSuccess = true;
    //        } 


    //        return loadShoppingDataSuccess;
    //    }

    //    static void SaveItemsShopData()
    //    {
    //        int playerCount = PlayerPrefs.GetInt("PlayerCount", 1);
    //        Utilities.SaveSerializedObject(GetShopDataFileName(playerCount), itemsShopData);
    //    }

    //    internal static CompositeGameData ComposeGameData()
    //    {
    //        CompositeGameData gameData = new()
    //        {
    //            m_Players = PlayersGamePlayData,
    //            itemsShopData = itemsShopData,
    //            inventoryData = inventoryData,
    //            playerPositionOnGrid = playerPositionOnGrid,
    //            currentSceneName = currentSceneName,
    //        };
    //        Debug.Log("Coin: " + PlayersGamePlayData[0].coins);
    //        return gameData;
    //    }

    public static bool LoadStenoData()
    {
        GameData gameData = SteganographyScreenshot.LoadData();
        if (gameData == null)
        {
            gameData= new GameData();
            return false;
        }

        return true;
    }

    public static bool LoadData()
    {
        if(Utilities.TryLoadSerializedObject(GameDataPath,out object result)){
            gameData = result as GameData;
            return true;
        }
        return false;
    }
}