using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonWithCircle : MonoBehaviour
{
    public GameObject circle;

    public void Deactivate() { 
        circle.SetActive(false);
    }
}
