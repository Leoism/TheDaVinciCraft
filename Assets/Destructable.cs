using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Destructable : MonoBehaviour
{
    /* public Tilemap destructableTilemap;
     // Start is called before the first frame update
     void Start()
     {
         destructableTilemap = GetComponent<Tilemap>();
     }
 */
/*
    private void OnCollisionEnter2D(Collision2D collision)
    {
     *//*   if (collision.gameObject.CompareTag("tm"))
        {
            //Debug.Log("hit");
            Vector3 hitPosition = Vector3.zero;
            foreach (ContactPoint2D hit in collision.contacts)
            {
                hitPosition.x = hit.point.x - collision.gameObject.GetComponent<Tilemap>().cellSize.x * hit.normal.x;
                hitPosition.y = hit.point.y - collision.gameObject.GetComponent<Tilemap>().cellSize.y * hit.normal.y;
                collision.gameObject.GetComponent<Tilemap>().SetTile
                (collision.gameObject.GetComponent<Tilemap>().WorldToCell(hitPosition), null);
*//*                Tilemap tl = collision.gameObject.GetComponent<Tilemap>();
                Destroy(tl.GetTile(tl.WorldToCell(hitPosition)));*//*
            }
        }*//*
    }*/
}
