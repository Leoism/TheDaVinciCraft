using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
public class shatterDestroy : MonoBehaviour
{
    // Start is called before the first frame update
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Weapon"))
        {
            if (GameManager.globalManager.isOnlineMode)
            {
                PhotonNetwork.Destroy(GetComponent<PhotonView>());
            }
            else
            {
                Destroy(gameObject);
            }
        }
    }
}
