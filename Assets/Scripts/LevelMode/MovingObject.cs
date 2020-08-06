using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingObject : MonoBehaviour
{

    public float moveSpeed = 6f;

    public bool movingToPos1 = true;

    [SerializeField] private Vector3 pos1;
    [SerializeField] private Vector3 pos2;
  
    [SerializeField] private float distanceToPos;

    public float xOffset = 3.5f;
    public float yOffset = 0f;

    private void Start()
    {
        pos1 = transform.position + new Vector3(-xOffset,-yOffset,0f);
        pos2 = transform.position + new Vector3(xOffset, yOffset, 0f);

        Debug.DrawRay(transform.position, pos1, Color.green);
        Debug.DrawRay(transform.position, pos2, Color.green);
    }
    // Update is called once per frame
    void Update()
    {
        if (movingToPos1)
        {
            //transform.position = Vector3.Lerp(transform.position, pos1,moveSpeed * Time.deltaTime);
            transform.position = Vector3.MoveTowards(transform.position, pos1, moveSpeed * Time.deltaTime);

           distanceToPos = Vector3.Distance(pos1, transform.position);

            if (distanceToPos <= 0.2f)
            {
                
                movingToPos1 = false;
            }
        }
        else if(!movingToPos1) //O zaman pos2 ye gidiyordur.
        {
           // transform.position = Vector3.Lerp(transform.position, pos2, moveSpeed * Time.deltaTime);

            transform.position = Vector3.MoveTowards(transform.position, pos2, moveSpeed * Time.deltaTime);

            distanceToPos = Vector3.Distance(pos2,transform.position);
            if (distanceToPos <= 0.2f)
            {
            
                movingToPos1 = true;
            }

        }

    }
}
