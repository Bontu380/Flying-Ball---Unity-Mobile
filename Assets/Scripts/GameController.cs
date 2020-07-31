using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public static GameController instance;
    public bool pause = true;
    public Text countDownText;
 

    void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else if(instance != this)
        {
            Destroy(gameObject);
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void startGame()
    {
        Time.timeScale = 1f;
        pause = false;
    }

    public IEnumerator countdownToStart(float seconds)
    {


        while (seconds > 0)
        {
            //Text.text = count.toString();
            Debug.Log(seconds);
            yield return new WaitForSeconds(1.025f);
            seconds--;
        }
        Debug.Log("Go !");
        GameController.instance.startGame();
        yield return null;
    }

}
