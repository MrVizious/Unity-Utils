using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DesignPatterns;
using Sirenix.Serialization;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Audio
{

    public class AudioManager : Singleton<AudioManager>
    {
        public bool stopOnMuted = true;
        private AudioSettings _settings;
        [OdinSerialize, ExecuteAlways]
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
                Debug.Log("Setting settings");
                _settings = value;
            }
        }
        private Transform musicParent, soundParent;
        private Pool<AudioSourceExtended> musicSources, soundSources;
        private AudioSourceExtended mainMusicSource;
        private new void Awake()
        {
            base.Awake();
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

        public AudioSourceExtended PlayMusic(AudioClip clip, float secondsToFadeIn = 0f, float secondsToFadeOut = 0f)
        {
            if (stopOnMuted && settings.SoundVolume <= 0f) return null;
            if (mainMusicSource != null && mainMusicSource.isActiveAndEnabled)
            {
                mainMusicSource.Stop();
            }
            AudioSourceExtended source = musicSources.Get();
            mainMusicSource = source;
            return source.Play(clip, 1f, true, secondsToFadeIn: secondsToFadeIn, secondsToFadeOut: secondsToFadeOut);
        }

        public AudioSourceExtended PlaySound(AudioClip clip, float minPitchRange = 1f, float maxPitchRange = 1f)
        {
            Debug.Log(clip);
            if (stopOnMuted && settings.SoundVolume <= 0f) return null;
            AudioSourceExtended source = soundSources.Get();
            return source.Play(clip, settings.SoundVolume, minPitchRange: minPitchRange, maxPitchRange: maxPitchRange);
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
}