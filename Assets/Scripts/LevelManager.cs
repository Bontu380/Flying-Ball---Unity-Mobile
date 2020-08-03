using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public static LevelManager instance;
    public List<GameObject> additiveObjects;

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

 

    public void loadNextLevel()
    {
       
      SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+1);
      GameController.instance.startGame();

     /*
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+1, LoadSceneMode.Additive);

        for (int i = 0; i < additiveObjects.Count; i++)
        {
            SceneManager.MoveGameObjectToScene(additiveObjects[i].gameObject,SceneManager.GetSceneByName("Level2"));
        }
    */

    }

    public void restartLevel()
    {
       
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        GameController.instance.startGame();
    }
}
