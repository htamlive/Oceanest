using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


[Serializable]
public enum PlayMode
{
    OnePlayer,
    TwoPlayer
}

public class MenuHandler : MonoBehaviour {

	[SerializeField] private string playScreen;

    public GameObject continueButton;
    public GameObject playmodePanel;

    public GameObject OptionPanelMobile;
    public GameObject OptionPanelPC;


    private void Start()
    {
        SetOnePlayer();

        playmodePanel.SetActive(!CheckPlatform(RuntimePlatform.Android));
        OptionPanelMobile.SetActive(CheckPlatform(RuntimePlatform.Android) || CheckPlatform(RuntimePlatform.WebGLPlayer));
        OptionPanelPC.SetActive(!CheckPlatform(RuntimePlatform.Android) && !CheckPlatform(RuntimePlatform.WebGLPlayer));

    }

    private bool CheckPlatform(RuntimePlatform platform)
    {
        return Application.platform == platform;
    }


    public void QuitGame()
    {
        Application.Quit();
    }

    public void LoadScene()
    {
        SceneManager.LoadScene(playScreen);
    }

    public void NewGame()
    {
        //GameDataManager.ResetData();
        LoadScene();
    }

    public void ContinueGame()
    {
        LoadScene();
    }

    public void LoadGame()
    {
        //if (GameDataManager.LoadStenoData())
        //{

        //    LoadScene();
        //}
    }

    private void UpdateGameMode(int playerCount)
    {
        PlayerPrefs.SetInt("PlayerCount", playerCount);
        
        //if (continueButton)
        //{
        //    continueButton.SetActive(GameDataManager.ReloadData());
        //}
    }

    public void SetTwoPlayer()
    {
        Debug.Log("SetTwoPlayer");
        UpdateGameMode(2);
    }

    public void SetOnePlayer()
    {
        UpdateGameMode(1);
    }

    public void TestLoadFromImage()
    {

        //string filePath = "D:/Desktop/screenshot.png";
        //CompositeGameData gameData = SteganographyScreenshot.ExtractGameDataFromImage(filePath);
        //gameData.itemsShopData.purchasedItemsIndexes.ForEach(i => Debug.Log("Purchased item: " + i));
    }

}
