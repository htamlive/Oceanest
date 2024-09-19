using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/*Triggers a dialogue conversation, passing unique commands and information to the dialogue box and inventory system for fetch quests, etc.*/

public class DialogueTrigger : MonoBehaviour
{

    [Header ("References")]
    public DialogueInteractable player;
    [SerializeField] private GameObject finishTalkingActivateObject; //After completing a conversation, an object can activate. 
    [SerializeField] private Animator iconAnimator; 

    [Header("Trigger")]
    [SerializeField] private bool autoHit; //Does the player need to press the interact button, or will it simply fire automatically?
    public bool completed;
    [SerializeField] private bool repeat; //Set to true if the player should be able to talk again and again to the NPC. 
    [SerializeField] private bool sleeping;

    [Header ("Dialogue")]
    [SerializeField] private string characterName; //The character's name shown in the dialogue UI
    [SerializeField] private string dialogueStringA; //The dialogue string that occurs before the fetch quest
    [SerializeField] private string dialogueStringB; //The dialogue string that occurs after fetch quest
    [SerializeField] private AudioClip[] audioLinesA; //The audio lines that occurs before the fetch quest
    [SerializeField] private AudioClip[] audioLinesB; //The audio lines that occur after the fetch quest
    [SerializeField] private AudioClip[] audioChoices; //The audio lines that occur when selecting an audio choice

    [Header ("Fetch Quest")]
    [SerializeField] private GameObject deleteGameObject; //If an NPC is holding the object, and gives it to you, this object will destroy
    [SerializeField] private string getWhichItem; //The inventory item given if items is fetched
    [SerializeField] private int getCoinAmount; //Or the amount of coins given if item is fetched
    [SerializeField] private string finishTalkingAnimatorBool; //After completing a conversation, an animation can be fired
    [SerializeField] private string finishTalkingActivateObjectString; //After completing a conversation, an object's name can be searched for and activated.
    [SerializeField] private Sprite getItemSprite; //The sprite of the inventory item given, shown in HUD
    [SerializeField] private AudioClip getSound; //When the player is given an object, this sound will play
    [SerializeField] private bool instantGet; //Player can be immediately given an item the moment the conversation begins
    [SerializeField] private string requiredItem; //The required fetch quest item
    [SerializeField] private int requiredCoins; //Or the required coins (cannot require both an item and coins)
    public Animator useItemAnimator; //If the player uses an item, like a key, an animator can be fired (ie to open a door)
    [SerializeField] private string useItemAnimatorBool; //An animator bool can be set to true once an item is used, like ae key.

    void OnTriggerStay(Collider col)
    {
        //Debug.Log("OnTriggerStay");
        if (col.gameObject == player.gameObject && !sleeping && !completed)
        {
            iconAnimator.SetBool("active", true);
            if (autoHit || (Input.GetAxis("Submit") > 0))
            {
                //Debug.Log("Submit");
                iconAnimator.SetBool("active", false);
                var gameStatus = GameDataManager.GetGameStatus();
                if (!gameStatus.bossDefeated)
                {
                    //Debug.Log("Not defeat");
                    if(!gameStatus.receivedSupport)
                        GameManager.Instance.dialogueBoxController.AppearV2("Greeting", characterName, this, false, finishTalkingAnimatorBool, GameDataManager.ReceiveSupportedCoins, "", repeat);
                    else
                        GameManager.Instance.dialogueBoxController.AppearV2("Advise", characterName, this, false, finishTalkingAnimatorBool, () => { }, "", repeat);
                }       
                else
                {
                    GameManager.Instance.dialogueBoxController.AppearV2("Thank you", characterName, this, false, finishTalkingAnimatorBool, GameManager.Instance.releaseWiningPoint, "", repeat);
                    //GameManager.Instance.dialogueBoxController.Appear(dialogueStringB, characterName, this, true, audioLinesB, audioChoices, "", null, "", repeat);
                }
                sleeping = true;
            }
        }
        else
        {
            iconAnimator.SetBool("active", false);
        }
    }


    void OnTriggerExit(Collider col)
    {
        Debug.Log("OnTriggerExit");
        if (col.gameObject == player.gameObject)
        {
            iconAnimator.SetBool("active", false);
            sleeping = completed;
        }
    }
}