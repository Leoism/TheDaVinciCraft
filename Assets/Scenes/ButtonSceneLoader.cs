using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonSceneLoader : MonoBehaviour
{
  void Start() { }

  public void LoadScene(string scene)
  {
    Debug.Log("HI");
    SceneManager.LoadScene(scene);
  }
}
