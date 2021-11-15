using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModeAnimation : MonoBehaviour
{
    private Vector3 initPosition;
    [SerializeField] private Vector3 finalPosition;
    void Awake()
    {
        initPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.Lerp(transform.position, finalPosition, 0.05f);
    }

    private void OnDisable() 
    {
        transform.position = initPosition;
    }
}
