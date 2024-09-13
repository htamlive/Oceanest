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



    private void Start()
    {
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
        SceneManager.LoadScene("Tutorial");
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
