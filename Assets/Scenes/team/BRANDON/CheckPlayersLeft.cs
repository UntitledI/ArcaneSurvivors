using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NewBehaviourScript : MonoBehaviour
{
    GameObject g;
    // Start is called before the first frame update
    void Start()
    {   
    }

    // Update is called once per frame
    void Update()
    {
        //Try and find the main character objects
        g = GameObject.Find("PlayerObj");
        if(g){
            //If the object does not equal null then keep the game going. Do nothing basically
        }else{
            //transfer scene
            GameOver("GameOverScreen");
        }
    }

    public void GameOver(string sceneName){
        SceneManager.LoadScene(sceneName);
    }
}
