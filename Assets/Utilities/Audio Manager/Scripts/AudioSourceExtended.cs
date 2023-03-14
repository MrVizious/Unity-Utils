using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UltEvents;
using Cysharp.Threading.Tasks;
using ExtensionMethods;
using DesignPatterns;
using Sirenix.OdinInspector;

namespace Audio
{

    [RequireComponent(typeof(AudioSource))]
    public class AudioSourceExtended : Poolable<AudioSourceExtended>
    {
        public AudioSource audioSource { get; private set; }
        public UltEvent onEndedPlaying;

        private float targetVolume, secondsToFadeOut;
        private bool fadingIn;

        private void Awake()
        {
            audioSource = gameObject.GetOrAddComponent<AudioSource>();
            audioSource.playOnAwake = false;
            if (onEndedPlaying == null)
            {
                onEndedPlaying = new UltEvent();
            }
        }

        public AudioSourceExtended Play(AudioClip clip, float volume, bool loop = false, float minPitchRange = 1f, float maxPitchRange = 1f, float secondsToFadeIn = 0f, float secondsToFadeOut = 0f)
        {
            // If the audio source is already in use, skip it
            if (audioSource.isPlaying) return null;

            targetVolume = volume;

#pragma warning disable CS4014
            FadeIn(secondsToFadeIn);
#pragma warning restore CS4014

            this.secondsToFadeOut = secondsToFadeOut;
            audioSource.loop = loop;

            audioSource.clip = clip;
            audioSource.pitch = Random.Range(minPitchRange, maxPitchRange);
            audioSource.Play();

            if (!loop)
            {
                ReleaseOnPlayingEnd();
            }

            return this;
        }

        [Button]
        public async void Stop()
        {
            await FadeOut(secondsToFadeOut);
            pool?.Release(this);
        }

        [Button]
        public void StopImmediately()
        {
            pool?.Release(this);
        }

        public void SetVolume(float volume)
        {
            volume = Mathf.Clamp01(volume);
            float ratio = volume / audioSource.volume;
            audioSource.volume = volume;
            targetVolume *= ratio;
            targetVolume = Mathf.Clamp01(targetVolume);
        }

        private async UniTask ListenForPlayingEnd()
        {
            await UniTask.WaitUntil(() => audioSource.isPlaying == false);
            onEndedPlaying?.Invoke();
        }

        private async void ReleaseOnPlayingEnd()
        {
            await ListenForPlayingEnd();
            pool?.Release(this);
        }

        public override void OnPoolRelease()
        {
            Debug.Log("Deactivating");
            gameObject.SetActive(false);
        }

        public override void OnPoolGet()
        {
            Debug.Log("Activating");
            gameObject.SetActive(true);
        }

        private async UniTaskVoid FadeIn(float secondsToFade)
        {
            fadingIn = true;
            if (secondsToFade <= 0f)
            {
                Debug.Log("Fading with 0");
                audioSource.volume = targetVolume;
                return;
            }
            audioSource.volume = 0f;
            float step = targetVolume / secondsToFade;
            while (audioSource.volume < targetVolume)
            {
                if (!fadingIn) return;
                audioSource.volume += step * Time.deltaTime;
                await UniTask.Yield();
            }
            audioSource.volume = targetVolume;
        }

        private async UniTask FadeOut(float secondsToFade)
        {
            if (fadingIn) fadingIn = false;
            float step = audioSource.volume / secondsToFade;
            while (audioSource.volume > 0f)
            {
                audioSource.volume -= step * Time.deltaTime;
                await UniTask.Yield();
            }
            audioSource.volume = 0f;
            audioSource.Stop();
        }
    }

}