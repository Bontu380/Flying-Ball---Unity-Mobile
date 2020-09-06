using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class MenuManager : MonoBehaviour
{

    public GameObject mainMenuPanel;
    public GameObject levelSelectPanel;

    private GameObject dontDestroyObjectsForLevels;


    public Button buttonPrefab;

    private Button[] levelButtons;


    private void Awake()
    {
        assignDontDestroyObjects();
           
    }

    public void startLevel()    
    {
        int buildIndex = SceneManager.GetActiveScene().buildIndex;
        StartCoroutine(loadLevel(buildIndex + 1));
    }

    public IEnumerator loadLevel(int index)
    {
        AsyncOperation loadAsyncLevel = SceneManager.LoadSceneAsync(index);
        while(!loadAsyncLevel.isDone)
        {
           
            yield return null;
        }
    }


    public void levelSelectPage()
    {
       
        mainMenuPanel.SetActive(false);
        createButtonsForLevelPage();
        levelSelectPanel.SetActive(true);
        

    }

    public void closeLevelSelectPanel()
    {
        levelSelectPanel.SetActive(false);
        mainMenuPanel.SetActive(true);

    }

    public void createButtonsForLevelPage()
    {

        float yPos = levelSelectPanel.transform.position.y + 100f ;
        float xPos = Screen.width / 2;

     

        int levelsAvaliableForPlay = LevelManager.instance.getPassedMaxLevel();


        levelButtons = new Button[levelsAvaliableForPlay+1];

        for (int i = 0; i < levelButtons.Length; i++)
        {
        
            if(i == LevelManager.instance.totalLevelCount)
            {
                break;
            }
            
            levelButtons[i] = (Button)Instantiate(buttonPrefab);

            levelButtons[i].transform.GetChild(0).GetComponent<Text>().text += " " + (i + 1);


            levelButtons[i].transform.SetParent(levelSelectPanel.transform);

            levelButtons[i].transform.position = new Vector3(xPos, yPos, 0f);
         

          
            yPos -= 120f;

            levelButtons[i].onClick.AddListener(onClickLevelButton);

            if(i < levelButtons.Length-1 || i == LevelManager.instance.totalLevelCount)
            {
                levelButtons[i].transform.GetChild(1).gameObject.SetActive(true); 
            }

            
        }
    }


    public void onClickLevelButton()
    {
        GameObject selectedGameObject = EventSystem.current.currentSelectedGameObject;
        int buildIndex = System.Array.IndexOf(levelButtons, selectedGameObject.GetComponent<Button>());

        levelSelectPanel.SetActive(false);
        Camera.main.gameObject.SetActive(false);

        LevelManager.instance.loadLevelCall(buildIndex + 1);

        for (int i = 0; i < dontDestroyObjectsForLevels.transform.childCount; i++)
        {
            dontDestroyObjectsForLevels.transform.GetChild(i).gameObject.SetActive(true);

        }

    }

    public void assignDontDestroyObjects()
    {
      DontDestroy dontDestroyScript = GameObject.FindObjectOfType<DontDestroy>();
        dontDestroyObjectsForLevels = dontDestroyScript.gameObject;

    }



}
