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

        public AudioSourceExtended PlayMusic(AudioClip clip)
        {
            return AudioManager.Instance.PlayMusic(clip);
        }
        protected virtual void SetSelected(int index)
        {
            Debug.Log("Selecting index " + index);
            selectedClip = index > 0 ? Items[index] : null;
        }
    }

}