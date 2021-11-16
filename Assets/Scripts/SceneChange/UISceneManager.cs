using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UISceneManager : MonoBehaviour
{
    public void MainMenu()
    {
        SceneManager.LoadScene("GameMenu");
    }
     public void BuyMenu()
    {
        SceneManager.LoadScene("BuyingMenu");
    }
    public void PlayerTurn()
    {
        SceneManager.LoadScene("PlayerSwitch");
    }
}
