using Zenject;

public sealed class UnityAudioBgmController : BaseAudioSource, IBgmController
{
    private UnityAudioBgmTable _unityAudioBgmTable;

    [Inject]
    private void Construct(UnityAudioBgmTable unityAudioBgmTable)
    {
        _unityAudioBgmTable = unityAudioBgmTable;
    }

    private void Awake()
    {
        audioSource.loop = true;

        PlayBgm(BgmType.Main);
    }

    public void PlayBgm(BgmType bgmType)
    {
        if (_unityAudioBgmTable.BgmList.ContainsKey(bgmType) == false)
        {
            return;
        }

        audioSource.clip = _unityAudioBgmTable.BgmList[bgmType];
        audioSource.Play();
    }

    public void StopBgm()
    {
        audioSource.Stop();
    }
}