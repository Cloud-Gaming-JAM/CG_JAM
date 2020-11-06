using UnityEngine.Audio;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;



public class AudioManager : MonoBehaviour
{
    [System.Serializable]
    public class Sound
    {
        public string clipName;
        public AudioClip clip;
        [HideInInspector] public AudioSource source;
        [Range(0f, 1f)] public float volume = 0.7f;
        [Range(.1f, 3f)] public float pitch = 1f;
        public bool playOnAwake = false;
        public bool loop = false;

        public Sound(float volumeParam, float pitchParam)
        {
            volume = volumeParam;
            pitch = pitchParam;
        }

        public Sound()
        {
            volume = 0.7f;
            pitch = 1f;
        }
    }
    public List<Sound> sounds = new List<Sound>() { new Sound(0.7f, 1) };
    public static AudioManager instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }
        foreach (Sound s in sounds)
        {
            if (s.source == null)
            {
                s.source = FindObjectOfType<AudioListener>().gameObject.AddComponent<AudioSource>();
            }
            s.source.volume = s.volume;
            s.source.clip = s.clip;
            s.source.playOnAwake = s.playOnAwake;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
            s.source.spatialize = false;
            s.source.spatialBlend = 1f;
        }
    }

    public void Start()
    {
        foreach (Sound s in sounds)
        {
            if (s.playOnAwake)
            {
                s.source.Play();
            }
        }
    }

    public void Play(string name)
    {
        Sound s = sounds.Find(sound => sound.clipName == name);
        if (s == null)
        {
            Debug.LogWarning("Sound " + name + " not found!");
            return;
        }

        s.source.Play();
    }

    public void Stop(string name)
    {
        Sound s = sounds.Find(sound => sound.clipName == name);
        if (s == null)
        {
            Debug.LogWarning("Sound " + name + " not found!");
            return;
        }

        s.source.Stop();
    }

    #region Edit Sound

    public void PitchChange(string name, float _pitch)
    {
        Sound s = sounds.Find(sound => sound.clipName == name);
        if (s == null)
        {
            Debug.LogWarning("Sound " + name + " not found!");
            return;
        }

        s.source.pitch = _pitch;
    }

    public void VolumeChange(string name, float _volume)
    {
        Sound s = sounds.Find(sound => sound.clipName == name);
        if (s == null)
        {
            Debug.LogWarning("Sound " + name + " not found!");
            return;
        }

        s.source.volume = _volume;
    }
    #endregion
}



