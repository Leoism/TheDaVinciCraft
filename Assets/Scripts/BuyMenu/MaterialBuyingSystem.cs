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

    // int variables
    private int totalMaterials;
    private int selectedMaterials = 0;


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
        totalMaterials = 12;
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
    }
    public void RemoveWood()
    {
        WoodCnt.woodCnt = sub(wood, woodAdd, woodRemove, totalMaterials, WoodCnt.woodCnt, selectedMaterials);
        selectedMaterials -= 1;
        // woodCnt.woodCnt = sub(wood, woodAdd, woodRemove, totalWoodCnt, woodCnt.woodCnt);
    }
    //------------------------------ Febric ----------------------------
    public void SelectFebric()
    {
        selectedMaterials += 1;
        FebricCnt.febricCnt = add(febric, febricAdd, febricRemove, totalMaterials, FebricCnt.febricCnt, selectedMaterials);
    }
    public void RemoveFebric()
    {
        FebricCnt.febricCnt = sub(febric, febricAdd, febricRemove, totalMaterials, FebricCnt.febricCnt, selectedMaterials);
        selectedMaterials -= 1;
    }

    //------------------------------ Stone -----------------------------
    public void SelectStone()
    {
        selectedMaterials += 1;
        StoneCnt.stoneCnt = add(stone, stoneAdd, stoneRemove, totalMaterials, StoneCnt.stoneCnt, selectedMaterials);
    }
    public void RemoveStone()
    {
        StoneCnt.stoneCnt = sub(stone, stoneAdd, stoneRemove, totalMaterials, StoneCnt.stoneCnt, selectedMaterials);
        selectedMaterials -= 1;
    }
    //------------------------------ Glass -----------------------------
    public void SelectGlass()
    {
        selectedMaterials += 1;
        GlassCnt.glassCnt = add(glass, glassAdd, glassRemove, totalMaterials, GlassCnt.glassCnt, selectedMaterials);
    }
    public void RemoveGlass()
    {
        GlassCnt.glassCnt = sub(glass, glassAdd, glassRemove, totalMaterials, GlassCnt.glassCnt, selectedMaterials);
        selectedMaterials -= 1;
    }

    //------------------------------ Metal -----------------------------
    public void SelectMetal()
    {
        selectedMaterials += 1;
        MetalCnt.metalCnt = add(metal, metalAdd, metalRemove, totalMaterials, MetalCnt.metalCnt, selectedMaterials);
    }
    public void RemoveMetal()
    {
        MetalCnt.metalCnt = sub(metal, metalAdd, metalRemove, totalMaterials, MetalCnt.metalCnt, selectedMaterials);
        selectedMaterials -= 1;
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
        return count;
    }
    public int sub(Button image, Button add, Button sub, int total, int count, int selected)
    {
        image.interactable = (selected < total + 1);
        add.interactable = (selected < total + 1);
        count -= 1;
        sub.interactable = (count > 0);
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
}
