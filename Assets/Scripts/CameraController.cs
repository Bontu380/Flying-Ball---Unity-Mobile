using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Camera cam;
    private float originalSize;
    private float zoomOutSize;
    private float zoomSmoothTime = 3.5f;
    public float countDownTime = 3f;
    

    void Start()
    {
        originalSize = cam.orthographicSize;
        zoomOutSize = originalSize + 6f;

        Coroutine waitForZoomOut = StartCoroutine(zoomOut());
        Coroutine waitForZoomIn = StartCoroutine(zoomIn(waitForZoomOut));
        //StartCoroutine(countdownToStart());
        
    }

/*
    private void Update()
    {
        
    }

*/

    public IEnumerator zoomOut()
    {
        while(cam.orthographicSize <= zoomOutSize - 0.001f)
        {
            cam.orthographicSize = Mathf.Lerp(cam.orthographicSize, zoomOutSize, zoomSmoothTime * Time.deltaTime);
            yield return null;
        }
        yield return new WaitForSeconds(1.5f);
    }

    public IEnumerator zoomIn(Coroutine waitForZoomOut)
    {

        yield return waitForZoomOut;

        while (cam.orthographicSize >= originalSize - 0.001f)
        {
            cam.orthographicSize = Mathf.Lerp(cam.orthographicSize, originalSize, zoomSmoothTime * Time.deltaTime);
            yield return null;
        }
        yield return new WaitForSeconds(1.5f);
    }

    /*public IEnumerator countdownToStart()
    {
        float localTime = 3f;

        while ()
        {
            yield return null;
        }

        GameController.instance.start = true;
        yield return null;
    }*/

}
