using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BuyingSystem : MonoBehaviour
{
    // Start is called before the first frame update

    int currentRound;
    public TextMeshProUGUI weaponCountEcho;
    public TextMeshProUGUI materialCountEcho;
    private List<int> weaponBuyLimit;
    private List<int> materialBuyLimit;
    public int currentAdded;
    void Start()
    {
        currentAdded = 0;
        weaponBuyLimit = GameManager.globalManager.GetWeaponCountForRound();
        materialBuyLimit = GameManager.globalManager.GetMaterialCountForRound();
    }

    void LateUpdate()
    {
        currentRound = GameManager.globalManager.Timer.GetRound();
        weaponCountEcho.text = "Weapon Buy Limit: " + (GetWeaponBuyLimit() - currentAdded);
        materialCountEcho.text = "Material Buy Limit: " + (GetMaterialBuyLimit() - currentAdded);
    }

    public int GetCurrentRound()
    {
        return GameManager.globalManager.GetCurrentRound();
    }

    public int GetWeaponBuyLimit()
    {
        return weaponBuyLimit[GetCurrentRound()];
    }

    public int GetMaterialBuyLimit()
    {
        return materialBuyLimit[GetCurrentRound()];
    }

    public void incrementAdded()
    {
        currentAdded++;
    }
    public void decremenentAdded()
    {
        if (currentAdded > 0)
        currentAdded--;
    }

    public void resetCurrentAdded()
    {
        currentAdded = 0;
    }

    public void ResetRounds()
    {
        currentAdded = 0;
        GameManager.globalManager.ResetRound();
    }
}
