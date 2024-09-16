using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SettingHandler : MonoBehaviour
{
    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void quitSetting()
    {
        SceneManager.LoadScene("Menu");
    }
}
