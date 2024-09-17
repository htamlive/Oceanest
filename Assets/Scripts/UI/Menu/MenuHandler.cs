using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuHandler : MonoBehaviour {

	[SerializeField] private string playScreen;


    public List<ButtonWithCircle> buttonWithCircles;

    public void ResetButtonsStatus()
    {
        for (int i = 0; i < buttonWithCircles.Count; i++)
        {
            buttonWithCircles[i].Deactivate();
        }

    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void LoadScene()
    {
        SceneManager.LoadScene(playScreen);
    }

    public void EnterTutorial()
    {
        GameDataManager.EnterTutorialMode();
        SceneManager.LoadScene("Tutorial");
    }

    public void NewGame()
    {
        GameDataManager.ResetData();
        LoadScene();
    }

    public void ContinueGame()
    {
        GameDataManager.LoadData();
        LoadScene();
    }

    public void LoadGame()
    {
        if (GameDataManager.LoadStenoData())
        {
            LoadScene();
        }
    }

    public void TestLoadFromImage()
    {

        //string filePath = "D:/Desktop/screenshot.png";
        //CompositeGameData gameData = SteganographyScreenshot.ExtractGameDataFromImage(filePath);
        //gameData.itemsShopData.purchasedItemsIndexes.ForEach(i => Debug.Log("Purchased item: " + i));
    }

}
