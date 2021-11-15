using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// keeps a UI object relative to a camera position while preserving scale
public class RelativeToCamera : MonoBehaviour
{
  public Vector3 percentageOfCamera;
  private float initCamSize = 0f;
  private RectTransform rt = null;
  private Vector3 initScale;
  void Start()
  {
    rt = GetComponent<RectTransform>();
    initCamSize = Camera.main.orthographicSize;
    initScale = rt.localScale;
  }
  // Update is called once per frame
  void Update()
  {
    Vector3 windowCorner = Camera.main.ViewportToWorldPoint(new Vector3(percentageOfCamera.x, percentageOfCamera.y, 0));
    transform.position = new Vector3(windowCorner.x, windowCorner.y, 0);
    rt.anchoredPosition3D = new Vector3(rt.anchoredPosition.x, rt.anchoredPosition.y, 1);
    rt.localScale = initScale * (Camera.main.orthographicSize / initCamSize);
  }
}
