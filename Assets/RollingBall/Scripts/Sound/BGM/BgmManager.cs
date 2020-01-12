using System.Collections.Generic;
using UnityEngine;
using Zenject;

public sealed class BgmManager : AudioInitializer
{
    private Dictionary<BgmType, AudioClip> _bgmList = null;

    [Inject]
    private void Construct(BgmTable bgmTable)
    {
        _bgmList = new Dictionary<BgmType, AudioClip>
        {
            {BgmType.Main, bgmTable.mainClip},
        };
    }

    private void Awake()
    {
        audioSource.loop = true;

        PlayBgm(BgmType.Main);
    }

    public void PlayBgm(BgmType bgmType)
    {
        audioSource.clip = _bgmList[bgmType];
        audioSource.Play();
    }
}