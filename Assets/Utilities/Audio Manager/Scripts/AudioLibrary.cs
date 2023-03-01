using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RuntimeSet;

[CreateAssetMenu(fileName = "AudioLibrary", menuName = "AudioManager/AudioLibrary", order = 0)]
public class AudioLibrary : RuntimeSet<AudioClip>
{
    public int latestIndexUsed = -1;
}