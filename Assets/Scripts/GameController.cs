using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public static GameController instance;
    public bool pause = true;
    public Text countDownText;
    public GameObject levelPassedPanel;
    public GameObject levelFailedPanel;
    public GameObject player;
 

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


    public void startGame()
    {
        Time.timeScale = 1f;
        Rigidbody2D playerRb = player.GetComponent<Rigidbody2D>();
        playerRb.gravityScale = 1f;
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

    public void die()
    {
        pauseGame();
        levelPassedPanel.SetActive(true);
    }

    public void levelPassed()
    {
        pauseGame();
        levelPassedPanel.SetActive(true);
    }

    public void  pauseGame()
    {
        pause = true;
        Rigidbody2D playerRb = player.GetComponent<Rigidbody2D>();
        playerRb.velocity = Vector2.zero;
        playerRb.gravityScale = 0;

    }

}
