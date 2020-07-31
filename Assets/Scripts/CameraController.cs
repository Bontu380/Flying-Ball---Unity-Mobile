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
        StartCoroutine(countdownToStart(waitForZoomIn,countDownTime));
        //Debug.Log("SA");
        
    }

/*
    private void Update()
    {
        
    }

*/

    public IEnumerator zoomOut()
    {
        while(cam.orthographicSize <= zoomOutSize - 0.05f)
        {
            cam.orthographicSize = Mathf.Lerp(cam.orthographicSize, zoomOutSize, zoomSmoothTime * Time.deltaTime);
            yield return null;
        }
        yield return new WaitForSeconds(1.5f);
    }

    public IEnumerator zoomIn(Coroutine waitForZoomOut)
    {

        yield return waitForZoomOut;

        while (cam.orthographicSize >= originalSize + 0.05f)
        {
            cam.orthographicSize = Mathf.Lerp(cam.orthographicSize, originalSize, zoomSmoothTime * Time.deltaTime);
            yield return null;
        }
        yield return new WaitForSeconds(1.5f);
    }

    public IEnumerator countdownToStart(Coroutine waitForZoomIn,float seconds)
    {
        yield return waitForZoomIn;

        float originalFontSize = GameController.instance.countDownText.fontSize;
        float targetFontSize = originalFontSize + 16;
        float smoothingTime = 1f;
    
        GameController.instance.countDownText.enabled = true;

        while(seconds > 0)
        {
            GameController.instance.countDownText.text = seconds.ToString();
            while (GameController.instance.countDownText.fontSize < targetFontSize )
            {
                GameController.instance.countDownText.fontSize = (int) Mathf.Lerp(originalFontSize, targetFontSize,smoothingTime * Time.deltaTime);
                yield return null;
            }
            yield return new WaitForSeconds(1.025f);
            seconds--;
         }
        GameController.instance.countDownText.text = "Go!";
        GameController.instance.startGame();
        yield return null;
    }
    

}
