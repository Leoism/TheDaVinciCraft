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
        currentRound = GameManager.globalManager.timer.GetRound();
        if (weaponCountEcho)
            weaponCountEcho.text = "Weapon Buy Limit: " + (GetWeaponBuyLimit());
        if (materialCountEcho)
            materialCountEcho.text = "Material Buy Limit: " + (GetMaterialBuyLimit());
    }

    public int GetCurrentRoundIdx()
    {
        return GameManager.globalManager.GetCurrentRound() - 1;
    }

    public int GetWeaponBuyLimit()
    {
        return weaponBuyLimit[GetCurrentRoundIdx()];
    }

    public int GetMaterialBuyLimit()
    {
        return materialBuyLimit[GetCurrentRoundIdx()];
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
    }
}
