using UnityEngine;
using UltEvents;
using Sirenix.OdinInspector;

[CreateAssetMenu(fileName = "AudioSettings", menuName = "AudioManager/AudioSettings", order = 0)]
public class AudioSettings : ScriptableObject
{
    [Header("Volume")]
    private float _masterVolume = 1f, _musicVolume = 1f, _soundVolume = 1f;

    [ShowInInspector, PropertyRange(0f, 1f)]
    public float MasterVolume
    {
        get => _masterVolume;
        set
        {
            value = Mathf.Clamp01(value);
            _masterVolume = value;
            onMasterVolumeChanged.Invoke();
        }
    }

    [ShowInInspector, PropertyRange(0f, 1f)]
    public float MusicVolume
    {
        get => _musicVolume;
        set
        {
            value = Mathf.Clamp01(value);
            _musicVolume = value;
            onMusicVolumeChanged.Invoke();
        }
    }

    [ShowInInspector, PropertyRange(0f, 1f)]
    public float SoundVolume
    {
        get => _soundVolume;
        set
        {
            value = Mathf.Clamp01(value);
            _soundVolume = value;
            onSoundVolumeChanged.Invoke();
        }
    }

    public UltEvent onMasterVolumeChanged, onMusicVolumeChanged, onSoundVolumeChanged;
}