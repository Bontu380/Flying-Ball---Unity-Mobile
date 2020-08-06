using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ParticleManager : MonoBehaviour
{
    ParticleSystem ps;
    Rigidbody2D playerRb;
    // Start is called before the first frame update
    void Start()
    {
        ps = GetComponent<ParticleSystem>();
        playerRb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 playerVelocity = playerRb.velocity;

    }
}
