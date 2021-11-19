using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class Artifact : MonoBehaviour
{
    public ArtDB artsDB;
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
    private void UpdateArt(int selectedArt)
    {
        Arts art = artsDB.GetArts(selectedArt);
        artWorkSprite.sprite = art.artSprite;
        artWorkSprite.transform.localScale = new Vector2(art.artSprite.bounds.size.x * 5, art.artSprite.bounds.size.y * 5);
    }
    
    private void Load()
    {
        selectedArt = PlayerPrefs.GetInt("selectedArt"); 
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Weapon"))
        {
            SceneManager.LoadScene("AlienWin");
            Destroy(this.gameObject);
        }
    }
}
