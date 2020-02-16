using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public abstract class BaseAudioSource : MonoBehaviour
{
    private AudioSource _audioSource;

    protected AudioSource audioSource
    {
        get
        {
            if (_audioSource == null)
            {
                _audioSource = GetComponent<AudioSource>();
            }

            return _audioSource;
        }
    }

    public float GetVolume() => audioSource.volume;

    public void SetVolume(float value) => audioSource.volume = value;
}