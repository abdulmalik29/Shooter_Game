using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;
    public Sound[] sounds;

    void Awake()
    {
        foreach (Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();

            s.source.clip = s.clip;
            s.source.loop = s.loop;
            s.source.pitch = s.pitch;
            s.source.volume = s.volume;

        }
    }

    // Update is called once per frame
    public void Play(string sound)
    {
        Debug.Log("test");
        Sound s = Array.Find(sounds, item => item.name == sound);
        if (s == null)
        {
            Debug.LogWarning("sound not found");
            return;
        }

        s.source.Play();
    }
}
