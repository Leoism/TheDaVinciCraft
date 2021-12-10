using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.EventSystems;

public class RayShooter : MonoBehaviour
{
    public GameObject rayPrefab = null;
    public Transform spawnPoint = null;
    public GameObject aimTarget = null;
    public Gameplay gameplayScene = null;
    // Start is called before the first frame update
    void Start()
    {
        Debug.Assert(rayPrefab != null);
        Debug.Assert(spawnPoint != null);
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
        if (Input.GetMouseButtonDown(0))
        {
            Shoot();
        }
    }

    void Shoot()
    {
        GameObject newRay;
        // TODO: network
        if (GameManager.globalManager.isOnlineMode)
        {
            newRay = PhotonNetwork.Instantiate("GamePlayScene/Ray", spawnPoint.position, spawnPoint.rotation);
        }
        else
        {
            newRay = Instantiate(rayPrefab, spawnPoint.position, spawnPoint.rotation);
        }
        gameplayScene.UseItem();
        RayBehavior rb = newRay.AddComponent<RayBehavior>();
        rb.SetTarget(Camera.main.ScreenToWorldPoint(Input.mousePosition));
    }

    public void Deactivate()
    {
        aimTarget.SetActive(false);
    }
    public void Activate()
    {
        aimTarget.SetActive(true);
    }
}
