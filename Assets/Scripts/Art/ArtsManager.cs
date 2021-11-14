using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class ArtsManager : MonoBehaviour
{
    public ArtDB artsDB;
    public Text artNameText;
    public SpriteRenderer artWorkSprite;
    private int selectedArt = 0;
    void Start()
    {
        if(!PlayerPrefs.HasKey("selectedArt")){
            selectedArt = 0;
        } else {
            Load();
        }

        UpdateArt(selectedArt);
    }

    public void ChangeScene(string scene)
    {
        SceneManager.LoadScene(scene);
    }
    public void Next()
    {
        selectedArt += 1;
        
        if(selectedArt >= artsDB.ArtsCount){
            selectedArt = 0;
        }

        UpdateArt(selectedArt);
        Save();
    }

    public void Prev()
    {
        selectedArt -= 1;

        if(selectedArt < 0 ){
            selectedArt = artsDB.ArtsCount - 1;
        }

        UpdateArt(selectedArt);
        Save();
    }

    private void UpdateArt(int selectedArt)
    {
        Arts art = artsDB.GetArts(selectedArt);
        artWorkSprite.sprite = art.artSprite;
        artNameText.text = art.artName;
    }

    private void Load()
    {
        selectedArt = PlayerPrefs.GetInt("selectedArt"); 
    }
    private void Save()
    {
        PlayerPrefs.SetInt("selectedArt", selectedArt);
    }

}
