using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Background : MonoBehaviour
{
    private Rigidbody2D rb;
    public float moveSpeed = 5f;
    private BoxCollider2D limitCollider;


    private float groundHorizontalLength;


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    
        limitCollider = GameObject.FindGameObjectWithTag("Ground").GetComponent<BoxCollider2D>();
        groundHorizontalLength = limitCollider.size.x;

        rb.velocity = new Vector2(-moveSpeed, 0f);
             
    }

 
    void Update()
    {
        if(transform.position.x < -groundHorizontalLength)
        {
            repositionBackground();
        }
    }

    public void repositionBackground()
    {
        transform.position = transform.position + new Vector3(2f * groundHorizontalLength, 0f,0f);
    }
}
