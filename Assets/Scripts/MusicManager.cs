using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.Audio;

public class MusicManager : MonoBehaviour
{
    public AudioSource musicMixer;
    private static MusicManager instance;
    public AudioClip click;
    private AudioSource mouse;


    public static MusicManager GetInstance()
    {
        return instance;
    }
    // Start is called before the first frame update
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this);
        }
    }

    public void SetVolumemusica(float volume)   //Cambia el volumen de la Musica 
    {

        musicMixer = gameObject.GetComponent<AudioSource>();
        Debug.Log("volumen de la musica" + volume);
    }

    private void Start()
    {
        mouse = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            mouse.PlayOneShot(click, 1.0f);
        }
         
    }
}
