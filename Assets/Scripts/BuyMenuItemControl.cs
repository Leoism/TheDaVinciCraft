using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuyMenuItemControl : MonoBehaviour
{
    // materials
    public GameObject woodPanel;
    public GameObject fabricPanel;
    public GameObject stonePanel;
    public GameObject glassPanel;
    public GameObject metalPanel;

    // weapons
    // public GameObject deforPanel;
    // public GameObject boomPanel;
    // public GameObject arrowPanel;
    public GameObject bBallPanel;
    public GameObject bombPanel;
    public GameObject mePanel;
    public GameObject magnetPanel;
    public GameObject rayPanel;
    public GameObject grenadePanel;



    private void Awake() 
    {
        if (GameManager.globalManager.GetCurrentRound() == 0) {
            RoundOne();
        }
        if (GameManager.globalManager.GetCurrentRound() == 1) {
            RoundTwo();
        }
    }
    private void RoundOne()
    {
        stonePanel.SetActive(false);
        glassPanel.SetActive(false);
        metalPanel.SetActive(false);

        bBallPanel.SetActive(false);
        bombPanel.SetActive(false);
        mePanel.SetActive(false);
        magnetPanel.SetActive(false);
        rayPanel.SetActive(false);
        grenadePanel.SetActive(false);
    }
    private void RoundTwo()
    {
        glassPanel.SetActive(false);
        metalPanel.SetActive(false);

        magnetPanel.SetActive(false);
        rayPanel.SetActive(false);
        grenadePanel.SetActive(false);
    }

}
