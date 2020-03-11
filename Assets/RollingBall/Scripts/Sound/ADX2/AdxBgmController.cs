public sealed class AdxBgmController : BaseCriAtomSource, IBgmController
{
    private void Awake()
    {
        criAtomSource.loop = true;

        PlayBgm(BgmType.Main);
    }

    public void PlayBgm(BgmType bgmType)
    {
        criAtomSource.cueName = bgmType.ToString();
        criAtomSource.Play();
    }

    public void StopBgm()
    {
        criAtomSource.Stop();
    }
}