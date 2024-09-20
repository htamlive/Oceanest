using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneLoader : MonoBehaviour
{
    private static SceneLoader instance;

    public static SceneLoader Instance { 
        get
        {
            if(instance == null)
                instance = instance = GameObject.FindObjectOfType<SceneLoader>();
            return instance;
        }
    }

    public GameObject loadingScreen;
    public Slider slider;
    public TMP_Text progressText;
    [SerializeField] private float smoothFactor = 5f;
    [SerializeField] private float progressBarWidthEased;

    public void LoadLevel(string sceneName)
    {
        //Debug.LogError(sceneName);
        StartCoroutine(LoadAsynchronously(sceneName));
    }

    IEnumerator LoadAsynchronously(string sceneName)
    {
        
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneName);

        loadingScreen.SetActive(true);
        progressBarWidthEased = 0;
        //operation.allowSceneActivation = false;
        while (!operation.isDone)
        {

            Debug.Log(operation.progress);
            float progress = Mathf.Clamp01(operation.progress / 0.9f);



            //progressBarWidthEased += (progress - progressBarWidthEased) * Time.deltaTime * smoothFactor;

            slider.value = progress;

            progressText.text = (progress * 100f).ToString("F0") + "%";

            //if(progressBarWidthEased >= 0.8)
            //{
            //    operation.
            //    break;
            //}

            yield return null;


        }

        progressText.text = "Initializing ...";

        yield return new WaitForEndOfFrame();
    }
}
