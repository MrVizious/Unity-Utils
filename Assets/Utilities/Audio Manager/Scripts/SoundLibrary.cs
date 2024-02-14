using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using Cysharp.Threading.Tasks;

namespace Audio
{

    [CreateAssetMenu(fileName = "SoundLibrary", menuName = "AudioManager/AudioLibrary/SoundLibrary", order = 1)]
    public class SoundLibrary : AudioLibrary
    {
        [SerializeField] private float minPitchRange = 1f, maxPitchRange = 1f;

        public async UniTask<AudioSourceExtended> PlaySound(AudioClip clip)
        {
            AudioManager audioManager = await AudioManager.GetInstance();
            return audioManager.PlaySound(clip, minPitchRange, maxPitchRange);
        }

        [Button]
        public async UniTask<AudioSourceExtended> PlayRandomSound()
        {
            AudioManager audioManager = await AudioManager.GetInstance();
            AudioClip clip = GetRandomExcludingIndex(out latestIndexUsed, latestIndexUsed);
            return audioManager.PlaySound(clip, minPitchRange, maxPitchRange);
        }

        [Button]
        public async UniTask<AudioSourceExtended> PlaySelectedSound()
        {
            return await PlaySound(selectedClip);
        }



    }
}
