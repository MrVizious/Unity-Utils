using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UltEvents;
using Cysharp.Threading.Tasks;
using ExtensionMethods;
using DesignPatterns;
using UnityEngine.Pool;

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

    public async void Play(AudioClip clip, bool loop = false, float minPitchRange = 1f, float maxPitchRange = 1f)
    {
        Debug.Log("Trying to play");
        // If the audio source is already in use, skip it
        if (audioSource.isPlaying) return;
        Debug.Log("Playing");

        audioSource.loop = loop;

        audioSource.clip = clip;
        audioSource.pitch = Random.Range(minPitchRange, maxPitchRange);
        audioSource.Play();

        if (!loop)
        {
            await ListenForPlayingEnd();
            pool?.Release(this);
        }

    }

    public async void Stop()
    {
        audioSource.Stop();
        await ListenForPlayingEnd();
        pool?.Release(this);
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
        Debug.Log("Finished playing!");
    }

    public override void OnPoolRelease()
    {
        Debug.Log("Releasing");
        gameObject.SetActive(false);
    }

    public override void OnPoolGet()
    {
        Debug.Log("Getting");
        gameObject.SetActive(true);
    }

}
