using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogueInteractable : MonoBehaviour
{
    private Submarine submarine;
    private void Start()
    {
        submarine = gameObject.GetComponent<Submarine>();

    }
    public void Freeze(bool freeze) 
    {
        submarine.Freeze();
        Debug.Log("Freeze: " + freeze);
    }
}
