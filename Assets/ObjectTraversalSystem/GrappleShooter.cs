using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.EventSystems;

public class GrappleShooter : MonoBehaviour
{
    public GameObject grapplePrefab = null;
    public Transform spawnPoint = null;
    public GameObject aimTarget = null;
    public Gameplay gameplayScene = null;
    public AudioClip shootingSound = null;
    public PhotonView canvasPhotonView = null;
    private AudioSource audioSource = null;
    // Start is called before the first frame update
    void Start()
    {
        Debug.Assert(grapplePrefab != null);
        Debug.Assert(spawnPoint != null);
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
        GameObject newGrapple;
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        if (GameManager.globalManager.isOnlineMode)
        {
            newGrapple = PhotonNetwork.Instantiate("GamePlayScene/Grapple", spawnPoint.position, spawnPoint.rotation, 0, new object[] { mousePos });
        }
        else
        {
            newGrapple = Instantiate(grapplePrefab, spawnPoint.position, spawnPoint.rotation);
            GrappleBehavior rb = newGrapple.AddComponent<GrappleBehavior>();
            rb.SetTarget(mousePos);
        }
        gameplayScene.UseItem();
        audioSource.Play();
        // Only the alien can send audio
        if (GameManager.globalManager.isOnlineMode && !PhotonNetwork.IsMasterClient)
        {
            canvasPhotonView.RPC("RPC_PlayGrapple", RpcTarget.Others);
        }
    }

    public void Deactivate()
    {
        if (!enabled) return;
        audioSource.clip = null;
        aimTarget.SetActive(false);
        enabled = false;
    }
    public void Activate()
    {
        if (enabled) return;
        audioSource.loop = false;
        audioSource.clip = shootingSound;
        aimTarget.SetActive(true);
        enabled = true;
    }
}
