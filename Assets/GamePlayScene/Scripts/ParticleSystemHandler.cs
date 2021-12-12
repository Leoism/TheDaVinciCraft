using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleSystemHandler : MonoBehaviour
{
  public List<ParticleSystem> particleSystems;

  public ParticleSystem PlayByName(string name)
  {
    switch (name)
    {
      case "Bomb":
        particleSystems[0].Play();
        return particleSystems[0];
      case "Alien Grenade":
        particleSystems[1].Play();
        return particleSystems[1];
    }
    return null;
  }
}
