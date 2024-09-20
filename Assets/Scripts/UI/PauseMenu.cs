using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/*Disables the cursor, freezes timeScale and contains functions that the pause menu button can use*/ 

public class PauseMenu : MonoBehaviour
{

    public List<ButtonWithCircle> buttonWithCircles;
    // Use this for initialization
    void OnEnable()
    {
        Time.timeScale = 0f;
    }

    public void Unpause()
    {
        for (int i = 0; i < buttonWithCircles.Count; i++)
        {
            buttonWithCircles[i].Deactivate();
        }
        //Cursor.visible = false;
        gameObject.SetActive(false);

        Time.timeScale = 1f;


    }

    public void Quit()
    {
        Time.timeScale = 1f;

        GameManager.Instance.SavePlayerData();

        SceneLoader.Instance.LoadLevel("Menu");
    }

    public void RestartLevel()
    {
        Time.timeScale = 1f;
        GameDataManager.ResetData();
        SceneLoader.Instance.LoadLevel(SceneManager.GetActiveScene().name);
    }
}
