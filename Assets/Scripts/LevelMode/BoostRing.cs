using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoostRing : MonoBehaviour
{

    public float boostForce = 40f;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.CompareTag("Player"))
        {
            Rigidbody2D playerRb = collision.GetComponent<Rigidbody2D>();

            Vector3 boostVector = playerRb.velocity.normalized * boostForce;

            playerRb.AddForce(boostVector,ForceMode2D.Impulse);

           // PlayerController.instance.startZoom("out");
        }
    }

}
