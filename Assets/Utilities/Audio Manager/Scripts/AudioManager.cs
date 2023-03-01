using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using DesignPatterns;

public class AudioManager : Singleton<AudioManager>
{
    public AudioSettings settings;
    private Transform musicParent, soundParent;
    private Pool<AudioSourceExtended> musicSources, soundSources;
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
        settings?.onMasterVolumeChanged.AddPersistentCall((Action)UpdateMasterVolume);
        settings?.onMusicVolumeChanged.AddPersistentCall((Action)UpdateMusicVolume);
        settings?.onSoundVolumeChanged.AddPersistentCall((Action)UpdateSoundVolume);
    }

    public void PlaySound(AudioClip clip, float minPitchRange = 1f, float maxPitchRange = 1f)
    {
        AudioSourceExtended source = soundSources.Get();
        source.Play(clip, minPitchRange: minPitchRange, maxPitchRange: maxPitchRange);
    }

    public void PlayMusic(AudioClip clip)
    {
        AudioSourceExtended source = musicSources.Get();
        source.Play(clip, true);
    }

    public void UpdateMasterVolume()
    {
        if (settings == null) return;
        UpdateMusicVolume();
        UpdateSoundVolume();
    }

    public void UpdateMusicVolume()
    {
        if (settings == null) return;
        foreach (AudioSourceExtended source in musicSources.activeObjects)
        {
            source.SetVolume(settings.MasterVolume * settings.MusicVolume);
        }
    }

    public void UpdateSoundVolume()
    {
        if (settings == null) return;
        foreach (AudioSourceExtended source in soundSources.activeObjects)
        {
            source.SetVolume(settings.MasterVolume * settings.SoundVolume);
        }
    }

}