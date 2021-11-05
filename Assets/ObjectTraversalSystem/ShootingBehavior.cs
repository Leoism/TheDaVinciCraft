using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingBehavior : MonoBehaviour
{
  // projectile settings
  public GameObject projectilePrefab = null;
  public GameObject trajectoryPointPrefab = null;
  public Transform projectileSpawnPoint = null;
  public GameObject powerBar = null;
  private GameObject[] trajectoryPoints;
  private int trajectoryPointCount = 20;
  private float trajectoryPointSpace = 0.0625f;
  // mouse settings
  private bool isAiming = false;
  // shooting settings
  private Vector3 beginDrag;
  private Vector3 endDrag;
  private Vector3 direction;
  private float shootStrength = 64f;
  private float dragStrength = 1f;
  private float dragCap = 40f;

  // Start is called before the first frame update
  void Start()
  {
    Debug.Assert(projectilePrefab != null);
    Debug.Assert(trajectoryPointPrefab != null);
    Debug.Assert(projectileSpawnPoint != null);
    trajectoryPoints = new GameObject[trajectoryPointCount];
    for (int i = 0; i < trajectoryPointCount; i++)
    {
      trajectoryPoints[i] = Instantiate(trajectoryPointPrefab, projectileSpawnPoint.position, Quaternion.identity);
      trajectoryPoints[i].GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, (trajectoryPointCount - i) / (float)trajectoryPointCount);
    }
  }

  // Update is called once per frame
  void Update()
  {
    DetectOnAim();
    OnAim();
  }

  void DetectOnAim()
  {
    if (Input.GetMouseButtonDown(0))
    {
      isAiming = true;
      beginDrag = Camera.main.ScreenToWorldPoint(Input.mousePosition);
      SetTrajectoryPointStatus(true);
    }

    if (Input.GetMouseButtonUp(0))
    {
      isAiming = false;
      CalculateShootSettings();
      Shoot();
      SetTrajectoryPointStatus(false);
    }
  }

  void OnAim()
  {
    if (!isAiming) return;
    CalculateShootSettings();
    SetPowerBar(dragStrength);
    for (int i = 0; i < trajectoryPointCount; i++)
    {
      trajectoryPoints[i].transform.position = TrajectoryPointPosition(i * trajectoryPointSpace);
    }
  }

  void CalculateShootSettings()
  {
    endDrag = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    direction = (beginDrag - endDrag).normalized;
    // calculate the drag distance and cap it, if higher than cap than max strength can only be 1
    dragStrength = Mathf.Min(Mathf.Abs(Vector3.Distance(beginDrag, endDrag)) / dragCap, 1);
  }

  void Shoot()
  {
    GameObject newProjectile = Instantiate(projectilePrefab, projectileSpawnPoint.position, projectileSpawnPoint.rotation);
    newProjectile.transform.up = direction;
    newProjectile.GetComponent<Rigidbody2D>().velocity = direction * shootStrength * dragStrength;
  }

  void SetTrajectoryPointStatus(bool isOn)
  {
    for (int i = 0; i < trajectoryPointCount; i++)
    {
      trajectoryPoints[i].SetActive(isOn);
    }
  }

  Vector2 TrajectoryPointPosition(float time)
  {
    Vector2 pos = (Vector2)projectileSpawnPoint.position + ((Vector2)direction * shootStrength * dragStrength * time) + 0.5f * Physics2D.gravity * (time * time);
    return pos;
  }

  void SetPowerBar(float s)
  {
    powerBar.transform.localScale = new Vector3(s, 1, 1);
  }
}
