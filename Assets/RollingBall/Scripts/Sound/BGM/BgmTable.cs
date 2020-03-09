using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

[CreateAssetMenu(menuName = "DataTable/BgmTable", fileName = "BgmTable")]
public sealed class BgmTable : SerializedScriptableObject
{
    public Dictionary<BgmType, AudioClip> bgmList;
}