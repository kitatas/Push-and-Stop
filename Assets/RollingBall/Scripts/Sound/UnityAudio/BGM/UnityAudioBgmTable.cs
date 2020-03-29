using UnityEngine;

[CreateAssetMenu(menuName = "DataTable/BgmTable", fileName = "BgmTable")]
public sealed class UnityAudioBgmTable : ScriptableObject
{
    [SerializeField] private AudioClip mainClip = null;

    public AudioClip main => mainClip;
}