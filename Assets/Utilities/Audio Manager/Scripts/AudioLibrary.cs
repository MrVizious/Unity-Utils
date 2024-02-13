using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RuntimeSet;
using Cysharp.Threading.Tasks;


namespace Audio
{
    public abstract class AudioLibrary : RuntimeSet<AudioClip>
    {
        public int latestIndexUsed = -1;
        protected AudioClip selectedClip;

        public virtual async UniTask<AudioSourceExtended> PlayMusic(AudioClip clip)
        {
            AudioManager audioManager = await AudioManager.GetInstance();
            return audioManager.PlayMusic(clip);
        }
        public virtual async UniTask<AudioSourceExtended> PlaySound(AudioClip clip, float minPitchRange = 1f, float maxPitchRange = 1f)
        {
            AudioManager audioManager = await AudioManager.GetInstance();
            return audioManager.PlaySound(clip, minPitchRange, maxPitchRange);
        }
        protected virtual void SetSelected(int index)
        {
            selectedClip = index >= 0 ? Items[index] : null;
        }
    }

}