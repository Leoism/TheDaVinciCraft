using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using Photon.Pun;
public class Dragable : MonoBehaviour
{   
    public Vector3 screenPoint;
    public Vector3 offset;
    public Vector3 firstMousePos;
    
    void OnMouseDown()
    {
        if (!GameManager.globalManager.currentPlayer.Equals("Human")) return;
        if (EventSystem.current.IsPointerOverGameObject()) return;
        // Only the master client can be the human
        if (GameManager.globalManager.isOnlineMode && !PhotonNetwork.IsMasterClient) return;

        // Only the master client can be the human
        if (GameManager.globalManager.isOnlineMode && !PhotonNetwork.IsMasterClient) return;
        firstMousePos = (Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }
    void OnMouseDrag()
    {
        if (!GameManager.globalManager.currentPlayer.Equals("Human")) return;
        if (EventSystem.current.IsPointerOverGameObject()) return;

        // Only the master client can be the human
        if (GameManager.globalManager.isOnlineMode && !PhotonNetwork.IsMasterClient) return;
        // Vector3 curScreenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0);
        Vector3 currMousePos = (Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 delta = currMousePos - firstMousePos;
        offset = delta;
        Vector3 newPos = gameObject.transform.position + delta;
        newPos.x = Mathf.Clamp(newPos.x, -70f, 16f);
        newPos.y = Mathf.Clamp(newPos.y, -34f, 12f);
        firstMousePos = currMousePos;
        // -70-16 -30-12
        transform.position = new Vector3(newPos.x, newPos.y, 0);
    }

}