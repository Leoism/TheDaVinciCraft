using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
    }
    void Start()
    {
        totalWeapons = 6;
        DeforestorCnt.deCnt = 0;
        MECnt.meCnt = 0;
        ArrowCnt.arrCnt = 0;
        BallCnt.ballCnt = 0;
        BoomCnt.boomCnt = 0;
        MagnetCnt.magCnt = 0;
        BombCnt.bombCnt = 0;
        RayCnt.rayCnt = 0;
        GrenadeCnt.grCnt = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (selectedWeapons >= totalWeapons) {
            activateAllAdds(false);
        }

        if (selectedWeapons < totalWeapons) {
            activateAllAdds(true);
        }

        if (CountDownTimer.isTimeUp == true) {
            activateAllAdds(false);
            activateAllSubs(false);
        }
    }

    //------------------------------ Deforestor -----------------------------
    public void SelectDeforestor()
    {
        selectedWeapons += 1;
        DeforestorCnt.deCnt = add(deforestor, deAdd, deSub, totalWeapons, DeforestorCnt.deCnt, selectedWeapons);
    }
    public void RemoveDeforestor()
    {
        DeforestorCnt.deCnt  = sub(deforestor, deAdd, deSub, totalWeapons, DeforestorCnt.deCnt , selectedWeapons);
        selectedWeapons -= 1;
    }

    //------------------------------ Mineral Extractor ------------------------
    public void SelectMExtractor()
    {
        selectedWeapons += 1;
        MECnt.meCnt = add(mExtractor, meAdd, meSub, totalWeapons, MECnt.meCnt, selectedWeapons);
    }
    public void RemoveMExtractor()
    {
        MECnt.meCnt = sub(mExtractor, meAdd, meSub, totalWeapons, MECnt.meCnt, selectedWeapons);
        selectedWeapons -= 1;
    }

    //------------------------------ Arrow ------------------------------------
    public void SelectArrow()
    {
        selectedWeapons += 1;
        ArrowCnt.arrCnt = add(arrow, arrAdd, arrSub, totalWeapons, ArrowCnt.arrCnt, selectedWeapons);
    }
    public void RemoveArrow()
    {
        ArrowCnt.arrCnt = sub(arrow, arrAdd, arrSub, totalWeapons, ArrowCnt.arrCnt, selectedWeapons);
        selectedWeapons -= 1;
    }

    //------------------------------ Ball ------------------------------------
    public void SelectBall()
    {
        selectedWeapons += 1;
        BallCnt.ballCnt = add(ball, ballAdd, ballSub, totalWeapons, BallCnt.ballCnt, selectedWeapons);
    }
    public void RemoveBall()
    {
        BallCnt.ballCnt = sub(ball, ballAdd, ballSub, totalWeapons, BallCnt.ballCnt, selectedWeapons);
        selectedWeapons -= 1;
    }


    //------------------------------ Boomerange ------------------------------------
    public void SelectBoom()
    {
        selectedWeapons += 1;
        BoomCnt.boomCnt = add(boomerange, boomAdd, boomSub, totalWeapons, BoomCnt.boomCnt, selectedWeapons);
    }
    public void RemoveBoom()
    {
        BoomCnt.boomCnt = sub(boomerange, boomAdd, boomSub, totalWeapons, BoomCnt.boomCnt, selectedWeapons);
        selectedWeapons -= 1;
    }


    //------------------------------ Magnet ------------------------------------
    public void SelectMag()
    {
        selectedWeapons += 1;
        MagnetCnt.magCnt = add(magnet, magAdd, magSub, totalWeapons, MagnetCnt.magCnt, selectedWeapons);
    }
    public void RemoveMag()
    {
        MagnetCnt.magCnt = sub(magnet, magAdd, magSub, totalWeapons, MagnetCnt.magCnt, selectedWeapons);
        selectedWeapons -= 1;
    }


    //------------------------------ Bomb ------------------------------------
    public void SelectBomb()
    {
        selectedWeapons += 1;
        BombCnt.bombCnt = add(bomb, bombAdd,bombSub, totalWeapons, BombCnt.bombCnt, selectedWeapons);
    }
    public void RemoveBomb()
    {
        BombCnt.bombCnt = sub(bomb, bombAdd,bombSub, totalWeapons, BombCnt.bombCnt, selectedWeapons);
        selectedWeapons -= 1;
    }

    //------------------------------ Ray ------------------------------------
    public void SelectRay()
    {
        selectedWeapons += 1;
        RayCnt.rayCnt = add(ray, rayAdd, raySub, totalWeapons, RayCnt.rayCnt, selectedWeapons);
    }
    public void RemoveRay()
    {
        RayCnt.rayCnt = sub(ray, rayAdd, raySub, totalWeapons, RayCnt.rayCnt, selectedWeapons);
        selectedWeapons -= 1;
    }

    //------------------------------ Grenade ------------------------------------
    public void SelectGrenade()
    {
        selectedWeapons += 1;
        GrenadeCnt.grCnt = add(grenade,grenadeAdd,grenadeSub, totalWeapons, GrenadeCnt.grCnt, selectedWeapons);
    }
    public void RemoveGrenade()
    {
        GrenadeCnt.grCnt = sub(grenade,grenadeAdd,grenadeSub, totalWeapons, GrenadeCnt.grCnt, selectedWeapons);
        selectedWeapons -= 1;
    }

    //------------------------------ helper method ---------------------
    private int add(Button image, Button add, Button sub, int total, int count, int selected)
    {
        count += 1;
        image.interactable = (selected < total);
        add.interactable = (selected < total);
        sub.interactable = (count > 0);
        return count;
    }
    private int sub(Button image, Button add, Button sub, int total, int count, int selected)
    {
        image.interactable = (selected < total + 1);
        add.interactable = (selected < total + 1);
        count -= 1;
        sub.interactable = (count > 0);
        return count;
    }

    private void activateAllAdds(bool condition)
    {
        deforestor.interactable = condition;
        deAdd.interactable = condition;
        mExtractor.interactable = condition;
        meAdd.interactable = condition;
        arrow.interactable = condition;
        arrAdd.interactable = condition;
        ball.interactable = condition;
        ballAdd.interactable = condition;
        boomerange.interactable = condition;
        boomAdd.interactable = condition;
        magnet.interactable = condition;
        magAdd.interactable = condition;
        bomb.interactable = condition;
        bombAdd.interactable = condition;
        ray.interactable = condition;
        rayAdd.interactable = condition;
        grenade.interactable = condition;
        grenadeAdd.interactable = condition;
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
        if (RayCnt.rayCnt > 0)
        {
            alienInventory.Add(CreateSprite(RayCnt.rayCnt, "ray"));
        }
        if (GrenadeCnt.grCnt > 0)
        {
            alienInventory.Add(CreateSprite(GrenadeCnt.grCnt, "grenade"));
        }
        GameManager.globalManager.SetAlienInventory(alienInventory);
    }

    private GameObject CreateSprite(int count, string name)
    {
        GameObject newGameObject = new GameObject();
        Item newItem = newGameObject.AddComponent<Item>();
        newItem.SetCount(count);
        Button itemButton = null;
        switch(name) {
            case "deforestor": 
                itemButton = deforestor;
                break;
            case "mineral":
                itemButton = mExtractor;
                break;
            case "arrow":
                itemButton = arrow;
                break;
            case "ball":
                itemButton = ball;
                break;
            case "boomerang":
                itemButton = boomerange;
                break;
            case "ray":
                itemButton = ray;
                break;
            case "grenade":
                itemButton = grenade;
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
