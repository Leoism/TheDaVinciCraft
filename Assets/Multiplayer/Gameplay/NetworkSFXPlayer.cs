using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
public class NetworkSFXPlayer : MonoBehaviour
{
  private AudioSource audioSource = null;
  public AudioClip raySFX = null;
  public AudioClip boomerangSFX = null;
  public AudioClip catapultSFX = null;
  // Update is called once per frame
  void Start()
  {
    // Only the human should be playing network audio
    if (!GameManager.globalManager.isOnlineMode || !PhotonNetwork.IsMasterClient) return;
    audioSource = GetComponent<AudioSource>();
  }

  public void PlayClip(string clipName)
  {
    audioSource.Stop();
    audioSource.loop = false;
    // Only the human should be playing network audio
    if (!GameManager.globalManager.isOnlineMode || !PhotonNetwork.IsMasterClient) return;
    switch (clipName)
    {
      case "Boomerang":
        audioSource.loop = true;
        audioSource.clip = boomerangSFX;
        break;
      case "Ray":
        audioSource.clip = raySFX;
        break;
      case "Catapult":
        audioSource.clip = catapultSFX;
        break;
    }
    audioSource.Play();
  }

  public void Stop()
  {
    audioSource.Stop();
  }
}
