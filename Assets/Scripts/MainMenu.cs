using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{

    private Dropdown _dropdown;
    private int difficulty;
    private int loadLevel = 1;

    

    private void Awake()
    {
        
    }
    public void PlayGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + loadLevel);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
    public void ChangeDifficulty(int diff)
    {
        difficulty = diff;
        Debug.Log("The number to load is " + diff);

        switch (difficulty)
        {
            case 0:
                loadLevel = 1;
                break;
            case 1:
                loadLevel = 2;
                break;
            case 2:
                loadLevel = 3;
                break;
            default: loadLevel = 1;
                break;
        }
    }
}
