using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Camera cam;
    float originalSize;
    float zoomOutSize;
    float time;
    /*void Start()
    {
        time = 3.5f;
        originalSize = cam.orthographicSize;
        zoomOutSize = originalSize + 6f;
  
    }

    // Update is called once per frame
    void Update()
    {
        zoomOut();
    }

    public void zoomOut()
    {
        cam.orthographicSize = Mathf.Lerp(cam.orthographicSize,zoomOutSize, time * Time.deltaTime);
    }*/

    void Start()
    {
        time = 3.5f;
        originalSize = cam.orthographicSize;
        zoomOutSize = originalSize + 6f;
        StartCoroutine(zoomOut());
    }

    public IEnumerator zoomOut()
    {
        while(cam.orthographicSize < zoomOutSize)
        {
            cam.orthographicSize = Mathf.Lerp(cam.orthographicSize, zoomOutSize, time * Time.deltaTime);
            yield return null;
        }
        yield return new WaitForSeconds(1.5f);
    }
}
