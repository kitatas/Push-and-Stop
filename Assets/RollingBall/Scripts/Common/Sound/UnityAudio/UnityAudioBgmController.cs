using RollingBall.Common.Sound.BGM;
using RollingBall.Common.Utility;
using UnityEngine;
using Zenject;

namespace RollingBall.Common.Sound.UnityAudio
{
    /// <summary>
    /// BGMを管理
    /// </summary>
    public sealed class UnityAudioBgmController : BaseAudioSource, IBgmController
    {
        private AudioClip[] _bgmList;

        [Inject]
        private void Construct(UnityAudioBgmTable unityAudioBgmTable)
        {
            _bgmList = unityAudioBgmTable.GetBgmList();
        }

        private void Start()
        {
            PlayBgm(BgmType.Main);
        }

        public void PlayBgm(BgmType bgmType, bool isLoop = true)
        {
            if (_bgmList.TryGetValue((int) bgmType, out var clip))
            {
                if (audioSource.clip == clip)
                {
                    return;
                }

                audioSource.clip = clip;
                audioSource.loop = isLoop;
                audioSource.Play();
            }
        }

        public void StopBgm()
        {
            audioSource.Stop();
        }
    }
}