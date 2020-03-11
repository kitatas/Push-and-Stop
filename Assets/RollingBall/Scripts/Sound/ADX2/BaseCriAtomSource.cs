using UnityEngine;

[RequireComponent(typeof(CriAtomSource))]
public abstract class BaseCriAtomSource : MonoBehaviour, IVolumeUpdatable
{
    private CriAtomSource _criAtomSource;

    protected CriAtomSource criAtomSource
    {
        get
        {
            if (_criAtomSource == null)
            {
                _criAtomSource = GetComponent<CriAtomSource>();
            }

            return _criAtomSource;
        }
    }

    public float GetVolume() => criAtomSource.volume;

    public void SetVolume(float value) => criAtomSource.volume = value;
}