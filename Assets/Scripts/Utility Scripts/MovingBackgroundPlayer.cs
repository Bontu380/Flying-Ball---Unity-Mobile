using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingBackgroundPlayer : MonoBehaviour
{
    private SpringJoint2D joint;
    private LineRenderer lineRenderer;
    private Rigidbody2D playerRb;
    private ParticleSystem ps;
    private Vector3 anchorPoint;
    private float tempTime = 0f;

    public bool goingRight = true;
    public float fixedVelocity = 5f;
    public float smoothFactor = 6f;
    void Awake()
    {
        joint = GetComponent<SpringJoint2D>();
        joint.autoConfigureConnectedAnchor = false;
        joint.frequency = 0;

        ps = GetComponent<ParticleSystem>();

        lineRenderer = GetComponent<LineRenderer>();
        playerRb = GetComponent<Rigidbody2D>();

        anchorPoint = joint.connectedAnchor;
    }

    private void Start()
    {

        playerRb.velocity = transform.right * fixedVelocity;

    }

    void Update()
    {
        tempTime += Time.deltaTime;

        if (tempTime >= 2.5f)
        {
           
            if (transform.position.y >= Camera.main.orthographicSize + 6f)
            {
                Debug.Log("Outer if");
                ps.Stop();
                if (goingRight && transform.position.x > anchorPoint.x)
                {
                    Debug.Log("If");

                    transform.rotation = Quaternion.AngleAxis(180f, transform.up) * transform.rotation;
                    goingRight = false;
                    playerRb.velocity = Vector2.zero;
                }
                else if (!goingRight && transform.position.x <= anchorPoint.x)
                {
                    Debug.Log("Else");

                    transform.rotation = Quaternion.AngleAxis(-180f, transform.up) * transform.rotation;
                    goingRight = true;

                }
                randomizeJointPointsAndDistance();
                playerRb.velocity = transform.right * fixedVelocity;

                tempTime = 0f;

                ps.Play();
                return;
            }
        }
        faceVelocityDirection();

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

    
    public void randomizeJointPointsAndDistance()
    { 
        joint.connectedAnchor = new Vector2(Random.Range(-6, 6f), anchorPoint.y);
        lineRenderer.SetPosition(1, joint.connectedAnchor); 
    }
}
