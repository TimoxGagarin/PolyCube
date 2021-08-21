using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatforms : MonoBehaviour
{
    public float Speed = 2F;

    public float h = 0;
    public float v = 0;

    private float i;
    private float j;

    public float TargetY;
    public float TargetX;

    private float posY;
    private float posX;
    private void Start()
    {
        posY = transform.position.y - h;
        posX = transform.position.x - v;
        i = Math.Abs(TargetY);
        j = Math.Abs(TargetX);
    }
    void FixedUpdate()
    {
        GetComponent<Rigidbody2D>().velocity = new Vector3(TargetX * Time.deltaTime * Speed , TargetY * Time.deltaTime * Speed, 0);
        if (transform.position.y >= TargetY + posY)
            TargetY = -i;
        if (transform.position.y <= posY)
            TargetY = i;
        if (transform.position.x >= TargetX + posX)
            TargetX = -j;
        if (transform.position.x <= posX)
            TargetX = j;
    }
}
