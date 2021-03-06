using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Photon.Pun;
public class Alienship : MonoBehaviour
{
    bool isInitialized = false;
    private float currentTime = 0;
    public GameObject powerBar = null;
    public ShootingBehavior shootingBehavior = null;
    public PhotonView canvasPhotonView = null;
    public GameObject grid = null;
    public Transform start = null;
    public Transform end = null;
    public Transform allTransforms = null;
    public float speed = 1.5f;
    private bool goUp = true;
    private int ticks = 0;
    private Vector3 oldPos; 
    
    void Start()
    {
        gameObject.SetActive(false);
        powerBar.SetActive(false);
        currentTime = 8;
    }
    public void Init()
    {
        if (isInitialized) return;
        oldPos = allTransforms.position;
        powerBar.SetActive(true);
        gameObject.SetActive(true);
        Vector3 windowCorner = Camera.main.ViewportToWorldPoint(new Vector3(0.8f, 0.85f, 1));
        transform.position = windowCorner;
        allTransforms.position = windowCorner;
        if (GameManager.globalManager.isOnlineMode && !PhotonNetwork.IsMasterClient)
        {
            PhotonView[] tilemapPhotonViews = grid.GetComponentsInChildren<PhotonView>();
            foreach (PhotonView tilemapPhotonView in tilemapPhotonViews)
            {
                tilemapPhotonView.RequestOwnership();
            }
        }
        if (oldPos.Equals(allTransforms.position))
        {
        ticks++;
        }
        if (ticks >= 5)
        {
        isInitialized = true;
        }
    }

    public void UnInit()
    {
        if (isInitialized)
        {
        gameObject.SetActive(false);
        }
    }
    void Update()
    {
        Bobble();
        // for addressing  the timer ran out for the alien 
        // but they were able to keep shooting and win.
        if (Timer.secRemaining <= 0)
        {
            if (GameManager.globalManager.isOnlineMode)
            {
                PhotonNetwork.LoadLevel("HumanWin");
            } else
            {
                SceneManager.LoadScene("HumanWin");
            }
        }
        
        // add timer about 10 sec
        if (GameManager.globalManager.alienInventory.TotalCount() <= 0)
        {
            // In online mode, we only want to run the timer on the alien side
            if (GameManager.globalManager.isOnlineMode && !PhotonNetwork.IsMasterClient)
            {
                CountTenSec();
            } else if (!GameManager.globalManager.isOnlineMode)
            {
                CountTenSec();
            }
        }
    }

    private void Bobble()
    {
        if (transform.position == start.position)
        {
            goUp = false;
        }
        else if (transform.position == end.position)
        {
            goUp = true;
        }
        transform.position = Vector3.MoveTowards(transform.position, (goUp ? start.position : end.position), speed * Time.smoothDeltaTime);
    }
    private void CountTenSec()
    {
        if (Timer.secRemaining < 10)
        {
            currentTime = Timer.secRemaining;
        }
        currentTime -= 1 * Time.deltaTime;
        // Debug.Log(currentTime);
        if (currentTime <= 0)
        {
            currentTime = 8;
            if (GameManager.globalManager.isOnlineMode)
            {
                canvasPhotonView.RPC("RPC_AlienOutOfWeapons", RpcTarget.MasterClient);
            } else
            {
                SceneManager.LoadScene("HumanWin");
            }
        }
    }
}
