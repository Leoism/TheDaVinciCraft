using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Alienship : MonoBehaviour
{
  bool isInitialized = false;
  public ShootingBehavior shootingBehavior = null;
  // Start is called before the first frame update
  void Start()
  {
    gameObject.SetActive(false);
  }
  public void Init()
  {
    if (isInitialized) return;
    gameObject.SetActive(true);
    Vector3 windowCorner = Camera.main.ViewportToWorldPoint(new Vector3(0.8f, 0.85f, 1));
    transform.position = windowCorner;
  }

  public void UnInit()
  {
    if (isInitialized)
    {
      gameObject.SetActive(false);
    }
  }
  void Update() {
    // add timer about 10 sec
    if (GameManager.globalManager.alienInventory.TotalCount() <= 0) {
      // float curTime = 10f;
      // while (curTime >= 0){
      //   curTime -= 1 * Time.deltaTime;
      // }
      // Debug.Log(curTime);
      // if (curTime == 0) {
      //   SceneManager.LoadScene("HumanWin");
      // }
      SceneManager.LoadScene("HumanWin");
    }
  }
}
