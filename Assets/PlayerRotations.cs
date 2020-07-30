using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRotations : MonoBehaviour
{
    Rigidbody2D playerRb;
    void Start()
    {
        playerRb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
       // Vector2 sipid = new Vector2(playerRb.velocity.x, 0f);

        Vector2 speed = playerRb.velocity;
        float angle = Vector2.Angle(speed, transform.position);


        //transform.eulerAngles= new Vector3(0f,0f,angle);

        transform.Rotate(Vector3.forward,angle * Time.deltaTime);
       // transform.Rotate(Vector3.forward,angle);
       
    }
}
