using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform objectToFollow;
    [SerializeField] private Vector3 offset;

    public void Init(Transform follow)
    {
        objectToFollow = follow;
    }
    
    private void Update()
    {
        if(objectToFollow == null) return;

        transform.position = objectToFollow.position + offset;
    }
}
