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
<<<<<<< Updated upstream
      if (humanInput.text.Trim().Equals("") || alienInput.text.Trim().Equals(""))
      {
        return;
      }
      Player humanPlayer = new Player();
      Player alienPlayer = new Player();
      humanPlayer.name = humanInput.text;
      humanPlayer.type = "(Human)";
      alienPlayer.name = alienInput.text;
      alienPlayer.type = "(Alien)";
      GameManager.globalManager.SetPlayers(humanPlayer, alienPlayer);
      SceneManager.LoadScene("BuyingMenu");
    });
  }
}
=======
        playButton.onClick.AddListener(() =>
        {
            if (humanInput.text.Trim().Equals("") || alienInput.text.Trim().Equals(""))
            {
                return;
            }

            Player humanPlayer = new Player
            {
                Name = humanInput.text,
                Type = "(Human)"
            };
            Player alienPlayer = new Player
            {
                Name = alienInput.text,
                Type = "(Alien)"
            };
            GameManager.globalManager.SetPlayers(humanPlayer, alienPlayer);
            // SceneManager.LoadScene("BuyingMenu");
            StartCoroutine(ChangeScene());
        });
    }

    public Animator musicAnim;
    public float waitTime;

    IEnumerator ChangeScene()
    {
        musicAnim.SetTrigger("fadeOut");
        yield return new WaitForSeconds(waitTime);
        SceneManager.LoadScene("BuyingMenu");
    }



}
>>>>>>> Stashed changes
