using Zenject;

public sealed class BgmManager : BaseAudioSource
{
    private BgmTable _bgmTable;

    [Inject]
    private void Construct(BgmTable bgmTable)
    {
        _bgmTable = bgmTable;
    }

    private void Awake()
    {
        audioSource.loop = true;

        PlayBgm(BgmType.Main);
    }

    public void PlayBgm(BgmType bgmType)
    {
        audioSource.clip = _bgmTable.bgmList[bgmType];
        audioSource.Play();
    }

    public void StopBgm()
    {
        audioSource.Stop();
    }
}