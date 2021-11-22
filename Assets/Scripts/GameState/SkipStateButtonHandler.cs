using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class SkipStateButtonHandler : MonoBehaviour
{
    public GameObject confirmBox = null;
    public Timer timer = null;

    public void Update()
    {
        if (timer.IsTimeUp())
        {
            closePanel();
        }
    }
    // Skip to next state
    public void goNext()
    {
        if (timer.battleSystem.state != BattleState.ALIENDESTROY)
        {
            timer.finishState();
            confirmBox.SetActive(false);
        }
    }

    public void showPanel()
    {
        confirmBox.SetActive(true);
    }
    public void closePanel()
    {
        confirmBox.SetActive(false);
    }
}
