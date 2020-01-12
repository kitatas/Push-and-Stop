using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public abstract class AudioInitializer : MonoBehaviour
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
}