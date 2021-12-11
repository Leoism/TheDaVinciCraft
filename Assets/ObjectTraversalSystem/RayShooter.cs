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
    public AudioClip shootingSound = null;
    private AudioSource audioSource = null;
    // Start is called before the first frame update
    void Start()
    {
        Debug.Assert(rayPrefab != null);
        Debug.Assert(spawnPoint != null);
        Debug.Log("hi");
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (gameplayScene.IsItemEmpty()) return;
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
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        if (GameManager.globalManager.isOnlineMode)
        {
            newRay = PhotonNetwork.Instantiate("GamePlayScene/Ray", spawnPoint.position, spawnPoint.rotation, 0, new object[] { mousePos });
        }
        else
        {
            newRay = Instantiate(rayPrefab, spawnPoint.position, spawnPoint.rotation);
            RayBehavior rb = newRay.AddComponent<RayBehavior>();
            rb.SetTarget(mousePos);
        }
        gameplayScene.UseItem();
        audioSource.Play();
    }

    public void Deactivate()
    {
        aimTarget.SetActive(false);
        enabled = false;
    }
    public void Activate()
    {
        aimTarget.SetActive(true);
        enabled = true;
    }
}
