using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayShooter : MonoBehaviour
{
  public GameObject rayPrefab = null;
  public Transform spawnPoint = null;
  public Gameplay gameplayScene = null;
  // Start is called before the first frame update
  void Start()
  {
    Debug.Assert(spawnPoint != null);
  }

  // Update is called once per frame
  void Update()
  {
    if (rayPrefab == null) return;
    if (Input.GetMouseButtonDown(0))
    {
      Shoot();
    }
  }

  void Shoot()
  {
    GameObject newRay = Instantiate(rayPrefab, spawnPoint.position, spawnPoint.rotation);
    RayBehavior rb = newRay.AddComponent<RayBehavior>();
    rb.SetTarget(Camera.main.ScreenToWorldPoint(Input.mousePosition));

    gameplayScene.UseItem();
  }
}
