using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class PauseButton : MonoBehaviour
{
    public TextMeshProUGUI pause;
    public Timer timer;
    bool isPaused = false;

    public void togglePauseButton()
    {
        isPaused = !isPaused;
        timer.togglePause();
        pause.text = isPaused ? "Unpause" : "Pause";
    }
}
