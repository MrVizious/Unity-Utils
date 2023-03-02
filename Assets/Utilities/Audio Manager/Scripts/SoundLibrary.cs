using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

namespace Audio
{

    [CreateAssetMenu(fileName = "SoundLibrary", menuName = "AudioManager/AudioLibrary/SoundLibrary", order = 0)]
    public class SoundLibrary : AudioLibrary
    {
        public float minPitchRange, maxPitchRange;

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
