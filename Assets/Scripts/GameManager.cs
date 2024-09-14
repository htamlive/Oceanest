using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;
    public GameObject winScreen;
    public HUD hud;

    public List<SubmarineStat> submarineStats;

    public static GameManager Instance
    {
        get
        {
            if (instance == null) instance = GameObject.FindObjectOfType<GameManager>();
            return instance;
        }
    }

    public Camera camera;
    public GameObject pauseMenu;

    // Start is called before the first frame update
    void Start()
    {
        
    }



    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            pauseMenu.SetActive(true);
        }
        if (Input.GetKeyDown(KeyCode.Print) && !Utilities.CheckPlatform(RuntimePlatform.Android) && !Utilities.CheckPlatform(RuntimePlatform.WebGLPlayer))
        {
            Debug.Log("camera: " + camera);
            StartCoroutine(SteganographyScreenshot.CaptureAndEmbedData(camera));
        }
#if UNITY_EDITOR
        if (Input.GetKey(KeyCode.V))
        {
            WinGame();
        }
#endif
    }

    internal void WinGame()
    {

        winScreen.SetActive(true);
    }

    internal void ResetGamePlay()
    {
        StartCoroutine(DelayResetGamePlay());
    }

    public void SavePlayerData()
    {
        var playerData = GameDataManager.GetPlayerData();
        foreach (var item in submarineStats)
        {
            var gameObject = item.gameObject;

            if (gameObject != null)
            {
                GameDataManager.UpdatePlayerTransformAndSave(gameObject.transform); 
            }
        }

        
    }

    private IEnumerator DelayResetGamePlay()
    {
        Time.timeScale = .5f;
        yield return new WaitForSeconds(0.05f);
        Instance.hud.animator.SetTrigger("coverScreen");
        Instance.hud.loadSceneName = SceneManager.GetActiveScene().name;
        Time.timeScale = 1f;
    }
}
