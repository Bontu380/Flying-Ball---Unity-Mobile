using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedUpBlock : MonoBehaviour
{
    private GrappableObject script;
    public bool speedingUpPlayer = false;
    public float maxSpeedLimit = 35f;
    public float increaseRate = 3f;
    public float increaseTimeRate = 0.3f;
    private float playerOriginalVelocity;
    void Start()

    {
        script = GetComponent<GrappableObject>();
        playerOriginalVelocity = PlayerController.instance.velocityMultiplier;
    }

    void Update()
    {
       
        
        if (script.beingGrappled && !speedingUpPlayer)
        {
            StartCoroutine(speedUpPlayer());
        }
        else if(!script.beingGrappled && speedingUpPlayer)
        { 
            PlayerController.instance.velocityMultiplier = playerOriginalVelocity;
            speedingUpPlayer = false;
        }




     }

    public IEnumerator speedUpPlayer()
    {
       
        speedingUpPlayer = true;
        while(PlayerController.instance.velocityMultiplier < maxSpeedLimit)
        {
            PlayerController.instance.velocityMultiplier += increaseRate;
            yield return new WaitForSeconds(increaseTimeRate);
           
        }
       
    }

}
