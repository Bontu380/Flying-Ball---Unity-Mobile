using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    private Rigidbody2D playerRb;
    private SpringJoint2D joint;
    private LineRenderer lineRenderer;
    public LayerMask grappable;
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
        /*if(Input.touchCount == 1)
         {
             if(Input.GetTouch(0).phase == TouchPhase.Began)
             {

                 grapple();

             }
         } 

        else if(Input.touchCount == 0)
        {
             releaseGrapple();

         }*/


        if (Input.GetMouseButtonDown(0))
        {

           // move();
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
        //Vector3 touchPosition = Input.GetTouch(0).position;

        Vector2 touchPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 playerPosition = transform.position;

        Vector2 raycastStartPos = touchPosition - playerPosition;


        RaycastHit2D hit;
        hit = Physics2D.Raycast(raycastStartPos, touchPosition - playerPosition, grappable);
      
       
        if (hit.collider != null)
        {
            grapplePoint = hit.point;
            Debug.DrawRay(transform.position, touchPosition-playerPosition, Color.green);
            Debug.Log(hit.transform.tag);
            isGrappling = true;
        }
        
 
    }

    public void releaseGrapple()
    {
        isGrappling = false;
    }

    public void drawRope()
    {
        if (!isGrappling) return;


        lineRenderer.SetPosition(0, transform.position);
        lineRenderer.SetPosition(1, grapplePoint);
    }


}
