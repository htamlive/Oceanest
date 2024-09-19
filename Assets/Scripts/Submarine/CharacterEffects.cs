using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class CharacterEffects : MonoBehaviour
{


    [Header("Effects")]
    public CameraEffects cameraEffects;
    public AudioSource SFXAudio;
    public AudioSource moveSoundAudio;


    private Rigidbody body;
    private SpriteRenderer sprite;



    // Start is called before the first frame update
    void Start()
    {
        body = GetComponent<Rigidbody>();
        sprite = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void StopSound()
    {
        moveSoundAudio.enabled = false;
    }

    public void SelectMoveSound()
    {
        moveSoundAudio.enabled = true;
    }



    public void DamageEffect()
    {
        //SFXAudio.PlayOneShot(sounds.damageSound.clip, sounds.damageSound.volume);
        StartCoroutine(FreezeFrameEffect());
        cameraEffects.Shake(100, 1f);
    }

    public void DieEffect()
    {
        //SFXAudio.PlayOneShot(sounds.deathSound.clip, sounds.deathSound.volume);
        //GameManager.Instance.audioSource.PlayOneShot(sounds.deathSound.clip, sounds.deathSound.volume);
        StartCoroutine(ShakeEffect());
        //StartCoroutine(FadeOutAndDisableEffect());
    }

    private IEnumerator ShakeEffect()
    {
        float shakeStartTime = Time.time;
        float shakeDuration = 3f;
        Vector3 lastPosition = body.transform.position;
        while (Time.time - shakeStartTime < shakeDuration)
        {
            float range = 0.1f;
            Vector2 dir = UnityEngine.Random.insideUnitCircle * range;
            //Debug.Log(dir);
            body.transform.position = lastPosition + new Vector3(dir.x, dir.y, 0);
            yield return null;
        }
        yield return null;
    }

    public void AttackEffect()
    {
        Debug.Log("AttackEffect");
        body.AddForce(body.transform.forward * (-1000) + body.transform.up * 100, ForceMode.Impulse);
    }

    //private IEnumerator FadeOutAndDisableEffect()
    //{
    //    float fadeStartTime = Time.time;
    //    float fadeDelay = 1f;
    //    float fadeDuration = 2f;
    //    yield return new WaitForSeconds(fadeDelay);
    //    while (Time.time - fadeStartTime < fadeDuration)
    //    {
    //        float alpha = Mathf.Lerp(1, 0, (Time.time - fadeStartTime) / fadeDuration);
    //        sprite.color = new Color(sprite.color.r, sprite.color.g, sprite.color.b, alpha);
    //        yield return null;
    //    }
    //    //player.gameObject.SetActive(false);
    //    yield return null;
    //}




    //public void AttackEffect()
    //{
    //    SFXAudio.PlayOneShot(sounds.attackSound.clip, sounds.attackSound.volume);
    //    cameraEffects.Shake(100, 1f);
    //}


    public IEnumerator FreezeFrameEffect(float length = .007f)
    {
        Time.timeScale = .1f;
        yield return new WaitForSeconds(length);
        Time.timeScale = 1f;
    }
}
