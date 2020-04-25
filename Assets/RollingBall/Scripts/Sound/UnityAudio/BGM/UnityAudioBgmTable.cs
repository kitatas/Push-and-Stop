using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "DataTable/BgmTable", fileName = "BgmTable")]
public sealed class UnityAudioBgmTable : ScriptableObject
{
    [SerializeField] private AudioClip mainClip = null;

    public Dictionary<BgmType, AudioClip> bgmTable => new Dictionary<BgmType, AudioClip>
    {
        {BgmType.Main, mainClip},
    };
}