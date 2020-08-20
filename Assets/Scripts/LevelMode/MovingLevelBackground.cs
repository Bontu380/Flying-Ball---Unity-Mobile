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
    private float ratio2;
    private float temp;

    

    private void Awake()
    { 
        initializeAttributes();
    }


    void Update()
    {
     
            transform.Translate(-Vector3.right * moveSpeed * Time.deltaTime);
        

        //if (transform.position.x <= 2f * ratio  * ( -camWidth-backgroundWidth))
        if (transform.position.x <= -nextPlacePos.x)
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

        ratio2 = camWidth / (backgroundWidth - camWidth);
        temp = -camWidth - -ratio + ((backgroundWidth - camWidth));

        //nextPlacePos = Vector3.zero + new Vector3((camWidth+backgroundWidth - transform.localScale.x), originalPos.y, 0f);
        nextPlacePos =  new Vector3(-ratio2 * (backgroundWidth +   transform.localScale.x * ratio + camWidth/-ratio2), originalPos.y, 0f);

        //Debug.Log(backgroundWidth);
        Debug.Log(nextPlacePos);
        //Debug.Log(ratio2);
        //Debug.Log(temp);
    }



    

}
