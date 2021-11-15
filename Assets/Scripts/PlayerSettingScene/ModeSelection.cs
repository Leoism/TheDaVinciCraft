using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ModeSelection : MonoBehaviour
{
    private int currentMode;
    [SerializeField] private Button prevButton;
    [SerializeField] private Button nextButton;
    private void Awake()
    {   
        SelectMode(0);
    }
    private void SelectMode(int _index)
    {
        prevButton.interactable = (_index != 0);
        nextButton.interactable = (_index != transform.childCount-1);
        // Debug.Log(transfrom.childCount);
        for(int i = 0; i < transform.childCount; i++){
            transform.GetChild(i).gameObject.SetActive(i == _index);
        }

        switch (transform.GetChild(_index).gameObject.name)
        {
        case "Short":
            GameManager.globalManager.SetGameType(GameMode.SHORT);
            break;
        case "Standard":
            GameManager.globalManager.SetGameType(GameMode.STANDARD);
            break;
        case "Long":
            GameManager.globalManager.SetGameType(GameMode.LONG);
            break;
        default:
            break;
        }
    }

    public void ChangeMode(int _change) 
    {
        currentMode += _change;
        SelectMode(currentMode);
    }
}
