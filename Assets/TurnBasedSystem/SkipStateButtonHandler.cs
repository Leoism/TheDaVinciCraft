using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class SkipStateButtonHandler : MonoBehaviour
{
    public Timer timer = null;

    // Skip to next state
    public void goNext()
    {
        timer.nextState();
    }
}
