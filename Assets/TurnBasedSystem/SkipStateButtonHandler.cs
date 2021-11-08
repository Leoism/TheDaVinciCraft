using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class SkipStateButtonHandler : MonoBehaviour
{
    public GameObject confirmBox = null;
    public Timer timer = null;

    // Skip to next state
    public void goNext()
    {
        timer.nextState();
        confirmBox.SetActive(false);
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
