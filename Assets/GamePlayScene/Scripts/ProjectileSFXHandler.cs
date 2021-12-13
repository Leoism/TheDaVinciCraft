using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileSFXHandler : MonoBehaviour
{
  public List<AudioClip> audioHandler;
  private AudioSource audioSource;
  private bool isPlaying = false;
  void Start()
  {
    audioSource = GetComponent<AudioSource>();
  }

  public void PlayClipAtIndex(int index)
  {
    if (isPlaying) return;
    audioSource.clip = audioHandler[index];
    audioSource.Play();
    isPlaying = true;
  }

  // Names must start with uppercase letter
  public void PlayClipByName(string name)
  {
    if (isPlaying) return;
    int index = -1;
    switch (name)
    {
      case "Bomb":
        index = 0;
        break;
      case "Mineral Extractor":
        index = 1;
        break;
      case "Oregon Man":
        index = 2;
        break;
      case "Arrow":
        index = 3;
        break;
    }

    if (index >= 0)
    {
      audioSource.clip = audioHandler[index];
      audioSource.Play();
      isPlaying = true;
    }
  }
}
