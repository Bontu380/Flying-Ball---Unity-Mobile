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

    private float ratio;


    float temp;

    SpriteRenderer sr;


    private void Awake()
    {

        initializeAttributes();
        originalPos = transform.position;
        nextPlacePos = Vector3.zero + new Vector3(camWidth + (backgroundWidth/transform.localScale.x), originalPos.y, 0f);
       
        
        
    }


    void Update()
    {
        
        transform.Translate(-Vector3.right * moveSpeed * Time.deltaTime);

     

        if (transform.position.x <= temp)
        {
            transform.position = nextPlacePos;
        }
          
    }



 
    public void initializeAttributes()
    {

        SpriteRenderer sr = GetComponent<SpriteRenderer>();
        if (sr == null) return;

        backgroundWidth = sr.sprite.bounds.size.x;
        backgroundHeight = sr.sprite.bounds.size.y;

        if (camHeight == 0f && camWidth == 0f)
        {
            camHeight = Camera.main.orthographicSize * 2.0f;
            camWidth = camHeight / Screen.height * Screen.width;
        }

        ratio =  camWidth  / (backgroundWidth - camWidth);

        Debug.Log(ratio);

        temp = -camWidth - -ratio  + ((backgroundWidth - camWidth));
        Debug.Log( temp);

        //  transform.localScale = new Vector3(camWidth / backgroundWidth, camHeight/ backgroundHeight);
    }
}
