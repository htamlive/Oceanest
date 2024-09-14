using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicTheme : MonoBehaviour
{
    public AudioSource music;

    public static MusicTheme Instance;



    // Start is called before the first frame update
    void Start()
    {
        music = GetComponent<AudioSource>();
        music.Play();   
        music.loop = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
