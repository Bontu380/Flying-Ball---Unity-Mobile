using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRotations : MonoBehaviour
{
    private Rigidbody2D playerRb;
    private SpringJoint2D joint;
    private float angle;
    private float smoothFactor = 3f;
    void Start()
    {
        playerRb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        /*
        Vector2 velocity = playerRb.velocity;
        angle = Mathf.Atan2(velocity.y,velocity.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        */

        Vector2 velocity = playerRb.velocity;
        angle = Mathf.Atan2(velocity.y, velocity.x) * Mathf.Rad2Deg;
        
        Quaternion rotated = Quaternion.AngleAxis(angle, Vector3.forward);
        transform.rotation = Quaternion.Lerp(transform.rotation, rotated,smoothFactor );

    }
}
