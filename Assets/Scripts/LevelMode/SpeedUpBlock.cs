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

    // Update is called once per frame
    void Update()
    {
        if (speedingUpPlayer)
        {
            return;
        }

        if(script.beingGrappled == true)
        {
            StartCoroutine(speedUpPlayer());
        }
        else
        {
            PlayerController.instance.velocityMultiplier = playerOriginalVelocity;
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
        speedingUpPlayer = false;
    }

}
