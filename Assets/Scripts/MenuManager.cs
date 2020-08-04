using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public void startLevel()    //BURALAR TAMAMEN GEÇİCİ OYUN BİTİNCE DÖNÜLECEK
    {
        int buildIndex = SceneManager.GetActiveScene().buildIndex;
        StartCoroutine(loadLevel(buildIndex + 1));
    }

    public IEnumerator loadLevel(int index)
    {
        AsyncOperation loadAsyncLevel = SceneManager.LoadSceneAsync(index);
        while(!loadAsyncLevel.isDone)
        {
            Debug.Log("Loading");
            yield return null;
        }
    }
}
