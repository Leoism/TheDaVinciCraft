using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{
    public TextMeshProUGUI timerEcho;
    public TextMeshProUGUI roundTitle;
    public BattleSystem battleSystem;

    float secRemaining;

    public float roundDelayTime = 5f;
    private float delayRemaining;

    bool isTimerRunning = false;
    bool isDelayRunning = false;
    bool isPaused = false;
    bool gameStarted = false;
    // Start is called before the first frame update
    void Start()
    {
        gameStarted = true;
        delayRemaining = roundDelayTime;
        roundTitle.text = string.Empty;
    }

    // Update is called once per frame
    void Update()
    {
        if (!gameStarted)
        {
            timerEcho.color = new Color(0f, 0f, 0f, 0f);
            return;

        }
        else
        {
            timerEcho.color = new Color(255f, 255f, 255f, 255f);
        }

        if (isPaused)
        {
            roundTitle.text = "PAUSED";
            return;
        }

        if (!isTimerRunning && !isDelayRunning)
        {
            switch (battleSystem.state)
            {
                case BattleState.NONE:
                    finishState();
                    break;
                case BattleState.HUMANBUY:
                    secRemaining = 30f;
                    isTimerRunning = true;
                    break;
                case BattleState.ALIENBUY:
                    secRemaining = 30f;
                    isTimerRunning = true;
                    break;
                case BattleState.HUMANBUILD:
                    secRemaining = 60f;
                    isTimerRunning = true;
                    break;
                case BattleState.ALIENDESTROY:
                    secRemaining = 60f;
                    isTimerRunning = true;
                    break;
                default: break;
            }
        }
        else {
            if (secRemaining > 1)
            {
                secRemaining -= Time.deltaTime;
                int seconds = (int) secRemaining;
                timerEcho.text = "Time: " + seconds;
            }
            else
            {
                // Timer stops running
                finishState();
            }
        }


    }

    public bool IsTimeUp()
    {
        return !isTimerRunning; 
    }

    public string GetCurrentPlayer()
    {
        return (battleSystem.state == BattleState.ALIENBUY || battleSystem.state == BattleState.ALIENDESTROY || battleSystem.state == BattleState.ALIENWIN) ?
        "Alien" : "Human";
    } 

    // Completes state and goes to next
    public void finishState()
    {
        secRemaining = 0;
        isTimerRunning = false;
        isDelayRunning = true;
        ShowDelay();
    }
    
    // Moves current state to next state
    public void nextState()
    {
        switch (battleSystem.state)
        {
            case BattleState.NONE:
                battleSystem.SetBattleState(BattleState.HUMANBUY);
                break;
            case BattleState.HUMANBUY:
                battleSystem.SetBattleState(BattleState.ALIENBUY);
                break;
            case BattleState.ALIENBUY:
                battleSystem.SetBattleState(BattleState.HUMANBUILD);
                break;
            case BattleState.HUMANBUILD:
                battleSystem.SetBattleState(BattleState.ALIENDESTROY);
                break;
            case BattleState.ALIENDESTROY:
                // Check win conditions
                break;
            default: break;
        }
    }

    void ShowDelay()
    {
        if (!isTimerRunning && delayRemaining > 1)
        {
            delayRemaining -= Time.deltaTime;
            int seconds = (int)delayRemaining;
            timerEcho.text = "Round \nSwitch \nDelay: " + seconds;

            switch (battleSystem.state)
            {
                case BattleState.NONE:
                    roundTitle.text = "Starting Game: Human Buy Phase Start";
                    break;
                case BattleState.HUMANBUY:
                    roundTitle.text = "Switching to Alien Buy Phase";
                    break;
                case BattleState.ALIENBUY:
                    roundTitle.text = "Switching to Human Build Phase";
                    break;
                case BattleState.HUMANBUILD:
                    roundTitle.text = "Switching to Alien Destroy Phase";
                    break;
                case BattleState.ALIENDESTROY:
                    roundTitle.text = "Calculating Round Winner";
                    break;
                default: break;
            }

        }
        else
        {
            roundTitle.text = string.Empty;
            isDelayRunning = false;
            delayRemaining = roundDelayTime;
            nextState();
        }
    }
    public void togglePause()
    {
        isPaused = !isPaused;
        if (!isPaused && !isDelayRunning)
        {
            roundTitle.text = string.Empty;
        }
    }
}

