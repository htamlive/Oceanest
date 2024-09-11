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
    //    //public static InventoryData inventoryData;
    //    //public static InventoryData storageInventoryData;
    //    //public static PlayerPositionOnGrid playerPositionOnGrid;
    //    public static String currentSceneName;

    //    //static readonly ShoppingItem selectedItem;

    //    static GameDataManager()
    //    {
    //        ReloadData();
    //    }


    //    //Player Data Methods -----------------------------------------------------------------------------
    //    //public static ShoppingItem GetSelectedItem()
    //    //{
    //    //    return selectedItem;
    //    //}

    //    //public static void SetSelectedItem(ShoppingItem item, int index)
    //    //{
    //    //    selectedItem = item;
    //    //    playerGamePlayData.selectedCharacterIndex = index;
    //    //    SavePlayerData();
    //    //}

    //    //public static int GetSelectedCharacterIndex()
    //    //{
    //    //    return playerGamePlayData.selectedCharacterIndex;
    //    //}

    public static int GetCoins()
    {
        return gameData.playerData.coins;
    }

    public static void AddCoins(int amount)
    {

        gameData.playerData.coins += amount;
        SaveData();
    }

    //    public static bool CanSpendCoins(int amount, int playerIndex = 0)
    //    {
    //        return (PlayersGamePlayData[playerIndex].coins >= amount);
    //    }


    //    public static void SpendCoins(int amount, int playerIndex = 0)
    //    {
    //        PlayersGamePlayData[playerIndex].coins -= amount;
    //        SavePlayerData();
    //    }

    //    public static void AddHealth(int amount, int playerIndex = 0)
    //    {

    //        PlayersGamePlayData[playerIndex].health = Mathf.Clamp(PlayersGamePlayData[playerIndex].health + amount, 0, 100);
    //        PlayerControl.GetInstanceAtIndex(playerIndex).health = PlayersGamePlayData[playerIndex].health;
    //        //Debug.Log("Health: " + PlayersGamePlayData[playerIndex].health);
    //        SavePlayerData();
    //    }


    //    public static void HealPlayer(int amount, int playerIndex = 0)
    //    {
    //        AddHealth(amount, playerIndex);
    //        PlayerControl.GetInstanceAtIndex(playerIndex).effects.HealEffect();

    //    }

    //    public static void SetHealth(int amount, int playerIndex = 0)
    //    {
    //        PlayersGamePlayData[playerIndex].health = Mathf.Clamp(amount, 0, 100);
    //        SavePlayerData();
    //    }

    //    public static int GetHealth(int playerIndex = 0)
    //    {
    //        return PlayersGamePlayData[playerIndex].health;
    //    }

    //    public static bool Dash(int playerIndex = 0)
    //    {
    //        return PlayersGamePlayData[playerIndex].dash;
    //    }

    //    public static bool DoubleJump(int playerIndex = 0)
    //    {
    //        return PlayersGamePlayData[playerIndex].doubleJump;
    //    }

    //    public static void AchieveDash(bool achieve)
    //    {
    //        int playerCount = PlayerPrefs.GetInt("PlayerCount", 1);
    //        for (int i = 0; i < playerCount; i++)
    //        {
    //            PlayersGamePlayData[i].dash = achieve;
    //            PlayerControl.GetInstanceAtIndex(i).skills.CanDash = achieve;
    //        }
    //        SavePlayerData();
    //    }

    //    public static void AchieveDoubleJump(bool achieve)
    //    {
    //        int playerCount = PlayerPrefs.GetInt("PlayerCount", 1);
    //        for (int i = 0; i < playerCount; i++)
    //        {
    //            PlayersGamePlayData[i].doubleJump = achieve;
    //            PlayerControl.GetInstanceAtIndex(i).skills.CanDoubleJump = achieve;
    //        }
    //        SavePlayerData();
    //    }

    //    static string GetPlayerDataFileName(int playerIndex, int playerCount)
    //    {
    //        return "player" + playerIndex + "-playmode" + playerCount + "-data.txt";
    //    }

    //    static string GetShopDataFileName(int playerCount)
    //    {
    //        return "shop-playmode" + playerCount + "-data.txt";
    //    }

    //    static string GetInventoryDataFileName(int playerCount)
    //    {
    //        return "inventoryData-playmode" + playerCount + ".txt";
    //    }

    //    static string GetStorageInventoryDataFileName(int playerCount)
    //    {
    //        return "storageInventoryData-playmode" + playerCount + ".txt";
    //    }

    //    static string GetPlayerPositionOnGridFileName(int playerCount)
    //    {
    //        return "playerPositionOnGrid-playmode" + playerCount + ".txt";
    //    }

    //    static public bool ReloadData()
    //    {

    //        return LoadPlayerData() && LoadItemsShopData() && LoadMapManagerData();
    //    }



    //    static public void ResetData()
    //    {
    //        int playerCount = PlayerPrefs.GetInt("PlayerCount", 1);
    //        PlayersGamePlayData = new(playerCount);
    //        for (int i = 0; i < playerCount; i++)
    //        {
    //            PlayersGamePlayData.Add(new PlayerGamePlayData());
    //        }
    //        SavePlayerData();

    //        itemsShopData = new();
    //        SaveItemsShopData();

    //        //playerPositionOnGrid.playerPosition = new(0, 0);
    //        //playerPositionOnGrid.cameFromDirection = DoorInfo.Direction.Down;

    //        inventoryData = null;
    //        playerPositionOnGrid = null;
    //        currentSceneName = null;
    //        storageInventoryData = null;

    //        SaveMapManagerData(inventoryData, storageInventoryData, playerPositionOnGrid, currentSceneName);

    //    }

    //    public static void SaveMapManagerData(InventoryData inventoryData, InventoryData storageInventoryData, PlayerPositionOnGrid playerPositionOnGrid, string currentSceneName)
    //    {
    //        int playerCount = PlayerPrefs.GetInt("PlayerCount", 1);
    //        if(inventoryData != null)
    //        {
    //            GameDataManager.inventoryData = inventoryData;
    //            Utilities.SaveSerializedObject(GetInventoryDataFileName(playerCount), inventoryData);
    //        }
    //        if(storageInventoryData != null)
    //        {
    //            GameDataManager.storageInventoryData = storageInventoryData;
    //            Utilities.SaveSerializedObject(GetStorageInventoryDataFileName(playerCount), storageInventoryData);
    //        }
    //        if(playerPositionOnGrid != null)
    //        {
    //            Debug.Log("Save position: " + playerPositionOnGrid.playerPosition);
    //            GameDataManager.playerPositionOnGrid = playerPositionOnGrid;
    //            Utilities.SaveSerializedObject(GetPlayerPositionOnGridFileName(playerCount), playerPositionOnGrid);
    //        }
    //        if(currentSceneName != null && currentSceneName != "")
    //        {
    //            GameDataManager.currentSceneName = currentSceneName;
    //            Utilities.SaveSerializedObject(GetCurrentSceneFileName(playerCount), currentSceneName);
    //        }
    //    }

    //    private static string GetCurrentSceneFileName(int playerCount)
    //    {
    //        return "currentScene-playmode" + playerCount + ".txt";
    //    }

    //    static bool LoadMapManagerData()
    //    {
    //        bool loadSuccess = true;
    //        int playerCount = PlayerPrefs.GetInt("PlayerCount", 1);
    //        inventoryData = null;
    //        playerPositionOnGrid = null;

    //        if (Utilities.TryLoadSerializedObject(GetInventoryDataFileName(playerCount), out var outInventoryData))
    //        {
    //            inventoryData = outInventoryData as InventoryData;
    //        } else
    //        {
    //            loadSuccess = false;
    //        }
    //        if (Utilities.TryLoadSerializedObject(GetPlayerPositionOnGridFileName(playerCount), out var outPlayerPositionOnGrid))
    //        {
    //            playerPositionOnGrid = outPlayerPositionOnGrid as PlayerPositionOnGrid;
    //        } else
    //        {
    //            loadSuccess = false;
    //        }
    //        if (Utilities.TryLoadSerializedObject(GetStorageInventoryDataFileName(playerCount), out var outStorageInventoryData))
    //        {
    //            storageInventoryData = outStorageInventoryData as InventoryData;
    //        } else
    //        {
    //            loadSuccess = false;
    //        }
    //        if (Utilities.TryLoadSerializedObject(GetCurrentSceneFileName(playerCount), out var outCurrentSceneName))
    //        {
    //            currentSceneName = outCurrentSceneName as string;
    //        } else
    //        {
    //            loadSuccess = false;
    //        }
    //        return loadSuccess;
    //    }

    //    static bool LoadPlayerData()
    //    {
    //        int playerCount = PlayerPrefs.GetInt("PlayerCount", 1);
    //        PlayersGamePlayData = new List<PlayerGamePlayData>(playerCount); // Ensure type is List<PlayerGamePlayData>

    //        bool loadPlayerDataSuccess = true;

    //        for (int i = 0; i < playerCount; i++)
    //        {
    //            if (Utilities.TryLoadSerializedObject(GetPlayerDataFileName(i, playerCount), out object obj))
    //            {
    //                if (obj is PlayerGamePlayData playerData)
    //                {
    //                    PlayersGamePlayData.Add(playerData);
    //                }
    //                else
    //                {
    //                    PlayersGamePlayData.Add(new PlayerGamePlayData());
    //                    loadPlayerDataSuccess = false;
    //                }
    //            }
    //            else
    //            {
    //                PlayersGamePlayData.Add(new PlayerGamePlayData());
    //                loadPlayerDataSuccess = false;
    //            }
    //            Debug.Log("i: " + i + " PlayerCount: " + playerCount + " PlayerData: " + PlayersGamePlayData.Count);
    //        }

    //        Debug.Log("Count: " + PlayersGamePlayData.Count + " PlayerCount: " + playerCount);





    //        Debug.Log("<color=green>[PlayerData] Loaded.</color>");
    //        return loadPlayerDataSuccess;
    //    }



    static void SaveData()
    {
        Utilities.SaveSerializedObject("gamedata.txt", gameData);
        Debug.Log("<color=magenta>[GameData] Saved.</color>");
    }

    //    public static void AddPurchasedCharacter(int itemIndex)
    //    {
    //        itemsShopData.purchasedItemsIndexes.Add(itemIndex);
    //        SaveItemsShopData();
    //    }

    //    public static List<int> GetAllPurchasedItem()
    //    {
    //        return itemsShopData.purchasedItemsIndexes;
    //    }

    //    public static int GetPurchasedItem(int index)
    //    {
    //        return itemsShopData.purchasedItemsIndexes[index];
    //    }

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
            return false;
        }

        return true;
    }
}