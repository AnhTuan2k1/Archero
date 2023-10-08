using System;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public Sound[] sounds;
    public GameObject btnPause;
    public GameObject btnUnPause;
    //public AudioClip audioClipPaused;

    public static AudioManager instance;

    private void Awake()
    {
        if (instance == null) instance = this;
        else
        {
            Destroy(this);
            return;
        }

        //DontDestroyOnLoad(this);
        foreach (Sound s in sounds)
        {
            s.audioSource = gameObject.AddComponent<AudioSource>();
            s.audioSource.clip = s.clip;

            s.audioSource.volume = s.volume;
            s.audioSource.pitch = s.pitch;
            s.audioSource.spatialBlend = s.spatialBlend;
            s.audioSource.loop = s.loop;
            s.audioSource.playOnAwake = false;
        }
    }

    private void Start()
    {
        //Play("BossTheme"/*, transform.position*/);
    }

    public void PlaySound(string name)
    {
        Sound s = Array.Find(sounds, x => x.name == name);
        if (s == null)
        {
            Debug.Log("Sound " + name + ": not found");
            return;
        }
        else
        {
            s.audioSource.Play();
        }

    }

    public void PauseSound(string name)
    {
        Sound s = Array.Find(sounds, x => x.name == name);
        if (s == null)
        {
            Debug.Log("Sound " + name + ": not found");
            return;
        }

        btnPause.SetActive(false);
        btnUnPause.SetActive(true);
        s.audioSource.Pause();
        //audioClipPaused = s.audioSource.clip;
    }

    public void UnPauseSound(string name)
    {
        Sound s = Array.Find(sounds, x => x.name == name);
        if (s == null)
        {
            Debug.Log("Sound " + name + ": not found");
            return;
        }

        btnPause.SetActive(true);
        btnUnPause.SetActive(false);
        s.audioSource.UnPause();
        //audioClipPaused = null;
    }
}