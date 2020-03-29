using UnityEngine;

[CreateAssetMenu(menuName = "DataTable/SeTable", fileName = "SeTable")]
public sealed class UnityAudioSeTable : ScriptableObject
{
    [SerializeField] private AudioClip decisionButtonClip = null;
    [SerializeField] private AudioClip cancelButtonClip = null;
    [SerializeField] private AudioClip hitClip = null;
    [SerializeField] private AudioClip clearClip = null;

    public AudioClip decisionButton => decisionButtonClip;
    public AudioClip cancelButton => cancelButtonClip;
    public AudioClip hit => hitClip;
    public AudioClip clear => clearClip;
}