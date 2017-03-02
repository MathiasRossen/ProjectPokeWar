using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{

    [HideInInspector]
    public string ownerName;
    public float velocity;

    Rigidbody2D rigidBody;

    // Use this for initialization
    void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
    }
    
    void FixedUpdate()
    {
        if (!IsGrounded())
        {
            rigidBody.AddForce(new Vector2((velocity / 100), 0), ForceMode2D.Impulse);
        }
    }

    bool IsGrounded()
    {
        return true;
    }
}
