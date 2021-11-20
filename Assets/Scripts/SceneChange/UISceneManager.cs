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
        if ((SceneManager.GetActiveScene().name.Equals("HumanWin") || SceneManager.GetActiveScene().name.Equals("AlienWin")) && GameManager.globalManager.GetRounds().Count >= (int)GameManager.globalManager.gameMode)
        {
            SceneManager.LoadScene("FinalResults");
        } else
        {
            SceneManager.LoadScene("BuyingMenu");
        }
    }
    public void PlayerTurn()
    {
        SceneManager.LoadScene("PlayerSwitch");
    }
}
