using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStatus : MonoBehaviour
{

    public float playerHealth = 100f;
    SphereCollider damageRadius;
    GameManager gm;
    AudioManager am;
    HUDManager hm;
    bool damaged;


    // Start is called before the first frame update
    void Start()
    {
        gm = FindObjectOfType<GameManager>();
        hm = FindObjectOfType<HUDManager>();
        am = FindObjectOfType<AudioManager>();
        damageRadius = GetComponent<SphereCollider>();

    }

    // Update is called once per frame
    void Update()
    {
        if (damaged)
        {
            hm.flashScreen();
        }
        else
        {
            hm.flashScreenOff();
        }
        damaged = false;
    }

    public void takeDamage(float damage)
    {
        damaged = true;
        am.Play("Hit");
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

    public void gameOverWin()
    {

        gm.EndGame();
        hm.ShowWinPanel();
    }
}
