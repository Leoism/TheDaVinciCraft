using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public Animator musicAnim;
    private float waitTime = 1.5f;
    public void ExitButton()
    {
        Application.Quit();
        Debug.Log("Game Close");
    }

    public void GameStart()
    {
        // SceneManager.LoadScene("PlayerSetting");
        StartCoroutine(ChangeScene());
    }

    IEnumerator ChangeScene()
    {
        musicAnim.SetTrigger("fadeOut");
        yield return new WaitForSeconds(waitTime);
        SceneManager.LoadScene("PlayerSetting");
    }

}
