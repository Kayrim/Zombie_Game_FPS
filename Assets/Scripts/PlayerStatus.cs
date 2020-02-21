using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatus : MonoBehaviour
{

    public float playerHealth = 100f;
    SphereCollider damageRadius;
    GameManager gm;
    HUDManager hm;


    // Start is called before the first frame update
    void Start()
    {
        gm = FindObjectOfType<GameManager>();
        hm = FindObjectOfType<HUDManager>();
        damageRadius = GetComponent<SphereCollider>();

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void takeDamage(float damage)
    {
        playerHealth -= damage;
        hm.SetHealth(playerHealth);
        Debug.Log("Player has taken "+damage+ "and has " + playerHealth + "left");
        if (playerHealth <= 0)
        {
            Debug.Log("Player Dead");
             gameOverDeath();
        }
    }

    private void gameOverDeath()
    {
        gm.EndGame();
        hm.ShowDeathPanel();

    }

    private void gameOverWin()
    {

        gm.EndGame();
        hm.ShowWinPanel();
    }
}
