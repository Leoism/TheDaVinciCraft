using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Timer : MonoBehaviour
{

    public Text timerEcho;
    public BattleSystem battleSystem;
    float secRemaining;
    bool timerEnded = false;
    bool timerStarted = false;
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


        if (!timerStarted)
        {
            switch (battleSystem.state)
            {
                case BattleState.HUMANBUY:
                    secRemaining = 30f;
                    timerStarted = true;
                    timerEnded = false;
                    break;
                case BattleState.ALIENBUY:
                    secRemaining = 30f;
                    timerStarted = true;
                    timerEnded = false;
                    break;
                case BattleState.HUMANBUILD:
                    secRemaining = 60f;
                    timerStarted = true;
                    timerEnded = false;
                    break;
                case BattleState.ALIENDESTROY:
                    secRemaining = 60f;
                    timerStarted = true;
                    timerEnded = false;
                    break;
                default: break;
            }
        }

        if (!timerEnded)
        {
            if (secRemaining > 1)
            {
                secRemaining -= Time.deltaTime;
                int seconds = (int)secRemaining;
                timerEcho.text = "Time Remaining: " + seconds;
            }
            else
            {
                
                nextState();
            }
        }


    }

    public void finishState()
    {
        nextState();
    }

    public void nextState()
    {
        timerEnded = true;
        timerStarted = false;
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
