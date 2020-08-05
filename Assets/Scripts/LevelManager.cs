using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public static LevelManager instance;
   

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else if (instance != null)
        {
            Destroy(gameObject);
        }
    }

 

   public void loadNextLevelCall()
    {

        int buildIndex = SceneManager.GetActiveScene().buildIndex;

        if (buildIndex < SceneManager.sceneCountInBuildSettings -1)
        {
            StartCoroutine(loadLevel(buildIndex + 1));
        }
        else
        {
            GameController.instance.resetEverything();
            SceneManager.LoadScene(0);
            
        }
    }

    public void restartLevelCall()
    {
        int buildIndex = SceneManager.GetActiveScene().buildIndex;
        StartCoroutine(loadLevel(buildIndex));

    }

    public void loadLevelCall(int buildIndex)
    {
        StartCoroutine(loadLevel(buildIndex));
    }

    public IEnumerator loadLevel(int buildIndexToLoad){

        AsyncOperation asyncLoadLevel = SceneManager.LoadSceneAsync(buildIndexToLoad);
        while (!asyncLoadLevel.isDone)
        {
            Debug.Log("Loading");
            yield return null;
        }
        Debug.Log("Level loaded");
        GameController.instance.startGame();
       

    }

 




 
}
