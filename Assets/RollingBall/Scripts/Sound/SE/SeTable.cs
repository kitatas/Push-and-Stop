using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

[CreateAssetMenu(menuName = "DataTable/SeTable", fileName = "SeTable")]
public sealed class SeTable : SerializedScriptableObject
{
    public Dictionary<SeType, AudioClip> seList;
}