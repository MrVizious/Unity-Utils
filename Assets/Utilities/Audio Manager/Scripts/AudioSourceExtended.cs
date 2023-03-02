using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UltEvents;
using Cysharp.Threading.Tasks;
using ExtensionMethods;
using DesignPatterns;

namespace Audio
{

    [RequireComponent(typeof(AudioSource))]
    public class AudioSourceExtended : Poolable<AudioSourceExtended>
    {
        public AudioSource audioSource { get; private set; }

        public UltEvent onEndedPlaying;
        private void Awake()
        {
            audioSource = gameObject.GetOrAddComponent<AudioSource>();
            audioSource.playOnAwake = false;
            if (onEndedPlaying == null)
            {
                onEndedPlaying = new UltEvent();
            }
        }

        public AudioSourceExtended Play(AudioClip clip, float volume, bool loop = false, float minPitchRange = 1f, float maxPitchRange = 1f)
        {
            // If the audio source is already in use, skip it
            if (audioSource.isPlaying) return null;

            audioSource.volume = volume;
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

        public void Stop()
        {
            audioSource.Stop();
        }

        public void SetVolume(float volume)
        {
            volume = Mathf.Clamp01(volume);
            audioSource.volume = volume;
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
            gameObject.SetActive(false);
        }

        public override void OnPoolGet()
        {
            gameObject.SetActive(true);
        }

    }

}