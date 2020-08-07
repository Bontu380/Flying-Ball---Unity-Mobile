using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingBackgroundPlayer : MonoBehaviour
{
    private SpringJoint2D joint;
    private LineRenderer lineRenderer;
    private Rigidbody2D playerRb;
    private Vector3 anchorPoint;

    public bool isRotated = false;
    public float fixedVelocity = 5f;
    public float smoothFactor = 6f;
    void Awake()
    {
        joint = GetComponent<SpringJoint2D>();
        joint.autoConfigureConnectedAnchor = false;
        joint.frequency = 0;


        lineRenderer = GetComponent<LineRenderer>();
        playerRb = GetComponent<Rigidbody2D>();

        anchorPoint = joint.connectedAnchor;
    }



    void Update()
    {

        faceVelocityDirection();

        if(transform.position.y >= Camera.main.orthographicSize)
        {
            if (!isRotated)
            {
                transform.rotation = Quaternion.Euler(0f, 180f, 0f);
                isRotated = true;
            }
            else
            {
                transform.rotation = Quaternion.Euler(0f, 0f, 0f);
                isRotated = false;
            }
        }
       
     
    }

    private void FixedUpdate()
    {
      
        playerRb.velocity = transform.right *  fixedVelocity;

        
    }

    private void LateUpdate()
    {
        updateLineStartPos();
    }

    public void updateLineStartPos()
    {

        lineRenderer.SetPosition(0, transform.position);
      
    }

    public void faceVelocityDirection()
    {
        Vector2 velocity = playerRb.velocity;
        float angleToVelocityDirection = Mathf.Atan2(velocity.y, velocity.x) * Mathf.Rad2Deg;
        Quaternion newRotation = Quaternion.AngleAxis(angleToVelocityDirection, Vector3.forward);
        transform.rotation = Quaternion.Lerp(transform.rotation, newRotation, smoothFactor * Time.deltaTime);
    }
}
