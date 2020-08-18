using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingLevelBackground : MonoBehaviour
{

    public float moveSpeed = 5f;

  

    private Vector3 nextPlacePos;
    private Vector3 originalPos;
    private static float camWidth;
    private static float backgroundWidth;
    private static float ratio;



    private void Awake()
    { 
        initializeAttributes();
    }


    void Update()
    {

        transform.Translate(-Vector3.right * moveSpeed * Time.deltaTime);

        if (transform.position.x <= 2f * ratio  * ( -camWidth-backgroundWidth))
        {
            transform.position = nextPlacePos;
        }

    }

    public void initializeAttributes()
    {
      
        originalPos = transform.position;

        SpriteRenderer sr = GetComponent<SpriteRenderer>();
        if (sr == null) return;

        if (backgroundWidth == 0 && ratio == 0 && camWidth == 0) //static variables not initialized
        {
            camWidth = (GameController.instance.originalCamSize * 2f) / Screen.height * Screen.width;
            backgroundWidth = sr.sprite.bounds.size.x;
            ratio = (camWidth + transform.localScale.x) / backgroundWidth;
        }
        nextPlacePos = Vector3.zero + new Vector3(2f * ratio * (camWidth+backgroundWidth - transform.localScale.x), originalPos.y, 0f);



        
        Debug.Log((camWidth + transform.localScale.x) / backgroundWidth);

        Debug.Log(nextPlacePos);
    }



    

}
