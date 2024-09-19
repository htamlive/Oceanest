using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossGate : MonoBehaviour
{
    public GameObject bossHealth;
   
    private void OnTriggerExit(Collider other)
    {
        Debug.Log(other.gameObject.layer);
        Debug.Log(LayerMask.NameToLayer("Player"));
        if(other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            var status = bossHealth.activeInHierarchy;
            bossHealth.gameObject.SetActive(!status);
        }
    }
}
