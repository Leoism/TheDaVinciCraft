using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameSettingMenu : MonoBehaviour
{
    public void Play()
    {
        SceneManager.LoadScene("BuyingMenu");
    }
    public void Home()
    {
        SceneManager.LoadScene("GameMenu");
    }

}
