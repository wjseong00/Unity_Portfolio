﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCam : MonoBehaviour
{
    public Transform target;
    public float moveDamping=15f;
    public float rotateDamping=10.0f;
    public float distance =5.0f;
    public float height = 4.0f;
    public float targetOffset=2.0f;

    private Transform tr;
    // Start is called before the first frame update
    void Start()
    {
        tr = GetComponent<Transform>();
    }
    private void LateUpdate()
    {
        var camPos = target.position - (target.forward * distance) + (target.up*height);

        tr.position = Vector3.Slerp(tr.position, camPos, Time.deltaTime * rotateDamping);
        tr.rotation = Quaternion.Slerp(tr.rotation, target.rotation, Time.deltaTime * rotateDamping);
        tr.LookAt(target.position + (target.up * targetOffset));
    }
    
}
