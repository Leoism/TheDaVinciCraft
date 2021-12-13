using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class WeaponBuyingSystem : MonoBehaviour
{
    [SerializeField] private Button deforestor;
    [SerializeField] private Button deAdd;
    [SerializeField] private Button deSub;
    [SerializeField] private Button mExtractor;
    [SerializeField] private Button meAdd;
    [SerializeField] private Button meSub;
    [SerializeField] private Button arrow;
    [SerializeField] private Button arrAdd;
    [SerializeField] private Button arrSub;
    [SerializeField] private Button ball;
    [SerializeField] private Button ballAdd;
    [SerializeField] private Button ballSub;
    [SerializeField] private Button boomerange;
    [SerializeField] private Button boomAdd;
    [SerializeField] private Button boomSub;
    [SerializeField] private Button magnet;
    [SerializeField] private Button magAdd;
    [SerializeField] private Button magSub;
    [SerializeField] private Button bomb;
    [SerializeField] private Button bombAdd;
    [SerializeField] private Button bombSub;
    [SerializeField] private Button ray;
    [SerializeField] private Button rayAdd;
    [SerializeField] private Button raySub;
    [SerializeField] private Button grenade;
    [SerializeField] private Button grenadeAdd;
    [SerializeField] private Button grenadeSub;
    [SerializeField] private Button grapple;
    [SerializeField] private BuyingSystem buySystem;
    [SerializeField] private List<InputField> allInputFields;

    // int variables
    private int totalWeapons;
    private int selectedWeapons = 0;
    private void Awake()
    {
        deSub.interactable = (DeforestorCnt.deCnt > 0);
        meSub.interactable = (MECnt.meCnt > 0);
        arrSub.interactable = (ArrowCnt.arrCnt > 0);
        ballSub.interactable = (BallCnt.ballCnt > 0);
        boomSub.interactable = (BoomCnt.boomCnt > 0);
        magSub.interactable = (MagnetCnt.magCnt > 0);
        bombSub.interactable = (BombCnt.bombCnt > 0);
        raySub.interactable = (RayCnt.rayCnt > 0);
        grenadeSub.interactable = (GrenadeCnt.grCnt > 0);


        activateAllAdds(false);
        // round 1 only activate deforestor, arrow boomerang
        if (GameManager.globalManager.GetCurrentRound() == 1)
        {
            activeAddsForRoundOne(true);
        }
        // round 2 unblock ball, bomb, mineral extractor
        else if (GameManager.globalManager.GetCurrentRound() == 1)
        {
            activeAddsForRoundTwo(true);
        }

    }
    void Start()
    {
        buySystem.currentAdded = 0;
        List<int> totalWeaponsList = GameManager.globalManager.GetWeaponCountForRound();
        int currentRoundIdx = buySystem.GetCurrentRoundIdx();
        totalWeapons = totalWeaponsList[currentRoundIdx];
        DeforestorCnt.deCnt = 0;
        MECnt.meCnt = 0;
        ArrowCnt.arrCnt = 0;
        BallCnt.ballCnt = 0;
        BoomCnt.boomCnt = 0;
        MagnetCnt.magCnt = 0;
        BombCnt.bombCnt = 0;
        RayCnt.rayCnt = 0;
        GrenadeCnt.grCnt = 0;
        GrappleCnt.grappleCnt = 1;
    }

    // Update is called once per frame
    void Update()
    {
        if (selectedWeapons >= totalWeapons)
        {
            activateAllAdds(false);
        }

        if (selectedWeapons < totalWeapons)
        {
            if (GameManager.globalManager.GetCurrentRound() == 1)
            {
                activeAddsForRoundOne(true);
            }
            else if (GameManager.globalManager.GetCurrentRound() == 2)
            {
                activeAddsForRoundTwo(true);
            }
            else
            {
                activateAllAdds(true);
            }
        }

        if (CountDownTimer.isTimeUp == true)
        {
            activateAllAdds(false);
            activateAllSubs(false);
        }
    }

    //------------------------------ Deforestor -----------------------------
    public void SelectDeforestor()
    {
        selectedWeapons += 1;
        DeforestorCnt.deCnt = add(deforestor, deAdd, deSub, totalWeapons, DeforestorCnt.deCnt, selectedWeapons);
        InputField inputField = allInputFields.Find((inputField) => inputField.gameObject.name.Equals("DeforestorInput"));
        inputField.SetTextWithoutNotify(DeforestorCnt.deCnt.ToString());
    }
    public void RemoveDeforestor()
    {
        DeforestorCnt.deCnt = sub(deforestor, deAdd, deSub, totalWeapons, DeforestorCnt.deCnt, selectedWeapons);
        selectedWeapons -= 1;
        InputField inputField = allInputFields.Find((inputField) => inputField.gameObject.name.Equals("DeforestorInput"));
        inputField.SetTextWithoutNotify(DeforestorCnt.deCnt.ToString());
    }

    //------------------------------ Mineral Extractor ------------------------
    public void SelectMExtractor()
    {
        // limit the minerla extractor selecting amount to at most 3
        if (MECnt.meCnt < 3) {
            selectedWeapons += 1;
            MECnt.meCnt = add(mExtractor, meAdd, meSub, totalWeapons, MECnt.meCnt, selectedWeapons);
        }
        if (MECnt.meCnt > 2) {
            mExtractor.interactable = false;
            meAdd.interactable = false;
        }
        InputField inputField = allInputFields.Find((inputField) => inputField.gameObject.name.Equals("MineralExtractInput"));
        inputField.SetTextWithoutNotify(MECnt.meCnt.ToString());
    }
    public void RemoveMExtractor()
    {
        MECnt.meCnt = sub(mExtractor, meAdd, meSub, totalWeapons, MECnt.meCnt, selectedWeapons);
        selectedWeapons -= 1;
        InputField inputField = allInputFields.Find((inputField) => inputField.gameObject.name.Equals("MineralExtractInput"));
        inputField.SetTextWithoutNotify(MECnt.meCnt.ToString());
    }

    //------------------------------ Arrow ------------------------------------
    public void SelectArrow()
    {
        selectedWeapons += 1;
        ArrowCnt.arrCnt = add(arrow, arrAdd, arrSub, totalWeapons, ArrowCnt.arrCnt, selectedWeapons);
        InputField inputField = allInputFields.Find((inputField) => inputField.gameObject.name.Equals("ArrowInput"));
        inputField.SetTextWithoutNotify(ArrowCnt.arrCnt.ToString());
    }
    public void RemoveArrow()
    {
        ArrowCnt.arrCnt = sub(arrow, arrAdd, arrSub, totalWeapons, ArrowCnt.arrCnt, selectedWeapons);
        selectedWeapons -= 1;
        InputField inputField = allInputFields.Find((inputField) => inputField.gameObject.name.Equals("ArrowInput"));
        inputField.SetTextWithoutNotify(ArrowCnt.arrCnt.ToString());
    }

    //------------------------------ Ball ------------------------------------
    public void SelectBall()
    {
        selectedWeapons += 1;
        BallCnt.ballCnt = add(ball, ballAdd, ballSub, totalWeapons, BallCnt.ballCnt, selectedWeapons);
        InputField inputField = allInputFields.Find((inputField) => inputField.gameObject.name.Equals("BallInput"));
        inputField.SetTextWithoutNotify(BallCnt.ballCnt.ToString());
    }
    public void RemoveBall()
    {
        BallCnt.ballCnt = sub(ball, ballAdd, ballSub, totalWeapons, BallCnt.ballCnt, selectedWeapons);
        selectedWeapons -= 1;
        InputField inputField = allInputFields.Find((inputField) => inputField.gameObject.name.Equals("BallInput"));
        inputField.SetTextWithoutNotify(BallCnt.ballCnt.ToString());
    }


    //------------------------------ Boomerange ------------------------------------
    public void SelectBoom()
    {
        selectedWeapons += 1;
        BoomCnt.boomCnt = add(boomerange, boomAdd, boomSub, totalWeapons, BoomCnt.boomCnt, selectedWeapons);     
        InputField inputField = allInputFields.Find((inputField) => inputField.gameObject.name.Equals("BoomerangInput"));
        inputField.SetTextWithoutNotify(BoomCnt.boomCnt.ToString());
    }
    public void RemoveBoom()
    {
        BoomCnt.boomCnt = sub(boomerange, boomAdd, boomSub, totalWeapons, BoomCnt.boomCnt, selectedWeapons);
        selectedWeapons -= 1;
        InputField inputField = allInputFields.Find((inputField) => inputField.gameObject.name.Equals("BoomerangInput"));
        inputField.SetTextWithoutNotify(BoomCnt.boomCnt.ToString());
    }


    //------------------------------ Magnet ------------------------------------
    public void SelectMag()
    {
        selectedWeapons += 1;
        MagnetCnt.magCnt = add(magnet, magAdd, magSub, totalWeapons, MagnetCnt.magCnt, selectedWeapons);
        InputField inputField = allInputFields.Find((inputField) => inputField.gameObject.name.Equals("MagnetInput"));
        inputField.SetTextWithoutNotify(MagnetCnt.magCnt.ToString());
    }
    public void RemoveMag()
    {
        MagnetCnt.magCnt = sub(magnet, magAdd, magSub, totalWeapons, MagnetCnt.magCnt, selectedWeapons);
        selectedWeapons -= 1;
        InputField inputField = allInputFields.Find((inputField) => inputField.gameObject.name.Equals("MagnetInput"));
        inputField.SetTextWithoutNotify(MagnetCnt.magCnt.ToString());
    }


    //------------------------------ Bomb ------------------------------------
    public void SelectBomb()
    {
        // limit the bomb selecting amount to at most 4
        if(BombCnt.bombCnt < 4) {
            selectedWeapons += 1;
            BombCnt.bombCnt = add(bomb, bombAdd, bombSub, totalWeapons, BombCnt.bombCnt, selectedWeapons);
        }
        if(BombCnt.bombCnt > 3){
            bomb.interactable = false;
            bombAdd.interactable = false;
        }
        InputField inputField = allInputFields.Find((inputField) => inputField.gameObject.name.Equals("BombInput"));
        inputField.SetTextWithoutNotify(BombCnt.bombCnt.ToString());
    }
    public void RemoveBomb()
    {
        BombCnt.bombCnt = sub(bomb, bombAdd, bombSub, totalWeapons, BombCnt.bombCnt, selectedWeapons);
        selectedWeapons -= 1;
        InputField inputField = allInputFields.Find((inputField) => inputField.gameObject.name.Equals("BombInput"));
        inputField.SetTextWithoutNotify(BombCnt.bombCnt.ToString());
    }

    //------------------------------ Ray ------------------------------------
    public void SelectRay()
    {
        // limit the ray selecting amount to at most 2
        if (RayCnt.rayCnt < 2) {
            selectedWeapons += 1;
            RayCnt.rayCnt = add(ray, rayAdd, raySub, totalWeapons, RayCnt.rayCnt, selectedWeapons);
        }
        if (RayCnt.rayCnt > 1){
            ray.interactable = false;
            rayAdd.interactable = false;
        }
        InputField inputField = allInputFields.Find((inputField) => inputField.gameObject.name.Equals("RayInput"));
        inputField.SetTextWithoutNotify(RayCnt.rayCnt.ToString());
    }
    public void RemoveRay()
    {
        RayCnt.rayCnt = sub(ray, rayAdd, raySub, totalWeapons, RayCnt.rayCnt, selectedWeapons);
        selectedWeapons -= 1;
        InputField inputField = allInputFields.Find((inputField) => inputField.gameObject.name.Equals("RayInput"));
        inputField.SetTextWithoutNotify(RayCnt.rayCnt.ToString());
    }

    //------------------------------ Grenade ------------------------------------
    public void SelectGrenade()
    {
        // limit the grenade selecting amout to at most 1
        if (GrenadeCnt.grCnt < 1)
        {
            selectedWeapons += 1;
            GrenadeCnt.grCnt = add(grenade, grenadeAdd, grenadeSub, totalWeapons, GrenadeCnt.grCnt, selectedWeapons);
        }
        if (GrenadeCnt.grCnt > 0)
        {
            grenade.interactable = false;
            grenadeAdd.interactable = false;
        }
        InputField inputField = allInputFields.Find((inputField) => inputField.gameObject.name.Equals("GrenadeInput"));
        inputField.SetTextWithoutNotify(GrenadeCnt.grCnt.ToString());
    }
    public void RemoveGrenade()
    {
        GrenadeCnt.grCnt = sub(grenade, grenadeAdd, grenadeSub, totalWeapons, GrenadeCnt.grCnt, selectedWeapons);
        selectedWeapons -= 1;
        InputField inputField = allInputFields.Find((inputField) => inputField.gameObject.name.Equals("GrenadeInput"));
        inputField.SetTextWithoutNotify(GrenadeCnt.grCnt.ToString());
    }

    //------------------------------ helper method ---------------------
    private int add(Button image, Button add, Button sub, int total, int count, int selected)
    {
        count += 1;
        image.interactable = (selected < total);
        add.interactable = (selected < total);
        sub.interactable = (count > 0);
        buySystem.incrementAdded();
        return count;
    }
    private int sub(Button image, Button add, Button sub, int total, int count, int selected)
    {
        image.interactable = (selected < total + 1);
        add.interactable = (selected < total + 1);
        count -= 1;
        sub.interactable = (count > 0);
        buySystem.decremenentAdded();
        return count;
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

        selectedWeapons = currentTotalSelected + currentWeaponCount >= totalWeapons ? totalWeapons : currentTotalSelected + currentWeaponCount;
        image.interactable = currentTotalSelected < maxWeapons;
        add.interactable = currentTotalSelected < maxWeapons;
        sub.interactable = currentWeaponCount > 0;
        return currentWeaponCount;
    }

    public void OnInput_UpdateCount(string newCount)
    {
        InputField activeInputField = allInputFields.Find((inputField) =>
        {
            return inputField.isFocused;
        });
        GameObject parentOfInputField = activeInputField.transform.parent.gameObject;
        // this is dependent on the order of the GameObjects in the inspector hierarchy
        // better approach is to refactor buying system, for now this should suffice
        // [Sprite, Add, Sub]
        Button[] displayButtons = parentOfInputField.GetComponentsInChildren<Button>();
        int count = 0;
        int.TryParse(newCount, out count);
        switch (parentOfInputField.name)
        {
            case "DeforestorPanel":
                count = setCount(displayButtons[0], displayButtons[1], displayButtons[2], totalWeapons, ref DeforestorCnt.deCnt, ref selectedWeapons, count);
                break;
            case "MineralExtractorPanel":
                count = setCount(displayButtons[0], displayButtons[1], displayButtons[2], totalWeapons, ref MECnt.meCnt, ref selectedWeapons, count);
                // undo the total number input
                selectedWeapons -= count;
                // calculate whether to add 2 or 0
                selectedWeapons += count >= 3 ? 3 : count;
                MECnt.meCnt = count >= 3 ? 3 : count;
                if (MECnt.meCnt > 3)
                {
                    bomb.interactable = false;
                    bombAdd.interactable = false;
                }
                count = MECnt.meCnt;
                break;
            case "ArrowPanel":
                count = setCount(displayButtons[0], displayButtons[1], displayButtons[2], totalWeapons, ref ArrowCnt.arrCnt, ref selectedWeapons, count);
                break;
            case "BallPanel":
                count = setCount(displayButtons[0], displayButtons[1], displayButtons[2], totalWeapons, ref BallCnt.ballCnt, ref selectedWeapons, count);
                break;
            case "BoomerangPanel":
                count = setCount(displayButtons[0], displayButtons[1], displayButtons[2], totalWeapons, ref BoomCnt.boomCnt, ref selectedWeapons, count);
                break;
            case "MagnetPanel":
                count = setCount(displayButtons[0], displayButtons[1], displayButtons[2], totalWeapons, ref MagnetCnt.magCnt, ref selectedWeapons, count);
                break;
            case "BombPanel":
                count = setCount(displayButtons[0], displayButtons[1], displayButtons[2], totalWeapons, ref BombCnt.bombCnt, ref selectedWeapons, count);
                // undo the total number input
                selectedWeapons -= count;
                // calculate whether to add 2 or 0
                selectedWeapons += count >= 4 ? 4 : count;
                BombCnt.bombCnt = count >= 4 ? 4 : count;
                if (BombCnt.bombCnt > 4)
                {
                    bomb.interactable = false;
                    bombAdd.interactable = false;
                }
                count = BombCnt.bombCnt;
                break;
            case "RayPanel":
                count = setCount(displayButtons[0], displayButtons[1], displayButtons[2], totalWeapons, ref RayCnt.rayCnt, ref selectedWeapons, count);
                // undo the total number input
                selectedWeapons -= count;
                // calculate whether to add 2 or 0
                selectedWeapons += count >= 2 ? 2 : count;
                RayCnt.rayCnt = count >= 2 ? 2 : count;
                if (RayCnt.rayCnt > 2)
                {
                    ray.interactable = false;
                    rayAdd.interactable = false;
                }
                count = RayCnt.rayCnt;
                break;
            case "GrenadePanel":
                count = setCount(displayButtons[0], displayButtons[1], displayButtons[2], totalWeapons, ref GrenadeCnt.grCnt, ref selectedWeapons, count);
                // undo the total number input
                selectedWeapons -= count;
                // calculate whether to add 1 or 0
                selectedWeapons += count >= 1 ? 1 : 0;
                GrenadeCnt.grCnt = count >= 1 ? 1 : 0;
                if (GrenadeCnt.grCnt > 0)
                {
                    grenade.interactable = false;
                    grenadeAdd.interactable = false;
                }
                count = GrenadeCnt.grCnt;
                break;
            default:
                break;
        }

        activeInputField.SetTextWithoutNotify(count.ToString());
    }

    private void activateAllAdds(bool condition)
    {
        deforestor.interactable = condition;
        deAdd.interactable = condition;
        arrow.interactable = condition;
        arrAdd.interactable = condition;
        ball.interactable = condition;
        ballAdd.interactable = condition;
        boomerange.interactable = condition;
        boomAdd.interactable = condition;
        magnet.interactable = condition;
        magAdd.interactable = condition;
        
        if(BombCnt.bombCnt < 4){
            bomb.interactable = condition;
            bombAdd.interactable = condition;
        }

        if (MECnt.meCnt < 3) {
            mExtractor.interactable = condition;
            meAdd.interactable = condition;
        }
        
        if (RayCnt.rayCnt < 2) {
            ray.interactable = condition;
            rayAdd.interactable = condition;
        }
        
        if (GrenadeCnt.grCnt < 1)
        {
            grenade.interactable = condition;
            grenadeAdd.interactable = condition;
        }
        // grenade.interactable = condition;
        // grenadeAdd.interactable = condition;
    }
    private void activeAddsForRoundOne(bool condition)
    {
        deforestor.interactable = condition;
        deAdd.interactable = condition;
        arrow.interactable = condition;
        arrAdd.interactable = condition;
        boomerange.interactable = condition;
        boomAdd.interactable = condition;
    }
    private void activeAddsForRoundTwo(bool condition)
    {
        deforestor.interactable = condition;
        deAdd.interactable = condition;
        arrow.interactable = condition;
        arrAdd.interactable = condition;
        boomerange.interactable = condition;
        boomAdd.interactable = condition;
        ball.interactable = condition;
        ballAdd.interactable = condition;
        magnet.interactable = condition;
        magAdd.interactable = condition;

        if(BombCnt.bombCnt < 4){
            bomb.interactable = condition;
            bombAdd.interactable = condition;
        }

        if (MECnt.meCnt < 3) {
            mExtractor.interactable = condition;
            meAdd.interactable = condition;
        }
    }

    private void activateAllSubs(bool condition)
    {
        deSub.interactable = condition;
        meSub.interactable = condition;
        arrSub.interactable = condition;
        ballSub.interactable = condition;
        boomSub.interactable = condition;
        magSub.interactable = condition;
        bombSub.interactable = condition;
        raySub.interactable = condition;
        grenadeSub.interactable = condition;
    }

    public void SaveAlienInventory()
    {
        List<GameObject> alienInventory = new List<GameObject>();
        if (DeforestorCnt.deCnt > 0)
        {
            alienInventory.Add(CreateSprite(DeforestorCnt.deCnt, "deforestor"));
        }
        if (MECnt.meCnt > 0)
        {
            alienInventory.Add(CreateSprite(MECnt.meCnt, "mineral"));
        }
        if (ArrowCnt.arrCnt > 0)
        {
            alienInventory.Add(CreateSprite(ArrowCnt.arrCnt, "arrow"));
        }
        if (BallCnt.ballCnt > 0)
        {
            alienInventory.Add(CreateSprite(BallCnt.ballCnt, "ball"));
        }
        if (BoomCnt.boomCnt > 0)
        {
            alienInventory.Add(CreateSprite(BoomCnt.boomCnt, "boomerang"));
        }
        if (MagnetCnt.magCnt > 0)
        {
            alienInventory.Add(CreateSprite(MagnetCnt.magCnt, "oregon"));
        }
        if (BombCnt.bombCnt > 0)
        {
            alienInventory.Add(CreateSprite(BombCnt.bombCnt, "bomb"));
        }
        if (RayCnt.rayCnt > 0)
        {
            alienInventory.Add(CreateSprite(RayCnt.rayCnt, "ray"));
        }
        if (GrenadeCnt.grCnt > 0)
        {
            alienInventory.Add(CreateSprite(GrenadeCnt.grCnt, "grenade"));
        }
        alienInventory.Add(CreateSprite(GrappleCnt.grappleCnt, "grapple"));
        GameManager.globalManager.SetAlienInventory(alienInventory);
    }

    private GameObject CreateSprite(int count, string name)
    {
        GameObject newGameObject = new GameObject();
        Item newItem = newGameObject.AddComponent<Item>();
        newItem.SetCount(count);
        Button itemButton = null;
        switch (name)
        {
            case "deforestor":
                itemButton = deforestor;
                newItem.SetItemName("Deforestor");
                newItem.SetMessage("Deforestor - Strong against Wood! ");
                break;
            case "mineral":
                itemButton = mExtractor;
                newItem.SetItemName("Mineral Extractor");
                newItem.SetMessage("Mineral Extractor - Strong against Stone! ");
                break;
            case "arrow":
                itemButton = arrow;
                newItem.SetItemName("Arrow");
                newItem.SetMessage("Arrow - Tears apart fabrics! ");
                break;
            case "ball":
                itemButton = ball;
                newItem.SetItemName("Bowling Ball ");
                newItem.SetMessage("Bowling Ball - Strong against glass & stone! ");
                break;
            case "boomerang":
                itemButton = boomerange;
                newItem.SetItemName("Boomerang");
                newItem.SetMessage("Boomerang - Strong against glass & fabric! ");
                break;
            case "oregon":
                itemButton = magnet;
                newItem.SetItemName("Oregon Man");
                newItem.SetMessage("Oregon Man - Greediness destroys wood, fabrics, and glass");
                break;
            case "bomb":
                itemButton = bomb;
                newItem.SetItemName("Bomb ");
                newItem.SetMessage("Bomb - Destroys everything but metals! ");
                break;
            case "ray":
                itemButton = ray;
                newItem.SetItemName("Raygun ");
                newItem.SetMessage("Raygun - Strong against everything except glass! ");
                break;
            case "grenade":
                itemButton = grenade;
                newItem.SetItemName("Alien Grenade ");
                newItem.SetMessage("Alien Grenade - Annihilates everything! ");
                break;
            case "grapple":
                itemButton = grapple;
                newItem.SetItemName("Grapple Hook ");
                newItem.SetMessage("Grapple Hook - Nabs the Art! ");
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
