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

    public Button buttonPrefab;

    private Button[] levelButtons;

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


    public void levelSelectPage()
    {
       
        mainMenuPanel.SetActive(false);
        createButtonsForLevelPage();
        levelSelectPanel.SetActive(true);
        

        


    }

    public void createButtonsForLevelPage()
    {

        float yPos = Screen.height / 2;
        float xPos = Screen.width / 2;

        //kaç level button olacağını kullanıcının önceden geçtiği levellere göre databaseden çekmek mantıklı

        levelButtons = new Button[SceneManager.sceneCountInBuildSettings-1]; 

        for (int i = 0; i < levelButtons.Length; i++)
        {
        

            levelButtons[i] = (Button)Instantiate(buttonPrefab);

            levelButtons[i].transform.GetChild(0).GetComponent<Text>().text += " " + (i + 1);

            levelButtons[i].transform.SetParent(levelSelectPanel.transform);

            levelButtons[i].transform.position = new Vector3(xPos , yPos, 0f);
            yPos -= 50f;

            levelButtons[i].onClick.AddListener(onClickLevelButton);

            
        }
    }


    void onClickLevelButton()
    {
        GameObject selectedGameObject = EventSystem.current.currentSelectedGameObject;
        int buildIndex = System.Array.IndexOf(levelButtons, selectedGameObject.GetComponent<Button>());
        StartCoroutine(loadLevel(buildIndex+1));

    }
}
