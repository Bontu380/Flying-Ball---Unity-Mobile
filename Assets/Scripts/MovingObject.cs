using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingObject : MonoBehaviour
{

    public float moveSpeed = 0.8f;

    public bool movingToPos1 = true;

    private Vector3 pos1;
    private Vector3 pos2;

    public float xOffset = 3.5f;
    public float yOffset = 0f;

    private void Start()
    {
        pos1 = transform.position + new Vector3(xOffset,yOffset,0f);
        pos2 = transform.position + new Vector3(-xOffset, -yOffset, 0f);
    }
    // Update is called once per frame
    void Update()
    {
        if (movingToPos1)
        {
            transform.Translate(pos1 * Time.deltaTime * moveSpeed);

            float distanceToPos = Vector3.Distance(pos1, transform.position);

            if (distanceToPos <= 0.2f)
            {
                movingToPos1 = false;
            }
        }
        else if(!movingToPos1) //O zaman pos2 ye gidiyordur.
        {
            transform.Translate(pos2 * Time.deltaTime * moveSpeed);

            float distanceToPos = Vector3.Distance(transform.position,pos2);
            if (distanceToPos <= 0.2f)
            {
                movingToPos1 = true;
            }

        }

    }
}
