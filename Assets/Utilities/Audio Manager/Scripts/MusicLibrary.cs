using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using Cysharp.Threading.Tasks;

namespace Audio
{
    [CreateAssetMenu(fileName = "MusicLibrary", menuName = "AudioManager/AudioLibrary/MusicLibrary", order = 0)]
    public class MusicLibrary : AudioLibrary
    {

        [SerializeField] private float secondsToFadeIn, secondsToFadeOut;
        public async override UniTask<AudioSourceExtended> PlayMusic(AudioClip clip)
        {
            Debug.Log("Seconds to fade in: " + secondsToFadeIn);
            AudioManager audioManager = await AudioManager.GetInstance();
            return audioManager.PlayMusic(clip, secondsToFadeIn, secondsToFadeOut);
        }

        [Button]
        public async UniTask<AudioSourceExtended> PlayRandomMusic()
        {
            AudioClip clip = GetRandomExcludingIndex(out latestIndexUsed, latestIndexUsed);
            return await PlayMusic(clip);
        }

        [Button]
        public async UniTask<AudioSourceExtended> PlaySelectedMusic()
        {
            return await PlayMusic(selectedClip);
        }
    }
}
