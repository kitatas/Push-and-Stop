using System.Collections.Generic;
using UnityEngine;

namespace RollingBall.Sound.UnityAudio.BGM
{
    [CreateAssetMenu(menuName = "DataTable/BgmTable", fileName = "BgmTable")]
    public sealed class UnityAudioBgmTable : ScriptableObject
    {
        [SerializeField] private AudioClip mainClip = null;

        public Dictionary<BgmType, AudioClip> bgmTable => new Dictionary<BgmType, AudioClip>
        {
            {BgmType.Main, mainClip},
        };
    }
}