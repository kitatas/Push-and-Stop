using Zenject;

public sealed class SeManager : BaseAudioSource
{
    private SeTable _seTable;

    [Inject]
    private void Construct(SeTable seTable)
    {
        _seTable = seTable;
    }

    public void PlaySe(SeType seType)
    {
        audioSource.PlayOneShot(_seTable.seList[seType]);
    }
}