using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Realtime;

public class PlayerListing : MonoBehaviour
{
  [SerializeField]
  public Text _text;
  public Photon.Realtime.Player Player { get; private set; }

  public void SetPlayerInfo(Photon.Realtime.Player player)
  {
    Player = player;
    _text.text = player.NickName;
  }
}
