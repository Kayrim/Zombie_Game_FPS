using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    bool gameEnded = false;
    public float restartDelay = 3f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void EndGame()
    {
        if (gameEnded == false)
        {
            gameEnded = true;
            Invoke("Restart", restartDelay);        
        }
    }

    void Restart()
    {
        SceneManager.LoadScene("Menu");
        Cursor.lockState = CursorLockMode.None;
    }
}
