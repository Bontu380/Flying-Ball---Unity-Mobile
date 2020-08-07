using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Camera cam;
    public float originalSize;
    public float countDownTime = 3f;
    public GameObject player;
    private Vector3 offset;


    public static CameraController instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != null)
        {
            Destroy(gameObject);
        }
    }
    void Start()
    {
     
        offset = transform.position - player.transform.position;
        originalSize = GameController.instance.originalCamSize;
    }


    private void Update()
    {
        transform.position = player.transform.position + offset;
    }



    public IEnumerator zoomOut(float targetZoomOutSize,float time)
    {
        while (cam.orthographicSize < targetZoomOutSize - 0.1f)
        {
            cam.orthographicSize = Mathf.Lerp(cam.orthographicSize, targetZoomOutSize, time * Time.deltaTime);
            //cam.orthographicSize = Mathf.MoveTowards(cam.orthographicSize, zoomOutSize, denemeMoveTowards * Time.deltaTime);
            yield return null;
        }
        yield return new WaitForSeconds(1.5f);
    }

    public IEnumerator zoomIn(Coroutine waitForZoomOut, float targetZoomInSize,float time)
    {

        yield return waitForZoomOut;

        while (cam.orthographicSize > targetZoomInSize + 0.1f)
        {

            cam.orthographicSize = Mathf.Lerp(cam.orthographicSize, originalSize, time * Time.deltaTime);
            yield return null;
        }

        yield return new WaitForSeconds(1.5f);
    }

    public IEnumerator countdownToStart(Coroutine waitForZoomIn, float seconds)
    {
        yield return waitForZoomIn;

        int differenceBetweenTextFonts = 32;
        int originalFontSize = GameController.instance.countDownText.fontSize;
        int targetFontSize = originalFontSize + differenceBetweenTextFonts;

        float smoothingTime = 0f;
        float duration = 0.6f;


        GameController.instance.countDownText.enabled = true;

        while (seconds > 0)
        {

            GameController.instance.countDownText.text = seconds.ToString();


            while (GameController.instance.countDownText.fontSize < targetFontSize)
            {
                smoothingTime += Time.deltaTime / duration;
                GameController.instance.countDownText.fontSize = (int)Mathf.Lerp(originalFontSize, targetFontSize, smoothingTime);
                yield return null;
            }

            smoothingTime = 0f;

            while (GameController.instance.countDownText.fontSize > originalFontSize)
            {
                smoothingTime += Time.deltaTime / duration;
                GameController.instance.countDownText.fontSize = (int)Mathf.Lerp(targetFontSize, originalFontSize, smoothingTime);
                yield return null;
            }


            yield return new WaitForSeconds(0.4f); //1 saniyeye tamamlamak için
            seconds--;
            smoothingTime = 0f;

        }
        GameController.instance.countDownText.text = "Go!";
        yield return new WaitForSeconds(0.5f);
 
        GameController.instance.countDownText.enabled = false;
        GameController.instance.resumeGame();
        yield return null;
    }

    public void startZoomOutInSequence(float originalSize,float zoomOutSize,float time)
    {
        
        Coroutine waitForZoomOut = StartCoroutine(zoomOut(zoomOutSize,time));
        Coroutine waitForZoomIn = StartCoroutine(zoomIn(waitForZoomOut, originalSize,time));
        StartCoroutine(countdownToStart(waitForZoomIn, countDownTime));

    }

}
