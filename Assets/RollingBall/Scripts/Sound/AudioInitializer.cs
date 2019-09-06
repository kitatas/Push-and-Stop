using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public abstract class AudioInitializer : MonoBehaviour
{
    protected AudioSource audioSource;

    protected virtual void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }
}