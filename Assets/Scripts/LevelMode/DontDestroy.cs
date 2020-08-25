using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontDestroy : MonoBehaviour
{
    private static DontDestroy instance;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != null)
        {
            Destroy(gameObject);
        }
       // deactivateChildren();
    }
    void Start()
    {
        GameObject[] objs = GameObject.FindGameObjectsWithTag(this.gameObject.tag);

        if (objs.Length > 1)
        {
            Destroy(this.gameObject);
        }
        //LevelManager.instance.additiveObjects.Add(this.gameObject);
        DontDestroyOnLoad(this.gameObject);

    }

    /*
    public void deactivateChildren()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            if (transform.GetChild(i).tag != "Manager" || transform.GetChild(i).tag != "MainCamera")
            {
                transform.GetChild(i).gameObject.SetActive(false);
            }
        }
    }
    */
}
