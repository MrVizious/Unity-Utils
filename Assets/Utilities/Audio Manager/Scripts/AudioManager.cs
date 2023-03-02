using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using DesignPatterns;
using Sirenix.OdinInspector;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class AudioManager : Singleton<AudioManager>
{
    public bool stopOnMuted = true;
    private AudioSettings _settings;
    [ShowInInspector]
    public AudioSettings settings
    {
        get
        {
            if (_settings == null)
            {
                _settings = AssetDatabase.LoadAssetAtPath<AudioSettings>("Assets/Settings/AudioSettingsAuto.asset");
            }
            if (_settings == null)
            {
                _settings = ScriptableObject.CreateInstance<AudioSettings>();
                _settings.name = "AudioSettings";
#if UNITY_EDITOR
                AssetDatabase.CreateAsset(_settings, "Assets/Settings/AudioSettingsAuto.asset");
#endif
            }
            return _settings;
        }
        set
        {
            _settings = value;
        }
    }
    private Transform musicParent, soundParent;
    private Pool<AudioSourceExtended> musicSources, soundSources;
    private AudioSourceExtended mainMusicSource;
    private void Start()
    {
        // Start GameObjects
        musicParent = new GameObject("Musics").transform;
        musicParent.parent = transform;
        soundParent = new GameObject("Sounds").transform;
        soundParent.parent = transform;

        // Create Pools
        musicSources = new Pool<AudioSourceExtended>(1, 5, newObjectName: "Music source", parent: musicParent);
        soundSources = new Pool<AudioSourceExtended>(2, 15, newObjectName: "Sound source", parent: soundParent);


        // Set Volume changes listeners
        settings.onMasterVolumeChanged += UpdateMasterVolume;
        settings.onMusicVolumeChanged += UpdateMusicVolume;
        settings.onSoundVolumeChanged += UpdateSoundVolume;

        if (stopOnMuted)
        {
            settings.onMasterMuted += StopMaster;
            settings.onMusicMuted += StopAllMusics;
            settings.onSoundMuted += StopAllSounds;
        }
    }

    public AudioSourceExtended PlayMusic(AudioClip clip)
    {
        if (stopOnMuted && settings.SoundVolume <= 0f) return null;
        AudioSourceExtended source = musicSources.Get();
        if (mainMusicSource)
        {
            mainMusicSource.Stop();
        }
        mainMusicSource = source;
        if (settings == null)
        {
            return source.Play(clip, 1f, true);
        }
        else
        {
            return source.Play(clip, settings.MusicVolume, true);
        }
    }

    public AudioSourceExtended PlaySound(AudioClip clip, float minPitchRange = 1f, float maxPitchRange = 1f)
    {
        if (stopOnMuted && settings.SoundVolume <= 0f) return null;
        AudioSourceExtended source = soundSources.Get();
        return source.Play(clip, settings.SoundVolume, minPitchRange: minPitchRange, maxPitchRange: maxPitchRange);
    }

    public AudioSourceExtended PlayRandomMusic(AudioLibrary library)
    {
        return PlayMusic(library.GetRandom());
    }

    public AudioSourceExtended PlayRandomSound(AudioLibrary library, float minPitchRange = 1f, float maxPitchRange = 1f)
    {
        AudioSourceExtended source = soundSources.Get();
        return PlaySound(library.GetRandom(), minPitchRange: minPitchRange, maxPitchRange: maxPitchRange);
    }

    public void UpdateMasterVolume()
    {
        if (settings == null) return;
        UpdateMusicVolume();
        UpdateSoundVolume();
    }

    public void UpdateMusicVolume()
    {
        float newVolume = settings.MasterVolume * settings.MusicVolume;
        foreach (AudioSourceExtended source in musicSources.allObjects)
        {
            source.SetVolume(newVolume);
        }
    }

    public void UpdateSoundVolume()
    {
        float newVolume = settings.MasterVolume * settings.SoundVolume;
        foreach (AudioSourceExtended source in soundSources.allObjects)
        {
            source.SetVolume(newVolume);
        }
    }

    public void StopMaster()
    {
        StopAllMusics();
        StopAllSounds();
    }

    public void StopAllMusics()
    {
        foreach (AudioSourceExtended source in musicSources.activeObjects)
        {
            source.Stop();
        }
    }

    public void StopAllSounds()
    {
        foreach (AudioSourceExtended source in soundSources.activeObjects)
        {
            source.Stop();
        }
    }

}