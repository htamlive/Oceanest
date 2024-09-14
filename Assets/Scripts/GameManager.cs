using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;
    public GameObject winScreen;
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
    }

    internal void WinGame()
    {

        winScreen.SetActive(true);
    }
}
