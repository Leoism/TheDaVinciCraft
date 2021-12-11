using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
public class BoomerangBehavior : MonoBehaviour
{
    public AudioSource audioSource;
    public PhotonView canvasPhotonView;
    // bezier curve settings
    private Vector3 start;
    private Vector3 point1;
    private Vector3 point2;
    // boomerang settings
    private float startTime = 0f;
    private float lifeSpan = 5f; // 5 seconds

    void Start()
    {
        startTime = Time.time;
        audioSource.loop = true;
        audioSource.Stop();
        audioSource.Play();
        // Only the alien can send audio
        if (GameManager.globalManager.isOnlineMode && !PhotonNetwork.IsMasterClient)
        {
            canvasPhotonView.RPC("RPC_StopAudio", RpcTarget.Others);
            canvasPhotonView.RPC("RPC_PlayBoomerang", RpcTarget.Others);
        }
    }
    // Update is called once per frame
    void Update()
    {
        float currTime = (Time.time - startTime) / lifeSpan;
        if (currTime >= 1)
        {
            if (GameManager.globalManager.isOnlineMode)
            {
                StopPlaying();
                PhotonNetwork.Destroy(GetComponent<PhotonView>());
            }
            else
            {
                StopPlaying();
                Destroy(gameObject);
            }
        }
        transform.position = CalculatePosition(currTime);
    }

    public void SetPoints(Vector3 p0, Vector3 p1, Vector3 p2)
    {
        start = p0;
        point1 = p1;
        point2 = p2;
    }

    public void SetLifespan(float ls)
    {
        lifeSpan = ls;
    }

    // Calculates the boomerang position based on the cubic bezier curves formula
    private Vector3 CalculatePosition(float completionPercent)
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

    private void StopPlaying()
    {
        audioSource.loop = false;
        audioSource.Stop();
        // Only the alien should send projectile audio updates
        if (GameManager.globalManager.isOnlineMode && !PhotonNetwork.IsMasterClient)
        {
            canvasPhotonView.RPC("RPC_StopAudio", RpcTarget.Others);
        }
    }
}
