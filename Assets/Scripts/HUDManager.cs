using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HUDManager : MonoBehaviour
{
    private int killTracker = 0;
    public int killGoal = 50;
    private GameObject deadPanel;
    private GameObject winPanel;
    public Slider slider;
    public Image damagePanel;
    public Color flashColor = new Color(1f, 0f, 0f, 0.1f);
    public float flashSpeed = 5f;
    private TextMeshProUGUI[] hudTexts;
    PlayerStatus playerStatus;
    

    // Start is called before the first frame update
    void Start()
    {

        playerStatus = FindObjectOfType<PlayerStatus>();
        hudTexts = GameObject.Find("HUD").transform.GetComponentsInChildren<TextMeshProUGUI>();
        slider = GameObject.Find("HUD").transform.GetComponentInChildren<Slider>();
        deadPanel = GameObject.Find("HUD").transform.Find("DeathPanel").gameObject;
        winPanel = GameObject.Find("HUD").transform.Find("WinPanel").gameObject;
        if (MainMenu.difficulty == 1)
        {
            killGoal = 5;
        }
        else if (MainMenu.difficulty == 2)
        {
            killGoal = 10;
        }
        else if (MainMenu.difficulty == 3)
        {
            killGoal = 15;
        }
    }
    public void ShowDeathPanel()
    {
        deadPanel.SetActive(true);
    }

    public void ShowWinPanel()
    {
        winPanel.SetActive(true);
    }

    public void SetHealth(float health)
    {
        slider.value = health;
    }

    public void ammoCount(float ammoLeft)
    {
        hudTexts[0].text = ammoLeft.ToString();
    }

    public void ReloadingWeapon()
    {
        hudTexts[1].text = "Reloading...";
    }

    public void UpdateKills()
    {
        killTracker++;
        hudTexts[2].text = killTracker + " / " + killGoal;
    }

    public void ReloadComplete()
    {
        hudTexts[1].text = "";
    }
    public void flashScreen()
    {
        damagePanel.color = flashColor;
    }
    public void flashScreenOff()
    {
        damagePanel.color = Color.Lerp(damagePanel.color, Color.clear, flashSpeed * Time.deltaTime);
    }
    // Update is called once per frame
    void Update()
    {
        if (killTracker >= killGoal)
        {
            playerStatus.gameOverWin();
        }
    }
}
