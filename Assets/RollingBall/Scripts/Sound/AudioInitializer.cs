using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public abstract class AudioInitializer : MonoBehaviour
{
    protected AudioSource audioSource { get; private set; }

    protected virtual void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }
}