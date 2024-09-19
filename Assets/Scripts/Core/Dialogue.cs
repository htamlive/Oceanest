using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*This script stores every dialogue conversation in a public Dictionary.*/

public class Dialogue : MonoBehaviour
{

    public Dictionary<string, string[]> dialogue = new Dictionary<string, string[]>();

    void Start()
    {
        //NPC
        dialogue.Add("Greeting", new string[] {
            "At last, the savior from the ancient prophecy has arrived. Welcome, brave adventurer. I have waited countless years for your coming.",
            "The only way to escape this cursed place is to defeat the ancient sea beast, Thal'Zaroth, whose shadow darkens these waters.",
            "Fear not, for I shall aid you in your quest.",
            "Take these 15 coins, use them to equip the skills you need to face Thal'Zaroth.",
            "Your destiny awaits, and with it, the hope of our freedom. Go forth, and may the sea favor your courage!",
        });

        //dialogue.Add("CharacterAChoice1", new string[] {
        //    "",
        //    "",
        //    "Let me go find some coins!",
        //});

        //dialogue.Add("CharacterAChoice2", new string[] {
        //    "",
        //    "",
        //    "What else can you do?"
        //});

        dialogue.Add("Advise", new string[] {
            "You return, yet Thal'Zaroth still stirs beneath the waves. The sea remains restless, and the curse lingers.",
            "But fear not, adventurer—such a foe is not easily vanquished.",
            "Take heart. Reflect on your skills, prepare yourself further.",
            "The beast’s defeat requires patience, strength, and wit. When you are ready once more, the sea will be waiting for your triumphant return."
        });

        dialogue.Add("Thank you", new string[] {
            "Thank you, brave adventurer, for vanquishing the wicked sea monster that cursed this place. Its dark magic had bound me for centuries, but now, thanks to your courage, I am free.",
            "In gratitude, I will cast a spell to release a magical sphere of immense power.",
            "That sphere can help you return home and only can appear once you return!",
            "May this gift aid you in your future quests. Farewell, and may the tides of fortune be ever in your favor!"
        });
    }
}
