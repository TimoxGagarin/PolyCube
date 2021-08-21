using System;
using System.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Levitation : MonoBehaviour
{
    public float Speed = 2F;

    public float deltaEarthY = 0;
    public float deltaEarthX = 0;

    private float deltaY;
    private float deltaX;

    public float TargetY;
    public float TargetX;

    private float posY;
    private float posX;
    private void Start()
    {
        posY = transform.position.y - deltaEarthY;
        posX = transform.position.x - deltaEarthX;
        deltaY = Math.Abs(TargetY);
        deltaX = Math.Abs(TargetX);
    }
    void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, new Vector3(transform.position.x + TargetX, transform.position.y + TargetY, transform.position.z), Time.deltaTime * Speed);
        if (transform.position.y >= TargetY + posY)
            TargetY = -deltaY;
        if (transform.position.y <= posY)
            TargetY = deltaY;
        if (transform.position.x >= TargetX + posX)
            TargetX = -deltaX;
        if (transform.position.x <= posX)
            TargetX = deltaX;
    }
}
