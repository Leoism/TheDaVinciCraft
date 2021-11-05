using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TrajectoryButton : MonoBehaviour
{
  public ShootingBehavior cat = null;
  public BoomerangShooter boom = null;
  public RayShooter ray = null;
  public GameObject aim = null;
  public Text displayText = null;
  // Start is called before the first frame update
  void Start()
  {
    Debug.Assert(boom != null);
    Debug.Assert(cat != null);
    Debug.Assert(aim != null);
    Debug.Assert(ray != null);
  }

  public void ActivateBehavior(int idx)
  {
    if (idx == 0)
    {
      cat.enabled = true;
      boom.enabled = false;
      ray.enabled = false;
      displayText.text = "Trajectory For: Arrow, Bomb, Alien Grenade, Bowling Ball, Deforester, Magnet";
      aim.SetActive(false);
    }
    else if (idx == 1)
    {
      boom.enabled = true;
      cat.enabled = false;
      ray.enabled = false;
      displayText.text = "Trajectory For: Boomerang";
      aim.SetActive(false);
    }
    else if (idx == 2)
    {
      ray.enabled = true;
      cat.enabled = false;
      boom.enabled = false;
      displayText.text = "Trajectory For: Ray";
      aim.SetActive(true);
    }
  }
}
