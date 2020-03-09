using Zenject;

public sealed class UnityAudioSeController : BaseAudioSource, ISeController
{
    private UnityAudioSeTable _unityAudioSeTable;

    [Inject]
    private void Construct(UnityAudioSeTable unityAudioSeTable)
    {
        _unityAudioSeTable = unityAudioSeTable;
    }

    public void PlaySe(SeType seType)
    {
        audioSource.PlayOneShot(_unityAudioSeTable.seList[seType]);
    }
}