using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Finish : MonoBehaviour
{
  private   BoxCollider2D finishCollider;
    private void Start()
    {
        finishCollider = GetComponent<BoxCollider2D>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.CompareTag("Player"))
        {
            PlayerController.instance.releaseGrapple();
            GameController.instance.levelPassed();
            StartCoroutine(lockPlayerInside());
        }
    }

    public IEnumerator lockPlayerInside()
    {
        yield return new WaitForSeconds(0.3f);
        finishCollider.isTrigger = false;
    }
}
