using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RuntimeSet;

namespace Audio
{
    public abstract class AudioLibrary : RuntimeSet<AudioClip>
    {
        public int latestIndexUsed = -1;
        protected AudioClip selectedClip;

        public virtual AudioSourceExtended PlayMusic(AudioClip clip)
        {
            return AudioManager.Instance.PlayMusic(clip);
        }
        public virtual AudioSourceExtended PlaySound(AudioClip clip, float minPitchRange = 1f, float maxPitchRange = 1f)
        {
            return AudioManager.Instance.PlaySound(clip, minPitchRange, maxPitchRange);
        }
        protected virtual void SetSelected(int index)
        {
            selectedClip = index >= 0 ? Items[index] : null;
        }
    }

}