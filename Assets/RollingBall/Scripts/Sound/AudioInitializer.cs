using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public abstract class AudioInitializer : MonoBehaviour
{
    private AudioSource _audioSource;

    protected virtual void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    protected void PlayLoop(bool value)
    {
        _audioSource.loop = value;
    }

    protected void PlayBgm(AudioClip audioClip)
    {
        _audioSource.clip = audioClip;
        _audioSource.Play();
    }

    protected void PlayOneShot(AudioClip audioClip)
    {
        _audioSource.PlayOneShot(audioClip);
    }
}