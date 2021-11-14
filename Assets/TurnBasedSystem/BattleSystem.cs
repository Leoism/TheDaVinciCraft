using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum BattleState { HUMANBUY, ALIENBUY, HUMANBUILD, ALIENDESTROY, HUMANWIN, ALIENWIN}
public class BattleSystem : MonoBehaviour
{
    public bool displayState = false;
    public BattleState state;
    public Text battleStateEcho = null;
    public GameObject humanPrefab = null;
    public GameObject alienPrefab = null;

    public Transform humanBattleStation = null;
    public Transform alienBattleStation = null;

    // Start is called before the first frame update
    void Start()
    {
        state = BattleState.HUMANBUY;
    }

    private void LateUpdate()
    {
        if (!displayState) return;
        switch (state)
        {
            case BattleState.HUMANBUY:
                battleStateEcho.text = "HUMAN player - Buy Round";
                break;
            case BattleState.ALIENBUY:
                battleStateEcho.text = "ALIEN player - Buy Round";
                break;
            case BattleState.HUMANBUILD:
                battleStateEcho.text = "HUMAN player - Build Round";
                break;
            case BattleState.ALIENDESTROY:
                battleStateEcho.text = "ALIEN player - Destroy Round";
                break;
        }
    }

    public void SetBattleState(BattleState setState)
    {
        this.state = setState;
    }
}
