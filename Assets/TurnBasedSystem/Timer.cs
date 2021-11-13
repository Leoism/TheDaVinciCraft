using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Timer : MonoBehaviour
{
    public Text timerEcho;
    public BattleSystem battleSystem;
    float secRemaining;
    bool isTimerRunning = false;
    bool gameStarted = false;

    // Start is called before the first frame update
    void Start()
    {
        gameStarted = true;
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


        if (!isTimerRunning)
        {
            switch (battleSystem.state)
            {
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
                int seconds = (int)secRemaining;
                timerEcho.text = "Time: " + seconds;
            }
            else
            {   
                nextState();
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
        nextState();
    }
    
    // Moves current state to next state
    public void nextState()
    {
        isTimerRunning = false;
        switch (battleSystem.state)
        {
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
}
