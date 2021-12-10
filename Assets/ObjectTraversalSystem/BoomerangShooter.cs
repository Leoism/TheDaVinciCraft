using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.EventSystems;
public class BoomerangShooter : MonoBehaviour
{
    public GameObject boomerangPrefab = null;
    public Transform spawnPoint = null;
    public GameObject trajectoryPointPrefab = null;
    private bool isFirstClick = true;
    private Vector3 firstClickPos;
    private Vector3 secondClickPos;
    // trajectory settings
    private int trajectoryPointCount = 15;
    public GameObject[] trajectoryPoints;
    public Gameplay gameplayScene = null;
    // Start is called before the first frame update
    void Start()
    {
        Debug.Assert(boomerangPrefab != null);
        Debug.Assert(spawnPoint != null);
        Debug.Assert(trajectoryPointPrefab != null);
        trajectoryPoints = new GameObject[trajectoryPointCount];
        for (int i = 0; i < trajectoryPointCount; i++)
        {
            trajectoryPoints[i] = Instantiate(trajectoryPointPrefab, spawnPoint.position, Quaternion.identity);
          //  trajectoryPoints[i].GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, (trajectoryPointCount - i) / (float)trajectoryPointCount);
        }
        SetTrajectoryPointStatus(false);
    }

    // Update is called once per frame
    void Update()
    {
        if ((EventSystem.current.IsPointerOverGameObject() &&
            EventSystem.current.currentSelectedGameObject != null &&
            EventSystem.current.currentSelectedGameObject.CompareTag("Button"))) 
        {
            return;
        }
       if (gameplayScene.IsItemEmpty()) { return; }

        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mousePos3d = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 mousePos = new Vector2(mousePos3d.x, mousePos3d.y);
            if (isFirstClick)
            {
                firstClickPos = mousePos;
                SetTrajectoryPointStatus(true);
            }
            else 
            {
                secondClickPos = mousePos;
                Shoot();
                SetTrajectoryPointStatus(false);
            }
            isFirstClick = !isFirstClick;
        }
        UpdateTrajectory();
    }

    void Shoot()
    {
        // cap boomerang speed multiplier at 1
        float multiplier = Mathf.Min(CalculateMaxDistance() / 150f, 1f);
        GameObject newProjectile = null;
        if (GameManager.globalManager.isOnlineMode)
        {
            newProjectile = PhotonNetwork.Instantiate("GamePlayScene/Projectile", spawnPoint.position, spawnPoint.rotation, 0, new object[] { "Boomerang" });
        }
        else
        {
            newProjectile = Instantiate(boomerangPrefab, spawnPoint.position, spawnPoint.rotation);
        }
        gameplayScene.UseItem();
        BoomerangBehavior bb = newProjectile.AddComponent<BoomerangBehavior>();
        bb.SetPoints(spawnPoint.position, firstClickPos, secondClickPos);
        bb.SetLifespan(5f * multiplier);
    }

    void SetTrajectoryPointStatus(bool isOn)
    {
        for (int i = 0; i < trajectoryPointCount; i++)
        {
            trajectoryPoints[i].SetActive(isOn);
        }
    }

    // find the largest distance
    float CalculateMaxDistance()
    {
        float d1 = Mathf.Abs(Vector3.Distance(firstClickPos, spawnPoint.position));
        float d2 = Mathf.Abs(Vector3.Distance(secondClickPos, spawnPoint.position));
        float d3 = Mathf.Abs(Vector3.Distance(secondClickPos, firstClickPos));
        return Mathf.Max(d1, Mathf.Max(d2, d3));
    }

    void UpdateTrajectory()
    {
        if (isFirstClick) return;
        float interval = 1f / trajectoryPointCount;
        for (int i = 0; i < trajectoryPointCount; i++)
        {
            trajectoryPoints[i].transform.position = CalculatePosition(i * interval, spawnPoint.position, firstClickPos, Camera.main.ScreenToWorldPoint(Input.mousePosition));
        }
    }

    Vector3 CalculatePosition(float completionPercent, Vector3 start, Vector3 point1, Vector3 point2)
    {
        // (1 - t)^3 * P0
        Vector3 part1 = Mathf.Pow(1 - completionPercent, 3) * start;
        // 3 * (1 - t)^2 * t * P1
        Vector3 part2 = 3 * Mathf.Pow(1 - completionPercent, 2) * completionPercent * point1;
        // 3 * (1 - t) * t^2 * P2
        Vector3 part3 = 3 * (1 - completionPercent) * Mathf.Pow(completionPercent, 2) * point2;
        // t^3 * P3 ||| P3 is P0 since a boomerang has to come back
        Vector3 part4 = Mathf.Pow(completionPercent, 3) * start;
        return part1 + part2 + part3 + part4;
    }
}
