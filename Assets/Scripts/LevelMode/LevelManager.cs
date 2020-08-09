using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public static LevelManager instance;
    public int totalLevelCount = 3;
    private bool isMaxLevelReached;

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

       // setPassedMaxLevel(0);

    }

    //private void Start()
   // {
        //totalLevelCount = getLevelCount();
              
    //}



    public void loadNextLevelCall()
    {
        //Burasi da düzenlenecek, menüye dön diye bir panel çıkarırız
        int buildIndex = SceneManager.GetActiveScene().buildIndex;

        if (buildIndex < SceneManager.sceneCountInBuildSettings -1)
        {
            StartCoroutine(loadLevel(buildIndex + 1));
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

        GameController.instance.allLevelsPassedPanel.SetActive(false);
        GameController.instance.levelPassedPanel.SetActive(false);
        GameController.instance.levelFailedPanel.SetActive(false);

        GameController.instance.loadingScreen.SetActive(true);

        AsyncOperation asyncLoadLevel = SceneManager.LoadSceneAsync(buildIndexToLoad);
        while (!asyncLoadLevel.isDone)
        {
            Debug.Log("Loading");
            yield return null;
        }
        Debug.Log("Level loaded");

       
        GameController.instance.loadingScreen.SetActive(false);

        if (buildIndexToLoad != 0)
        {
            GameController.instance.startGame();
        }
        else
        {
           
            GameController.instance.deactivateEverything();
            GameController.instance.resumeGame();
        }
        
       
    }

    public void checkIfNewLevelUnlocked()
    {
        int currentLevel = SceneManager.GetActiveScene().buildIndex;

        if (currentLevel > LevelManager.instance.totalLevelCount)
        {
            return;
        }

        int maxPassedLevel = getPassedMaxLevel();

        if (currentLevel > maxPassedLevel)
        {
            setPassedMaxLevel(currentLevel); //Index = 0 zaten menu ekrani

        }
    }

    public void goToMenu()
    {

       // Coroutine waitToLoad = StartCoroutine(loadLevel(0));
       // GameController.instance.resetEverything(waitToLoad);

         StartCoroutine(loadLevel(0));
         
    }


    //CALISMIYOR EXCEPTION FIRLATIYOR

    public int getLevelCount()
    {
        int count = 0;

        string keyword = "Level";

       

        for (int i = 0; i < SceneManager.sceneCountInBuildSettings; i++)
        {
        
          Scene sceneToCheck = SceneManager.GetSceneByBuildIndex(i);
          if (sceneToCheck.name.Contains(keyword))
          {
               
            count++;
          }
        }
        return count;
    }

    public int getPassedMaxLevel()
    {
        return PlayerPrefs.GetInt("PassedMaxLevel");
    }

    public void setPassedMaxLevel(int levelIndex)
    {
        PlayerPrefs.SetInt("PassedMaxLevel", levelIndex);
    }

    public bool checkIfMaxLevelReached()
    {
        bool flag = false;

        int passedMaxLevel = SceneManager.GetActiveScene().buildIndex;

        if (passedMaxLevel >= totalLevelCount)
        {
            flag = true;
        }

        return flag;
    }



  








}
