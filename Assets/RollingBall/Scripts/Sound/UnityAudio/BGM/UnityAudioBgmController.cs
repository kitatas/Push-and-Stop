using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace RollingBall.Sound.UnityAudio.BGM
{
    /// <summary>
    /// BGMを管理
    /// </summary>
    public sealed class UnityAudioBgmController : BaseAudioSource, IBgmController
    {
        private Dictionary<BgmType, AudioClip> _bgmList;

        [Inject]
        private void Construct(UnityAudioBgmTable unityAudioBgmTable)
        {
            _bgmList = unityAudioBgmTable.bgmTable;
        }

        private void Awake()
        {
            audioSource.loop = true;

            PlayBgm(BgmType.Main);
        }

        public void PlayBgm(BgmType bgmType)
        {
            if (_bgmList.ContainsKey(bgmType) == false)
            {
                return;
            }

            audioSource.clip = _bgmList[bgmType];
            audioSource.Play();
        }

        public void StopBgm()
        {
            audioSource.Stop();
        }
    }
}