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
    private float velocityMultiplier = 3f;

    [SerializeField] private Vector2 currentSpeed;

    void Start()
    {
        playerRb = GetComponent<Rigidbody2D>();
        lineRenderer = GetComponent<LineRenderer>();
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetMouseButtonDown(0))
        {
            grapple();
        }


        else if (Input.GetMouseButtonUp(0))
        {
            releaseGrapple();
        }

        currentSpeed = playerRb.velocity;
    }


    void LateUpdate()
    {
        drawRope();     
    }

    public void grapple()
    {

        Vector2 touchPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 playerPosition = transform.position;
        lineRenderer.positionCount = 2;

        RaycastHit2D hit;
        hit = Physics2D.Raycast(transform.position, touchPosition - playerPosition, Mathf.Infinity, LayerMask.GetMask("Obstacle"));
      
       
        if (hit.collider != null)
        {
            grapplePoint = hit.point;
            float distance = Vector3.Distance(grapplePoint,transform.position);
            createJoint(distance);
            setUpPhysicsForGrapple();
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
        playerRb.velocity *= velocityMultiplier;
    }

    public void setUpPhysicsForRelease()
    {
        playerRb.gravityScale = 1f;
        playerRb.angularDrag = 0.5f;
        playerRb.velocity /= velocityMultiplier;
    }


}
