using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*Used for coins, health, inventory items, and even ammo if you want to create a gun shooting mechanic!*/

public class Collectable : MonoBehaviour
{
    enum ItemType { LevelItem, InventoryItem, Coin, Health, Ammo, Treasure }; //Creates an ItemType category
    [SerializeField] ItemType itemType; //Allows us to select what type of item the gameObject is in the inspector
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip bounceSound;
    [SerializeField] private AudioClip[] collectSounds;
    [SerializeField] private int itemAmount;
    [SerializeField] public string itemName; //If an inventory item, what is its name?
    [SerializeField] private Sprite UIImage; //What image will be displayed if we collect an inventory item?

    public bool isCollected = false;
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void OnTriggerEnter(Collider col)
    {

        if (CheckIsPlayer(col))
        {
            Collect();
        } else if (col.gameObject.layer == 14)
        {
            Collect();
        }
    }

    private static bool CheckIsPlayer(Collider col)
    {
        //Debug.LogError(col.gameObject.name);
        return GameManager.Instance.CheckIsPlayer(col.gameObject);
    }

    public void Collect()
    {
        if (isCollected)
        {
            return;
        }
        isCollected = true;
//        else if (itemType == ItemType.Coin)
//        {
//            PlayerControl.Instance.coins += itemAmount;
//            GameDataManager.AddCoins(itemAmount);
//            GameSharedUI.Instance.UpdateCoinsUIText();

//#if UNITY_EDITOR
//            if (Input.GetKey(KeyCode.C))
//            {
//                GameDataManager.AddCoins(100);
//            }

//#endif


//        }
        //else if (itemType == ItemType.Health)
        //{
        //    if (PlayerControl.Instance.health < PlayerControl.Instance.maxHealth)
        //    {
        //        GameManager.Instance.hud.HealthBarHurt();
        //        PlayerControl.Instance.health += itemAmount;
        //    }
        //}
        if (itemType == ItemType.Treasure)
        {
            GameManager.Instance.WinGame();
        }

        if (collectSounds.Length > 0)
        {
            //GameManager.Instance.audioSource.PlayOneShot(collectSounds[Random.Range(0, collectSounds.Length)], Random.Range(.6f, 1f));
        }

        //PlayerControl.Instance.FlashEffect();


        //If my parent has an Ejector script, it means that my parent is actually what needs to be destroyed, along with me, once collected
        if (transform.parent.GetComponent<Ejector>() != null)
        {
            Destroy(transform.parent.gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

    }
}
