using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleScreen : MonoBehaviour
{

    public string newGameScene; 
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void MainMenu() {

    }

    public void StartGame() {
        SceneManager.LoadScene(newGameScene);
    }

    public void Quit() {
        Debug.Log("Quit");
        Application.Quit();
    }
}