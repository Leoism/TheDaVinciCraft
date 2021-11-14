using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ActionBarBehavior : MonoBehaviour
{
    public Text WoodAmountEcho;
    public Text StoneAmountEcho;
    public Text MetalAmountEcho;
    public Text FabricAmountEcho;
    public Text GlassAmountEcho;
    public Text SelectedMatEcho;

    private int selected;
    
    List<Item> currList;
    Button[] buttons;
    Text[] texts;
    
    // Start is called before the first frame update
    void Start()
    {
        buttons = this.GetComponentsInChildren<Button>();
        currList = GameManager.humanInventory.getItemList();
    }

    // Update is called once per frame
    void Update()
    {
        showAmount();
    }

    void showAmount()
    {
        WoodAmountEcho.text =   "" + currList[0].amount;
        StoneAmountEcho.text =  "" + currList[1].amount;
        MetalAmountEcho.text =  "" + currList[2].amount;
        FabricAmountEcho.text = "" + currList[3].amount;
        GlassAmountEcho.text =  "" + currList[4].amount;
    }
    public void selectedMaterial(int index)
    {
        // TODO: Keep button selected after click and have proper sprite attached
        switch(index)
        {
        case 0:
            SelectedMatEcho.text = "Selected: Wood";
            break;
        case 1:
            SelectedMatEcho.text = "Selected: Stone";
            break;
        case 2:
            SelectedMatEcho.text = "Selected: Metal";
            break;
        case 3:
            SelectedMatEcho.text = "Selected: Fabric";
            break;
        case 4:
            SelectedMatEcho.text = "Selected: Glass";
            break;
        default:
            SelectedMatEcho.text = "Selected: None";
            break;
        }
    }


}

