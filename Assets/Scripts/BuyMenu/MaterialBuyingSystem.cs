using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MaterialBuyingSystem : MonoBehaviour
{
    // ------------------------------ Buttons  ------------------------------
    [SerializeField] private Button wood;
    [SerializeField] private Button woodAdd;
    [SerializeField] private Button woodRemove;
    [SerializeField] private Button febric;
    [SerializeField] private Button febricAdd;
    [SerializeField] private Button febricRemove;
    [SerializeField] private Button stone;
    [SerializeField] private Button stoneAdd;
    [SerializeField] private Button stoneRemove;
    [SerializeField] private Button glass;
    [SerializeField] private Button glassAdd;
    [SerializeField] private Button glassRemove;
    [SerializeField] private Button metal;
    [SerializeField] private Button metalAdd;
    [SerializeField] private Button metalRemove;
    [SerializeField] private BuyingSystem buySystem;
    [SerializeField] private List<InputField> allInputFields;
    // int variables
    private int totalMaterials;
    private int selectedMaterials = 0;
    // public static int round = 0;

    private void Awake() 
    {
        // inactive the decrease button at the beginning
        woodRemove.interactable = (WoodCnt.woodCnt > 0);
        febricRemove.interactable = (FebricCnt.febricCnt > 0);
        stoneRemove.interactable = (StoneCnt.stoneCnt > 0);
        glassRemove.interactable = (GlassCnt.glassCnt > 0);
        metalRemove.interactable = (MetalCnt.metalCnt > 0);
    }
    void Start()
    {
        buySystem.currentAdded = 0;
        List<int> totalMaterialList = GameManager.globalManager.GetMaterialCountForRound();
        int currentRound = buySystem.GetCurrentRound();
        totalMaterials = totalMaterialList[currentRound];
        WoodCnt.woodCnt = 0;
        FebricCnt.febricCnt = 0;
        StoneCnt.stoneCnt = 0;
        GlassCnt.glassCnt = 0;
        MetalCnt.metalCnt = 0;
    }

    private void Update() {
        if (CountDownTimer.isTimeUp == true) {
            inactiveAllButs();
        }
        if (selectedMaterials >= totalMaterials) {
            activeAllAdds(false);
        }
        if (selectedMaterials < totalMaterials) {
            activeAllAdds(true);
        }
    }

    //------------------------------ Wood -------------------------------------
    public void SelectWood()
    {
        selectedMaterials += 1;
        WoodCnt.woodCnt = add(wood, woodAdd, woodRemove, totalMaterials, WoodCnt.woodCnt, selectedMaterials);
        // woodCnt.woodCnt = add(wood, woodAdd, woodRemove, totalWoodCnt, woodCnt.woodCnt);
        InputField inputField = allInputFields.Find((inputField) => inputField.gameObject.name.Equals("WoodInput"));
        inputField.SetTextWithoutNotify(WoodCnt.woodCnt.ToString());
    }
    public void RemoveWood()
    {
        WoodCnt.woodCnt = sub(wood, woodAdd, woodRemove, totalMaterials, WoodCnt.woodCnt, selectedMaterials);
        selectedMaterials -= 1;
        // woodCnt.woodCnt = sub(wood, woodAdd, woodRemove, totalWoodCnt, woodCnt.woodCnt);
        InputField inputField = allInputFields.Find((inputField) => inputField.gameObject.name.Equals("WoodInput"));
        inputField.SetTextWithoutNotify(WoodCnt.woodCnt.ToString());
    }
    //------------------------------ Febric ----------------------------
    public void SelectFebric()
    {
        selectedMaterials += 1;
        FebricCnt.febricCnt = add(febric, febricAdd, febricRemove, totalMaterials, FebricCnt.febricCnt, selectedMaterials);
        InputField inputField = allInputFields.Find((inputField) => inputField.gameObject.name.Equals("FabricInput"));
        inputField.SetTextWithoutNotify(FebricCnt.febricCnt.ToString());
    }
    public void RemoveFebric()
    {
        FebricCnt.febricCnt = sub(febric, febricAdd, febricRemove, totalMaterials, FebricCnt.febricCnt, selectedMaterials);
        selectedMaterials -= 1;
        InputField inputField = allInputFields.Find((inputField) => inputField.gameObject.name.Equals("FabricInput"));
        inputField.SetTextWithoutNotify(FebricCnt.febricCnt.ToString());
    }

    //------------------------------ Stone -----------------------------
    public void SelectStone()
    {
        selectedMaterials += 1;
        StoneCnt.stoneCnt = add(stone, stoneAdd, stoneRemove, totalMaterials, StoneCnt.stoneCnt, selectedMaterials);
        InputField inputField = allInputFields.Find((inputField) => inputField.gameObject.name.Equals("StoneInput"));
        inputField.SetTextWithoutNotify(StoneCnt.stoneCnt.ToString());
    }
    public void RemoveStone()
    {
        StoneCnt.stoneCnt = sub(stone, stoneAdd, stoneRemove, totalMaterials, StoneCnt.stoneCnt, selectedMaterials);
        selectedMaterials -= 1;
        InputField inputField = allInputFields.Find((inputField) => inputField.gameObject.name.Equals("StoneInput"));
        inputField.SetTextWithoutNotify(StoneCnt.stoneCnt.ToString());
    }
    //------------------------------ Glass -----------------------------
    public void SelectGlass()
    {
        selectedMaterials += 1;
        GlassCnt.glassCnt = add(glass, glassAdd, glassRemove, totalMaterials, GlassCnt.glassCnt, selectedMaterials);
        InputField inputField = allInputFields.Find((inputField) => inputField.gameObject.name.Equals("GlassInput"));
        inputField.SetTextWithoutNotify(GlassCnt.glassCnt.ToString());
    }
    public void RemoveGlass()
    {
        GlassCnt.glassCnt = sub(glass, glassAdd, glassRemove, totalMaterials, GlassCnt.glassCnt, selectedMaterials);
        selectedMaterials -= 1;
        InputField inputField = allInputFields.Find((inputField) => inputField.gameObject.name.Equals("GlassInput"));
        inputField.SetTextWithoutNotify(GlassCnt.glassCnt.ToString());
    }

    //------------------------------ Metal -----------------------------
    public void SelectMetal()
    {
        selectedMaterials += 1;
        MetalCnt.metalCnt = add(metal, metalAdd, metalRemove, totalMaterials, MetalCnt.metalCnt, selectedMaterials);
        InputField inputField = allInputFields.Find((inputField) => inputField.gameObject.name.Equals("MetalInput"));
        inputField.SetTextWithoutNotify(MetalCnt.metalCnt.ToString());
    }
    public void RemoveMetal()
    {
        MetalCnt.metalCnt = sub(metal, metalAdd, metalRemove, totalMaterials, MetalCnt.metalCnt, selectedMaterials);
        selectedMaterials -= 1;
        InputField inputField = allInputFields.Find((inputField) => inputField.gameObject.name.Equals("MetalInput"));
        inputField.SetTextWithoutNotify(MetalCnt.metalCnt.ToString());
    }

    //------------------------------ Getters ---------------------------
    // public string GetwoodCnt()
    // {
    //     return WoodCnt.woodCnt.ToString();
    // }

    //------------------------------ helper method ---------------------
    public int add(Button image, Button add, Button sub, int total, int count, int selected)
    {
        count += 1;
        image.interactable = (selected < total);
        add.interactable = (selected < total);
        sub.interactable = (count > 0);
        buySystem.incrementAdded();
        return count;
    }
    public int sub(Button image, Button add, Button sub, int total, int count, int selected)
    {
        image.interactable = (selected < total + 1);
        add.interactable = (selected < total + 1);
        count -= 1;
        sub.interactable = (count > 0);
        buySystem.decremenentAdded();
        return count;
    }
    private void inactiveAllButs()
    {
        wood.interactable = false;
        woodAdd.interactable = false;
        febric.interactable = false;
        febricAdd.interactable = false;
        stone.interactable = false;
        stoneAdd.interactable = false;
        glass.interactable = false;
        glassAdd.interactable = false;
        metal.interactable = false;
        metalAdd.interactable = false;
        woodRemove.interactable = false;
        febricRemove.interactable = false;
        stoneRemove.interactable = false;
        glassRemove.interactable = false;
        metalRemove.interactable = false;
    }
    private void activeAllAdds(bool conditions) {
        wood.interactable = conditions;
        woodAdd.interactable = conditions;
        febric.interactable = conditions;
        febricAdd.interactable = conditions;
        stone.interactable = conditions;
        stoneAdd.interactable = conditions;
        glass.interactable = conditions;
        glassAdd.interactable = conditions;
        metal.interactable = conditions;
        metalAdd.interactable = conditions;
    }

        private int setCount(Button image, Button add, Button sub,
                         int maxWeapons, ref int currentWeaponCount,
                         ref int currentTotalSelected, int newValue)
    {
        // remove the current weapon count from the total
        currentTotalSelected -= currentWeaponCount;
        currentWeaponCount = 0;
        // the new weapon count should be the value set
        currentWeaponCount = newValue + currentTotalSelected > maxWeapons ? (maxWeapons - currentTotalSelected > 0 ? maxWeapons - currentTotalSelected : 0) : newValue;

        selectedMaterials = currentTotalSelected + currentWeaponCount >= totalMaterials ? totalMaterials : currentTotalSelected + currentWeaponCount;
        image.interactable = currentTotalSelected < maxWeapons;
        add.interactable = currentTotalSelected < maxWeapons;
        sub.interactable = currentWeaponCount > 0;
        return currentWeaponCount;
    }

    public void OnInput_UpdateCount(string newCount)
    {
        InputField activeInputField = allInputFields.Find((inputField) => {
            return inputField.isFocused;
        });
        GameObject parentOfInputField = activeInputField.transform.parent.gameObject;
        // this is dependent on the order of the GameObjects in the inspector hierarchy
        // better approach is to refactor buying system, for now this should suffice
        // [Sprite, Add, Sub]
        Button[] displayButtons = parentOfInputField.GetComponentsInChildren<Button>();
        int count = 0;
        int.TryParse(newCount, out count);
        switch(parentOfInputField.name) {
            case "WoodPanel":
                count = setCount(displayButtons[0], displayButtons[1], displayButtons[2], totalMaterials, ref WoodCnt.woodCnt, ref selectedMaterials, count);
                break;
            case "FebricPanel":
                count = setCount(displayButtons[0], displayButtons[1], displayButtons[2], totalMaterials, ref FebricCnt.febricCnt, ref selectedMaterials, count);
                break;
            case "StonePanel":
                count = setCount(displayButtons[0], displayButtons[1], displayButtons[2], totalMaterials, ref StoneCnt.stoneCnt, ref selectedMaterials, count);
                break;
            case "GlassPanel":
                count = setCount(displayButtons[0], displayButtons[1], displayButtons[2], totalMaterials, ref GlassCnt.glassCnt, ref selectedMaterials, count);
                break;
            case "MetalPanel":
                count = setCount(displayButtons[0], displayButtons[1], displayButtons[2], totalMaterials, ref MetalCnt.metalCnt, ref selectedMaterials, count);
                break;
            default:
                break;
        }

        activeInputField.SetTextWithoutNotify(count.ToString());
    }

    public void SaveHumanInventory()
    {
        List<GameObject> humanInventory = new List<GameObject>();
        if (FebricCnt.febricCnt > 0)
        {
            humanInventory.Add(CreateSprite(FebricCnt.febricCnt, "fabric"));
        }
        if (WoodCnt.woodCnt > 0)
        {
            humanInventory.Add(CreateSprite(WoodCnt.woodCnt, "wood"));
        }
        if (StoneCnt.stoneCnt > 0)
        {
            humanInventory.Add(CreateSprite(StoneCnt.stoneCnt, "stone"));
        }
        if (GlassCnt.glassCnt > 0)
        {
            humanInventory.Add(CreateSprite(GlassCnt.glassCnt, "glass"));
        }
        if (MetalCnt.metalCnt > 0)
        {
            humanInventory.Add(CreateSprite(MetalCnt.metalCnt, "metal"));
        }
        GameManager.globalManager.SetHumanInventory(humanInventory);
    }

    private GameObject CreateSprite(int count, string name)
    {
        GameObject newGameObject = new GameObject();
        Item newItem = newGameObject.AddComponent<Item>();
        newItem.SetCount(count);
        Button itemButton = null;
        switch(name) {
            case "fabric":
                itemButton = febric;
                newItem.SetItemName("Fabric");
                newItem.SetMessage("Fabric - Don't run into any archers ");
                break;
            case "wood":
                itemButton = wood;
                newItem.SetItemName("Wood");
                newItem.SetMessage("Wood - The pioneer of minecraft houses ");
                break;
            case "stone":
                itemButton = stone;
                newItem.SetItemName("Stone");
                newItem.SetMessage("Stone - Cement, wow, very stronk! ");
                break;
            case "glass":
                itemButton = glass;
                newItem.SetItemName("Glass");
                newItem.SetMessage("Glass - Hopefully it's bulletproof ");
                break;
            case "metal":
                itemButton = metal;
                newItem.SetItemName("Metal");
                newItem.SetMessage("Metal - OP material, pls nerf! ");
                break;
            default:
                break;
        }
        Sprite itemSprite = itemButton.GetComponent<Image>().sprite;
        Image newImage = newGameObject.AddComponent<Image>();
        newImage.sprite = itemSprite;
        Button newButton = newGameObject.AddComponent<Button>();
        
        newButton.GetComponent<RectTransform>().sizeDelta = new Vector2(128, 128);
        newGameObject.AddComponent<ItemPreserver>();
        GameObject textCount = new GameObject();
        Text newText = textCount.AddComponent<Text>();
        newText.font = Resources.GetBuiltinResource(typeof(Font), "Arial.ttf") as Font;
        newText.fontSize = 75;
        textCount.transform.localPosition = newGameObject.transform.position + new Vector3(150, 0, 0);
        textCount.transform.parent = newGameObject.transform;

        ActionBarItem actionBarItem = newGameObject.AddComponent<ActionBarItem>();
        actionBarItem.actionItem = newItem;
        return newGameObject;
    }
}
