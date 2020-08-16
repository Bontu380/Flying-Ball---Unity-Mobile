using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingBackground : MonoBehaviour
{

    public float moveSpeed = 5f;

    private Vector3 nextPlacePos;

    private Vector3 originalPos;

    private static float camWidth;
    private static float camHeight;

    private float backgroundWidth;
    private float backgroundHeight;

    public Camera cam;

    SpriteRenderer sr;


    private void Awake()
    {



        // initializeAttributes();
        //setBackgroundSize();
        resizeToFitScreen();
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
        transform.localScale = new Vector3(camWidth / backgroundWidth, camHeight / backgroundHeight, 0f);
    }

    public void initializeAttributes()
    {

        Debug.Log(cam.orthographicSize);

        //sr = GetComponent<SpriteRenderer>();

        camHeight = 2f * cam.orthographicSize;
        Debug.Log(camHeight);

        camWidth = camHeight * Camera.main.aspect;
        Debug.Log(camWidth);


        //backGroundWidth = sr.sprite.bounds.size.x;
        //backGroundHeight = sr.sprite.bounds.size.y;

    }

    public void resizeToFitScreen()
    {

        SpriteRenderer sr = GetComponent<SpriteRenderer>();
        if (sr == null) return;

        transform.localScale = new Vector3(1, 1, 1);

        backgroundWidth = sr.sprite.bounds.size.x;
        backgroundHeight = sr.sprite.bounds.size.y;

        if (camHeight == 0f && camWidth == 0f)
        {
            camHeight = Camera.main.orthographicSize * 2.0f;
            camWidth = camHeight / Screen.height * Screen.width;
        }

        transform.localScale = new Vector3(camWidth / backgroundWidth, camHeight/ backgroundHeight);
    }
}
