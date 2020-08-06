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
       /* else
        {
            //Burada başka bir panel açılacak bütün bölümler bitti gibi
            GameController.instance.resetEverything();
            SceneManager.LoadScene(0);
            
        } */
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

        //yield return new WaitForSeconds(1f);
        if (buildIndexToLoad != 0)
        {
            GameController.instance.startGame();
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
        GameController.instance.resetEverything();
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
