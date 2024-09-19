using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManagement : MonoBehaviour
{
    public Camera overlayCam;

    // Start is called before the first frame update
    void Start()
    {
        overlayCam.gameObject.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
