using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class BgmManager : AudioInitializer
{
    [Inject] private readonly BgmTable _bgmTable = default;
    private Dictionary<BgmType, AudioClip> _bgmList;

    protected override void Awake()
    {
        base.Awake();
        audioSource.loop = true;

        _bgmList = new Dictionary<BgmType, AudioClip>
        {
            {BgmType.Main, _bgmTable.mainClip},
        };

        PlayBgm(BgmType.Main);
    }

    public void PlayBgm(BgmType bgmType)
    {
        audioSource.clip = _bgmList[bgmType];
        audioSource.Play();
    }

    public void StopBgm()
    {
        audioSource.Stop();
    }
}