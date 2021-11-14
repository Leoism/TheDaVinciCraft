using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager sTheGlobalBehavior = null;
    public static Timer timer = null;

    public static Inventory humanInventory;
    public static Inventory alienInventory;
    public static BattleSystem battleSystem;
    
    private void Awake()
    {
        humanInventory = new Inventory();
        alienInventory = new Inventory();
        timer = new Timer();
    }

    // Start is called before the first frame update
    void Start()
    {
        GameManager.sTheGlobalBehavior = this;  // Singleton pattern
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    Inventory getHumanInventory()
    {
        return humanInventory;
    }


}
