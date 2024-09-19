﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/*This script can be used on pretty much any gameObject. It provides several functions that can be called with 
animation events in the animation window.*/

public class AnimatorFunctions : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private ParticleSystem particleSystem;
    [SerializeField] private Animator setBoolInAnimator;

    // If we don't specify what audio source to play sounds through, just use the one on player.
    void Start()
    {
        //if (audioSource != null) audioSource = PlayerControl.Instance.audioSource;
    }

    //Hide and unhide the player


    //Play a sound through the specified audioSource
    void PlaySound(AudioClip? whichSound)
    {
        if (whichSound != null)
        {
            audioSource.PlayOneShot(whichSound);
        } else
        {
            Debug.Log("which Sound is null");
        }
    }

    public void EmitParticles(int amount)
    {
        particleSystem.Emit(amount);
    }


    public void SetTimeScale(float time)
    {
        Time.timeScale = time;
    }

    public void SetAnimBoolToFalse(string boolName)
    {
        setBoolInAnimator.SetBool(boolName, false);
    }

    public void SetAnimBoolToTrue(string boolName)
    {
        setBoolInAnimator.SetBool(boolName, true);
    }

    public void FadeOutMusic()
    {
       //GameManager.Instance.gameMusic.GetComponent<AudioTrigger>().maxVolume = 0f;
    }

    public void LoadScene(string whichLevel)
    {
        SceneManager.LoadScene(whichLevel);
    }

    //Slow down or speed up the game's time scale!
    public void SetTimeScaleTo(float timeScale)
    {
        Time.timeScale = timeScale;
    }
}
    