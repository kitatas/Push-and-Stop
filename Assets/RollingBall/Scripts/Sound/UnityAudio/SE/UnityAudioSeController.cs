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
        if (_unityAudioSeTable.SeList.ContainsKey(seType) == false)
        {
            return;
        }

        audioSource.PlayOneShot(_unityAudioSeTable.SeList[seType]);
    }
}