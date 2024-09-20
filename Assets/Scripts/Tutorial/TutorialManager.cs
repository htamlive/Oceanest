using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TutorialManager : MonoBehaviour
{
    public GameObject[] popUps;
    [SerializeField] private int popUpIndex;
    private bool finished = false;
    private float timer = 5.0f;
    private List<int> indexes;

    private List<int> TutorialIndexes()
    {

        return new List<int> { 0, 1, 2, 3, 4, 5};
    }

    private void Start()
    {
        indexes = TutorialIndexes();

        UpdatePopUp();
    }

    private void UpdatePopUp()
    {
        for (int i = 0; i < popUps.Length; i++)
        {
            if (popUpIndex < indexes.Count && i == indexes[popUpIndex])
            {
                popUps[i].SetActive(true);
            }
            else
            {
                popUps[i].SetActive(false);
            }
        }
    }

    private void CheckPlayerInputMatchScene(int index, bool matchInput)
    {
        if (popUpIndex < indexes.Count && indexes[popUpIndex] == index)
        {
            if (matchInput)
            {
                WaitToTry();

            }
            CheckUpdateScene();
        }
    }

    private void Update()
    {
        timer -= Time.deltaTime;


        CheckPlayerInputMatchScene(0, CheckPlayerMovement());
        CheckPlayerInputMatchScene(1, CheckPlayerDive());
        CheckPlayerInputMatchScene(2, CheckPlayerControllSpeed());
        CheckPlayerInputMatchScene(3, CheckPlayerAttack());
        CheckPlayerInputMatchScene(4, CheckPlayerOpenShop());

    }

    private static bool CheckPlayerOpenShop()
    {
        return (Input.GetKeyDown(KeyCode.I));
    }

    private static bool CheckPlayer2Attack()
    {
        return (Input.GetKeyDown(KeyCode.Keypad2));
    }

    private static bool CheckPlayerAttack()
    {
        return (Input.GetKeyDown(KeyCode.J));
    }

    private static bool CheckPlayerControllSpeed()
    {
        return (Input.GetKeyDown(KeyCode.Q) || Input.GetKeyDown(KeyCode.E));
    }

    private static bool CheckPlayerDive()
    {
        return (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.S));
    }

    private static bool CheckPlayer2Movement()
    {
        return (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.UpArrow));
    }

    private static bool CheckPlayerMovement()
    {
        return (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.D));
    }

    private void WaitToTry()
    {
        if (!finished)
        {
            timer = 5.0f;
        }
        finished = true;
        
    }

    private void CheckUpdateScene()
    {
        if (finished && timer <= 0)
        {
            popUpIndex++;

            UpdatePopUp();
            finished = false;
            timer = 5.0f;
        }
    }

    public void ReturnToMenu()
    {
        SceneLoader.Instance.LoadLevel("Menu");
    }
}
