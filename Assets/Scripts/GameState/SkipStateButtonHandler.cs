using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
public class SkipStateButtonHandler : MonoBehaviour
{
    public GameObject confirmBox = null;
    public Timer timer = null;
    public PhotonView canvasPhotonView;

    public void Start()
    {
        // Deactivate for the alien
        if (GameManager.globalManager.isOnlineMode && !PhotonNetwork.IsMasterClient)
        {
            gameObject.SetActive(false);
        }
    }
    public void Update()
    {
        if (timer.IsTimeUp())
        {
            closePanel();
        }
        if (timer.battleSystem.state == BattleState.ALIENDESTROY)
        {
            this.gameObject.SetActive(false);
        }
    }
    // Skip to next state
    public void goNext()
    {
        if (timer.battleSystem.state != BattleState.ALIENDESTROY)
        {
            timer.finishState();
            confirmBox.SetActive(false);
            if (GameManager.globalManager.isOnlineMode)
            {
                canvasPhotonView.RPC("RPC_OnClick_Done", RpcTarget.Others);
            }
        }
    }

    public void showPanel()
    {
        confirmBox.SetActive(true);
        confirmBox.tag = "Button";
    }
    public void closePanel()
    {
        confirmBox.SetActive(false);
    }
}
