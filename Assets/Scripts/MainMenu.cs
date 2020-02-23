using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    private Dropdown _dropdown;
    public static int difficulty = 1;    

    private void Awake()
    {
        
    }
    public void PlayGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
    public void ChangeDifficulty(int diff)
    {
        difficulty = diff+1;
        Debug.Log("The Diffculty is " + difficulty);
    }
}
