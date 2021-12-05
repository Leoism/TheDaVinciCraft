using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dragable : MonoBehaviour
{   
    private Vector3 screenPoint;
    private Vector3 offset;
    void OnMouseDown()
    {
        if (!GameManager.globalManager.currentPlayer.Equals("human")) return;
        screenPoint = Camera.main.WorldToScreenPoint(gameObject.transform.position); 
        offset = gameObject.transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z));
        // offset = gameObject.transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0));
    }
    void OnMouseDrag()
    {
        if (!GameManager.globalManager.currentPlayer.Equals("Human")) return;
        // Vector3 curScreenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0);
        Vector3 curScreenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z);
        Vector3 curPosition = Camera.main.ScreenToWorldPoint(curScreenPoint) + offset;
        curPosition.x = Mathf.Clamp(curPosition.x, -70f, 16f);
        curPosition.y = Mathf.Clamp(curPosition.y, -34f, 12f);
        // -70-16 -30-12
        transform.position = new Vector3(curPosition.x, curPosition.y, 0);
    }

}