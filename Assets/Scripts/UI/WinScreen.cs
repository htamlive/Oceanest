using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinScreen : MonoBehaviour
{
    [SerializeField] AudioClip openSound;
    [SerializeField] Animator winTextAnimator;

    // Use this for initialization
    void OnEnable()
    {
        StartCoroutine(
                //Cursor.visible = true;
                ShowUp());
    }

    private IEnumerator ShowUp()
    {
        //GameManager.Instance.audioSource.PlayOneShot(openSound);
        yield return new WaitForSeconds(.5f);
        winTextAnimator.Play("TextShowUp");
        yield return new WaitForSeconds(1f);
        Time.timeScale = 0f;

        
    }

    public void Quit()
    {
        Time.timeScale = 1f;
        GameDataManager.ResetData();
        SceneManager.LoadScene("Menu");
    }


}
