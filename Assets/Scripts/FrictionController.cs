using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrictionController : MonoBehaviour
{
    public PhysicsMaterial2D[] PhysicsArray;
    public GameObject GameField;
    private PhysicsMaterial2D rbMaterial;
    private void Start()
    {
        GetComponent<Rigidbody2D>().sharedMaterial = PhysicsArray[0];
    }

    private void Update()
    {
        if (!GetComponent<HeroController>().OnGround)
        {
            GetComponent<Rigidbody2D>().sharedMaterial = PhysicsArray[1];
        }
        else if(GetComponent<HeroController>().OnGround)
        {
            GetComponent<Rigidbody2D>().sharedMaterial = PhysicsArray[0];
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Platform" && collision.gameObject.GetComponent<Levitation>())
        {
            transform.SetParent(collision.gameObject.transform);
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Platform" && collision.gameObject.GetComponent<Levitation>())
        {
            transform.SetParent(GameField.transform);
        }
    }
}
