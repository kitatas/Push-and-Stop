using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace RollingBall.Sound.UnityAudio.SE
{
    /// <summary>
    /// SEを管理
    /// </summary>
    public sealed class UnityAudioSeController : BaseAudioSource, ISeController
    {
        private Dictionary<SeType, AudioClip> _seList;

        [Inject]
        private void Construct(UnityAudioSeTable unityAudioSeTable)
        {
            _seList = unityAudioSeTable.seTable;
        }

        public void PlaySe(SeType seType)
        {
            if (_seList.ContainsKey(seType) == false)
            {
                return;
            }

            audioSource.PlayOneShot(_seList[seType]);
        }
    }
}