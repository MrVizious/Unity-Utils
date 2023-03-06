using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

namespace Audio
{
    [CreateAssetMenu(fileName = "MusicLibrary", menuName = "AudioManager/AudioLibrary/MusicLibrary", order = 0)]
    public class MusicLibrary : AudioLibrary
    {

        public override AudioSourceExtended PlayMusic(AudioClip clip)
        {
            return AudioManager.Instance.PlayMusic(clip);
        }

        [Button]
        public AudioSourceExtended PlayRandomMusic()
        {
            AudioClip clip = GetRandomExcludingIndex(out latestIndexUsed, latestIndexUsed);
            return AudioManager.Instance.PlayMusic(clip);
        }

        [Button]
        public AudioSourceExtended PlaySelectedMusic()
        {
            return PlayMusic(selectedClip);
        }
    }
}
