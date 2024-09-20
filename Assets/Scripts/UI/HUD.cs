using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

/*Manages and updates the HUD, which contains your health bar, coins, etc*/

public class HUD : MonoBehaviour
{
    [Header ("Reference")]
    public Animator animator;
    [SerializeField] private GameObject ammoBar;
    public TextMeshProUGUI coinsMesh;
    [SerializeField] private List<HealthBarUI> healthBarPlayers;
    [SerializeField] float smoothFactor = 5f;

    private float ammoBarWidth;
    private float ammoBarWidthEased; //Easing variables slowly ease towards a number
    [System.NonSerialized] public Sprite blankUI; //The sprite that is shown in the UI when you don't have any items
    //private float coins;
    //private float coinsEased;
    [System.NonSerialized] public string loadSceneName;
    [System.NonSerialized] public bool resetPlayer;


    void Start()
    {

        ammoBarWidth = 1;
        ammoBarWidthEased = ammoBarWidth;
        //coins = (float)PlayerControl.Instance.coins;
        //coinsEased = coins;
    }

    void Update()
    {
        //Update coins text mesh to reflect how many coins the player has! However, we want them to count up.
        //coinsMesh.text = Mathf.Round(coinsEased).ToString();
        //coinsEased += ((float)PlayerControl.Instance.coins - coinsEased) * Time.deltaTime * 5f;

        //if (coinsEased >= coins)
        //{
        //    animator.SetTrigger("getGem");
        //    coins = coinsEased + 1;
        //}
        //int playerCount = PlayerPrefs.GetInt("PlayerCount", 1);
        for (int i = 0; i < healthBarPlayers.Count; i++)
        {
            healthBarPlayers[i].UpdateHealthBar();
        }

        //if (ammoBar)
        //{
        //    ammoBarWidth = (float)PlayerControl.Instance.ammo / (float)PlayerControl.Instance.maxAmmo;
        //    ammoBarWidthEased += (ammoBarWidth - ammoBarWidthEased) * Time.deltaTime * ammoBarWidthEased;
        //    ammoBar.transform.localScale = new Vector2(ammoBarWidthEased, transform.localScale.y);
        //}

    }



    public void HealthBarHurt()
    {
        animator.SetTrigger("hurt");
    }

    void ResetScene()
    {
        SceneLoader.Instance.LoadLevel(loadSceneName);
        //GameDataManager.ResetHealth();
        GameDataManager.ResetData();
    }



}
