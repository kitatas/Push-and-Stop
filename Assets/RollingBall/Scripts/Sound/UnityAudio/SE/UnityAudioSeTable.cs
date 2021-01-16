using System.Collections.Generic;
using UnityEngine;

namespace RollingBall.Sound.UnityAudio.SE
{
    [CreateAssetMenu(menuName = "DataTable/SeTable", fileName = "SeTable")]
    public sealed class UnityAudioSeTable : ScriptableObject
    {
        [SerializeField] private AudioClip decisionButtonClip = null;
        [SerializeField] private AudioClip cancelButtonClip = null;
        [SerializeField] private AudioClip hitClip = null;
        [SerializeField] private AudioClip clearClip = null;

        public Dictionary<SeType, AudioClip> seTable => new Dictionary<SeType, AudioClip>
        {
            {SeType.DecisionButton, decisionButtonClip},
            {SeType.CancelButton,   cancelButtonClip},
            {SeType.Hit,            hitClip},
            {SeType.Clear,          clearClip},
        };
    }
}