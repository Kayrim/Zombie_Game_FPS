using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HUDManager : MonoBehaviour
{

    private GameObject deadPanel;
    private GameObject winPanel;
    public Slider slider;
    private TextMeshProUGUI[] ammoTexts;
    

    // Start is called before the first frame update
    void Start()
    {
        ammoTexts = GameObject.Find("HUD").transform.GetComponentsInChildren<TextMeshProUGUI>();
        slider = GameObject.Find("HUD").transform.GetComponentInChildren<Slider>();
        deadPanel = GameObject.Find("HUD").transform.Find("DeathPanel").gameObject;
        winPanel = GameObject.Find("HUD").transform.Find("WinPanel").gameObject;
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
        ammoTexts[0].text = ammoLeft + "/6";
    }

    public void ReloadingWeapon()
    {
        ammoTexts[1].text = "Reloading...";
    }

    public void ReloadComplete()
    {
        ammoTexts[1].text = "";
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
