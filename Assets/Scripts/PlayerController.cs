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
            Debug.DrawRay(transform.position, touchPosition-playerPosition, Color.green);
            Debug.Log(hit.collider.name);
            isGrappling = true;
        }
        
 
    }

    public void releaseGrapple()
    {
        lineRenderer.positionCount = 0;
        isGrappling = false;
        
    }

    public void drawRope()
    {
        if (!isGrappling) return;

        lineRenderer.SetPosition(0, transform.position);
        lineRenderer.SetPosition(1, grapplePoint);
    
    }


}
