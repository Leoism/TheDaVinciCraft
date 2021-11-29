using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BgMusic : MonoBehaviour
{
    public static BgMusic BackgroundMusic;

    private void Awake() 
    {
        if (BackgroundMusic != null && BackgroundMusic != this) {
            Destroy(this.gameObject);
            return;
        }

        BackgroundMusic = this;
        DontDestroyOnLoad(this);
    }
}
