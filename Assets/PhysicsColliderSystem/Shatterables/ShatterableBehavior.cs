using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShatterableBehavior : MonoBehaviour
{
  public float minImpactPerct = 15f; // the percentage of the impact speed relative to the objects area
  public float threshhold = 100f; // the max velocity that the object can take
  public int maxShards = 100; // the amount of shattered materials to make
  public GameObject tilePrefab = null;
  private Vector2 spriteDimensions;
  private float minImpactVel = 0f; // the minimum velocity an object must hit at to shatter this object
  private float totalAccum = 0f; // total velocity accumulated from hits 
  // Start is called before the first frame update
  void Start()
  {
    spriteDimensions = GetComponent<SpriteRenderer>().size;
    minImpactVel = (minImpactPerct / 100) * spriteDimensions.x * spriteDimensions.y;
    // may the threshhold is smaller than the calculated minimal impact 
    minImpactVel = Mathf.Min(minImpactVel, threshhold);
    if (spriteDimensions.x < 0.15f)
    {
      Destroy(gameObject);
    }
  }

  void OnCollisionEnter2D(Collision2D collision)
  {
    if (!collision.gameObject.name.Contains("Circle")) return;
    GameObject go = collision.gameObject;
    Rigidbody2D rb = go.GetComponent<Rigidbody2D>();
    float impactVelocity = rb.velocity.magnitude;
    if (impactVelocity < minImpactVel && totalAccum < minImpactVel)
    {
      totalAccum += impactVelocity > minImpactVel * 0.25f ? impactVelocity : 0; // we only want to account large impacts not small negligible ones
      return;
    }
    if (totalAccum > minImpactVel)
    {
      Shatter(Mathf.Min(totalAccum / threshhold, 1));
    }
    else
    {
      Shatter(Mathf.Min(impactVelocity / threshhold, 1));
    }
    Destroy(gameObject);
  }

  void Shatter(float shatterStrength)
  {
    // calculate amount of shards the shatter will make
    int numShards = (int)Mathf.Round(maxShards * shatterStrength);
    // calculate size of each shard to fill the current area
    float shardDimensions = FitSquares(spriteDimensions.x, spriteDimensions.y, numShards);
    int rowAmount = (int)(spriteDimensions.y / shardDimensions);
    int colAmount = (int)(spriteDimensions.x / shardDimensions);
    // the shattered version should start from the top left
    Vector3 startingPos = new Vector3(transform.position.x - spriteDimensions.x / 2, transform.position.y + spriteDimensions.y / 2);
    // adjust because transform.position is centered but we want top left corner
    // to at startingPos
    startingPos += new Vector3(shardDimensions / 2, shardDimensions / 2 * -1, 0);
    for (int i = 0; i < rowAmount; i++)
    {
      for (int j = 0; j < colAmount; j++)
      {
        // Create new shattered game objects
        GameObject newGo = Instantiate(tilePrefab, startingPos + new Vector3(j * shardDimensions, i * shardDimensions * -1, 0), Quaternion.identity);
        newGo.AddComponent<Rigidbody2D>();
        // set the box collider because this behavior is dependent on sprite renderer sizes
        newGo.AddComponent<BoxCollider2D>().size = new Vector2(shardDimensions, shardDimensions);
        SpriteRenderer newSr = newGo.GetComponent<SpriteRenderer>();
        ShatterableBehavior sb = newGo.AddComponent<ShatterableBehavior>();
        // set the shatterable behavior for the new game object
        sb.tilePrefab = tilePrefab;
        sb.threshhold = threshhold;
        sb.maxShards = (int)Mathf.Max(maxShards / 2, 8);
        // set the sprite renderer size
        newSr.size = new Vector2(shardDimensions, shardDimensions);
      }
    }
  }

  /// Calculates the optimal side of squares to be fit into a rectangle
  /// Inputs: x, y: width and height of the available area.
  ///         n: number of squares to fit
  /// Returns: the optimal side of the fitted squares
  /// found from: https://math.stackexchange.com/a/2536926 
  float FitSquares(float x, float y, int n)
  {
    float sx, sy;

    float px = Mathf.Ceil(Mathf.Sqrt(n * x / y));
    if (Mathf.Floor(px * y / x) * px < n)
    {
      sx = y / Mathf.Ceil(px * y / x);
    }
    else
    {
      sx = x / px;
    }

    var py = Mathf.Ceil(Mathf.Sqrt(n * y / x));
    if (Mathf.Floor(py * x / y) * py < n)
    {
      sy = x / Mathf.Ceil(x * py / y);
    }
    else
    {
      sy = y / py;
    }

    return Mathf.Max(sx, sy);
  }
}
