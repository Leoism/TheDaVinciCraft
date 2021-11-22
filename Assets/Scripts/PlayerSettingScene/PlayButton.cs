using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayButton : MonoBehaviour
{
    // Start is called before the first frame update
    public Button playButton = null;
    public InputField humanInput = null;
    public InputField alienInput = null;

    void Start()
    {
        playButton.onClick.AddListener(() =>
        {
            if (humanInput.text.Trim().Equals("") || alienInput.text.Trim().Equals(""))
            {
                return;
            }

            Player humanPlayer = new Player
            {
                name = humanInput.text,
                type = "(Human)"
            };
            Player alienPlayer = new Player
            {
                name = alienInput.text,
                type = "(Alien)"
            };
            GameManager.globalManager.SetPlayers(humanPlayer, alienPlayer);
            SceneManager.LoadScene("BuyingMenu");
        });
    }
}