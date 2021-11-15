using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CountDownTimer : MonoBehaviour
{
    private float currentTime = 0;
    public float startingTime;
    public static bool isTimeUp = false;
    private float colorChangeAt; 
    [SerializeField] public Text countdowntext;
    void Start()
    {
        currentTime = startingTime;
        colorChangeAt = startingTime / 3;
    }

    void Update()
    {
        if (!isTimeUp){
            Countdown();
        }    
    }

    public void Countdown()
    {
        isTimeUp = false;

        if (currentTime <= colorChangeAt){
            countdowntext.color = Color.red;
        }

        currentTime -= 1 * Time.deltaTime;
        countdowntext.text = currentTime.ToString("0");

        if (currentTime <= 0){
            currentTime = 0;
            isTimeUp = true;
        }
    }
    public void StartTimer() 
    {
        isTimeUp = false;
        currentTime = startingTime;
    }


}
