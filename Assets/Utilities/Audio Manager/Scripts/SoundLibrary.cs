using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

namespace Audio
{

    [CreateAssetMenu(fileName = "SoundLibrary", menuName = "AudioManager/AudioLibrary/SoundLibrary", order = 1)]
    public class SoundLibrary : AudioLibrary
    {
        [SerializeField] private float minPitchRange = 1f, maxPitchRange = 1f;

        public AudioSourceExtended PlaySound(AudioClip clip)
        {
            return AudioManager.Instance.PlaySound(clip, minPitchRange, maxPitchRange);
        }

        [Button]
        public AudioSourceExtended PlayRandomSound()
        {
            AudioClip clip = GetRandomExcludingIndex(out latestIndexUsed, latestIndexUsed);
            return AudioManager.Instance.PlaySound(clip, minPitchRange, maxPitchRange);
        }

        [Button]
        public AudioSourceExtended PlaySelectedSound()
        {
            return PlaySound(selectedClip);
        }



    }
}
