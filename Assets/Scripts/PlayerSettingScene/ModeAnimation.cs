using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModeAnimation : MonoBehaviour
{
    private Vector3 initPosition;
    [SerializeField] private RectTransform finalRect;
    void Awake()
    {
        initPosition = GetComponent<RectTransform>().anchoredPosition3D;
    }

    // Update is called once per frame
    void Update()
    {
        GetComponent<RectTransform>().anchoredPosition3D = Vector3.Lerp(GetComponent<RectTransform>().anchoredPosition3D, finalRect.anchoredPosition3D, 5f * Time.smoothDeltaTime);
    }

    private void OnDisable() 
    {
        GetComponent<RectTransform>().anchoredPosition3D = initPosition;
    }
}
