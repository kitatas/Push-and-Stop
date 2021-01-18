using RollingBall.Common.Sound.BGM;
using UnityEngine;

namespace RollingBall.Common.Sound.UnityAudio
{
    [CreateAssetMenu(fileName = "BgmTable", menuName = "DataTable/BgmTable")]
    public sealed class UnityAudioBgmTable : ScriptableObject
    {
        [SerializeField] private AudioClip mainClip = default;

        public AudioClip[] GetBgmList()
        {
            var bgmCount = System.Enum.GetValues(typeof(BgmType)).Length;
            var bgmList = new AudioClip[bgmCount];
            bgmList[(int) BgmType.Main] = mainClip;

            return bgmList;
        }
    }
}