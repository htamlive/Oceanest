using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoseScreen : MonoBehaviour
{
    [SerializeField] AudioClip openSound;
    [SerializeField] Animator textAnimator;

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
        textAnimator.Play("TextShowUp");
        yield return new WaitForSeconds(1f);
        Time.timeScale = 0f;


    }

    public void Quit()
    {
        Time.timeScale = 1f;
        GameDataManager.ResetData();
        SceneManager.LoadScene("Menu");
    }

    public void Replay()
    {
        Time.timeScale = 1f;
        GameDataManager.ResetData();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }


}
