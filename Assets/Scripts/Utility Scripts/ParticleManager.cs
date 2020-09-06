using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ParticleManager : MonoBehaviour
{
    public static ParticleManager instance;
    ParticleSystem ps;
    Rigidbody2D playerRb;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
 

    }
    void Start()
    {
        ps = GetComponent<ParticleSystem>();
        playerRb = GetComponent<Rigidbody2D>();
   
    }
    
    void Update()
    {
        Vector2 playerVelocity = playerRb.velocity;
        updateParticles();
    }

    public void updateParticles()
    {
        var emission = ps.emission;
        emission.rateOverTime = playerRb.velocity.magnitude + 1f;
    }

}



