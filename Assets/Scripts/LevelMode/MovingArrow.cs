using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingArrow : MonoBehaviour
{
    private Vector3 originalPos;
    private Vector3 limitPos;
    public float smoothFactor = 2f;
    void Start()
    {
        originalPos = transform.position;
        limitPos = transform.parent.position;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.Lerp(originalPos, limitPos, smoothFactor);
        float distanceToLimit = Vector3.Distance(transform.position, limitPos);
        if(distanceToLimit <= 0.2f)
        {
            transform.position = originalPos;
        }
    }
}
