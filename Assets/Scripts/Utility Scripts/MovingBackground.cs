using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingBackground : MonoBehaviour
{

    public float moveSpeed = 5f;

    private Vector3 nextPlacePos;

    private Vector3 originalPos;

    private float camWidth;
    private float camHeight;

    private float backGroundWidth;
    private float backGroundHeight;

    SpriteRenderer sr;


    private void Awake()
    {
        initializeAttributes();
        setBackgroundSize();
        nextPlacePos = Vector3.zero + new Vector3(camWidth-transform.localScale.x ,0f,0f);
        originalPos = transform.position;
    }



    void Update()
    {
        
        transform.Translate(-Vector3.right * moveSpeed * Time.deltaTime);

        if(transform.position.x <= -camWidth)
        {
            transform.position = nextPlacePos;
        }
        
        
        
    }

    public void setBackgroundSize()
    {
        transform.localScale = new Vector3(camWidth / backGroundWidth, camHeight / backGroundHeight, 0f);
    }

    public void initializeAttributes()
    {
        sr = GetComponent<SpriteRenderer>();

        camHeight = 2f * Camera.main.orthographicSize;
        camWidth = camHeight * Camera.main.aspect;

        backGroundWidth = sr.sprite.bounds.size.x;
        backGroundHeight = sr.sprite.bounds.size.y;

    }
}
