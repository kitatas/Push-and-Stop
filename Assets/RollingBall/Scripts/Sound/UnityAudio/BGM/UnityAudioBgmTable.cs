using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

[CreateAssetMenu(menuName = "DataTable/BgmTable", fileName = "BgmTable")]
public sealed class UnityAudioBgmTable : SerializedScriptableObject
{
    [SerializeField] private Dictionary<BgmType, AudioClip> bgmList = null;

    public Dictionary<BgmType, AudioClip> BgmList => bgmList;
}