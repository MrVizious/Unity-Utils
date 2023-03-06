using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

namespace Audio
{
    [CreateAssetMenu(fileName = "MusicLibrary", menuName = "AudioManager/AudioLibrary/MusicLibrary", order = 0)]
    public class MusicLibrary : AudioLibrary
    {
        [SerializeField] private float secondsToFadeIn, secondsToFadeOut;
        public override AudioSourceExtended PlayMusic(AudioClip clip)
        {
            Debug.Log("Seconds to fade in: " + secondsToFadeIn);
            return AudioManager.Instance.PlayMusic(clip, secondsToFadeIn, secondsToFadeOut);
        }

        [Button]
        public AudioSourceExtended PlayRandomMusic()
        {
            AudioClip clip = GetRandomExcludingIndex(out latestIndexUsed, latestIndexUsed);
            return PlayMusic(clip);
        }

        [Button]
        public AudioSourceExtended PlaySelectedMusic()
        {
            return PlayMusic(selectedClip);
        }
    }
}
