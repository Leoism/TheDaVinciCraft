using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuyMenuItemControl : MonoBehaviour
{
    public GameObject woodPanel;
    public GameObject fabricPanel;
    public GameObject stonePanel;
    public GameObject glassPanel;
    public GameObject metalPanel;

    private void Awake() 
    {
        if (GameManager.globalManager.GetCurrentRound() == 0) {
            RoundOne();
        }
    }
    private void RoundOne()
    {
        stonePanel.SetActive(false);
        glassPanel.SetActive(false);
        metalPanel.SetActive(false);
    }
    private void RoundTwo()
    {
        glassPanel.SetActive(false);
        metalPanel.SetActive(false);
    }

}
