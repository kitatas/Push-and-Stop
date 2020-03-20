using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

[CreateAssetMenu(menuName = "DataTable/SeTable", fileName = "SeTable")]
public sealed class UnityAudioSeTable : SerializedScriptableObject
{
    [SerializeField] private Dictionary<SeType, AudioClip> seList;

    public Dictionary<SeType, AudioClip> SeList => seList;
}