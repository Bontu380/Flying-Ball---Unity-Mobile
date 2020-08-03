using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    private Rigidbody2D playerRb;
    private SpringJoint2D joint;
    private LineRenderer lineRenderer;
    private Vector2 grapplePoint;
    public bool isGrappling = false;
    private SpringJoint2D jointNode;
    private float velocityMultiplier = 10f;
    private float forceMultiplier;
    private float smoothFactor = 6f;
    Touch touch;
    [SerializeField] private Vector2 currentSpeed;
    [SerializeField] private float currentSpeedMagnitude;
    [SerializeField] private float angle;

    void Start()
    {
        playerRb = GetComponent<Rigidbody2D>();
        lineRenderer = GetComponent<LineRenderer>();

    }


    void Update()
    {

        if (GameController.instance.pause) return;

       /* if (isGrappling)
        {
            playerRb.velocity = playerRb.velocity.normalized * velocityMultiplier;

        }
        */

        Vector2 velocity = playerRb.velocity;
        angle = Mathf.Atan2(velocity.y, velocity.x) * Mathf.Rad2Deg;
        Quaternion newRotation = Quaternion.AngleAxis(angle, Vector3.forward);
        transform.rotation = Quaternion.Lerp(transform.rotation, newRotation, smoothFactor * Time.deltaTime);


        /*
        if (Input.GetMouseButtonDown(0))
        {
            grapple();
        }
        

        else if (Input.GetMouseButtonUp(0))
        {
            releaseGrapple();
        }
        */

      

        if (Input.touchCount > 0)
        {
            touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Began)
            {
                grapple();
            }
        }


        else if (Input.touchCount == 0)
        {
            if (touch.phase == TouchPhase.Ended)
            {
                releaseGrapple();
            }
        }

       /* float playerDistanceToOrigin = Vector3.Distance(transform.position, Vector3.zero);
        if (playerDistanceToOrigin > 100f)
        {
            GameController.instance.die(); 
        }*/



        currentSpeed = playerRb.velocity;
        currentSpeedMagnitude = playerRb.velocity.magnitude;
        
    }

    private void FixedUpdate()
    {
        if (isGrappling)
        {
            playerRb.velocity = playerRb.velocity.normalized * velocityMultiplier;

        }
    }


    void LateUpdate()
    {
        drawRope();     
    }

    public void grapple()
    {

        // Vector2 touchPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

       
        Vector2 touchPosition = Camera.main.ScreenToWorldPoint(touch.position);


        Vector2 playerPosition = transform.position;
        lineRenderer.positionCount = 2;

        RaycastHit2D hit;
        hit = Physics2D.Raycast(transform.position, touchPosition - playerPosition, 30f, LayerMask.GetMask("Obstacle"));

        playerRb.gravityScale = 1;
       
        if (hit.collider != null)
        {
            grapplePoint = hit.point;
            float distance = Vector3.Distance(grapplePoint,transform.position);
            createJoint(distance);
            playerRb.gravityScale = 1;
           // setUpPhysicsForGrapple();
           // playerRb.velocity = playerRb.velocity.normalized * velocityMultiplier;
            isGrappling = true;
        }
        
 
    }

    public void releaseGrapple()
    {
        lineRenderer.positionCount = 0;
        Destroy(jointNode);
        setUpPhysicsForRelease();
        isGrappling = false;
        
    }

    public void drawRope()
    {
        if (!isGrappling) return;

        lineRenderer.SetPosition(0, transform.position);
        lineRenderer.SetPosition(1, grapplePoint);
    
    }

    public void createJoint(float distance)
    {
       jointNode = gameObject.AddComponent<SpringJoint2D>();
       jointNode.autoConfigureConnectedAnchor = false;
       jointNode.connectedAnchor = grapplePoint;
       jointNode.distance = distance;
       jointNode.frequency = 0f;
         
    }

    public void setUpPhysicsForGrapple()
    {
        playerRb.angularDrag = 0f;
        playerRb.gravityScale = 0f;
    
    }

    public void setUpPhysicsForRelease()
    {
        playerRb.gravityScale = 1f;
        playerRb.angularDrag = 0.5f;

    }



    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("Obstacle") || collision.transform.CompareTag("Wall"))
        {
            GameController.instance.die();
        }
    }



}
