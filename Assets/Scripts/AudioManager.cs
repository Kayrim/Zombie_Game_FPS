using UnityEngine;
using System;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{

    public Sound[] sounds;


    // Initialising all sounds in the game into an array to be managed 
    private void Awake()
    {
        foreach (Sound s in sounds)
        {
           s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.volume = s.volume;
            s.source.loop = s.loop;
        }
    }

    public void Play(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
        {
            Debug.LogError("Sound with this file name: " + name + " not found");
        }
        s.source.Play();
    }

    private void Start()
    {
        if ((SceneManager.GetActiveScene().name == "Level1")|| (SceneManager.GetActiveScene().name == "Level2") || (SceneManager.GetActiveScene().name == "Level3"))
        {

            Play("ThemeMusic");
        }
        else Play("IntroMusic");

    }
}
