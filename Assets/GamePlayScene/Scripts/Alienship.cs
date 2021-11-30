using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Alienship : MonoBehaviour
{
    bool isInitialized = false;
    private float currentTime = 0;
    public GameObject powerBar = null;
    public ShootingBehavior shootingBehavior = null;
    
    void Start()
    {
        gameObject.SetActive(false);
        powerBar.SetActive(false);
        currentTime = 8;
    }
    public void Init()
    {
        if (isInitialized) return;
        powerBar.SetActive(true);
        gameObject.SetActive(true);
        Vector3 windowCorner = Camera.main.ViewportToWorldPoint(new Vector3(0.8f, 0.85f, 1));
        transform.position = windowCorner;
    }

    public void UnInit()
    {
        if (isInitialized)
        {
        gameObject.SetActive(false);
        }
    }
    void Update()
    {
        // for addressing  the timer ran out for the alien 
        // but they were able to keep shooting and win.
        if (Timer.secRemaining <= 0)
        {
            SceneManager.LoadScene("HumanWin");
        }
        
        // add timer about 10 sec
        if (GameManager.globalManager.alienInventory.TotalCount() <= 0)
        {
            CountTenSec();
        }
    }
    private void CountTenSec()
    {
        if (Timer.secRemaining < 10)
        {
            currentTime = Timer.secRemaining;
        }
        currentTime -= 1 * Time.deltaTime;
        // Debug.Log(currentTime);
        if (currentTime <= 0)
        {
            currentTime = 8;
            SceneManager.LoadScene("HumanWin");
        }
    }
}
