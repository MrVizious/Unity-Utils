using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

namespace Audio
{
    public class PlayAudioTrigger : MonoBehaviour
    {
        public enum AudioType
        {
            Sound,
            Music
        }
        public AudioClip clip;
        public AudioType audioType;

        [ShowIf("audioType", AudioType.Sound)]
        public float minPitchRange = 1f, maxPitchRange = 1f;

        [ShowIf("audioType", AudioType.Music)]
        public float secondsToFadeIn = 0f, secondsToFadeOut = 0f;

        [Button]
        public async void Invoke()
        {
            AudioManager audioManager = await AudioManager.GetInstance();
            switch (audioType)
            {
                case AudioType.Sound:
                    audioManager.PlaySound(clip, minPitchRange, maxPitchRange);
                    break;
                case AudioType.Music:
                    audioManager.PlayMusic(clip, secondsToFadeIn, secondsToFadeOut);
                    break;
            }
        }
    }
}
