using RollingBall.Common.Sound.SE;
using RollingBall.Common.Utility;
using UnityEngine;
using Zenject;

namespace RollingBall.Common.Sound.UnityAudio
{
    /// <summary>
    /// SEを管理
    /// </summary>
    public sealed class UnityAudioSeController : BaseAudioSource, ISeController
    {
        private AudioClip[] _seList;

        [Inject]
        private void Construct(UnityAudioSeTable unityAudioSeTable)
        {
            _seList = unityAudioSeTable.GetSeList();
        }

        public void PlaySe(SeType seType)
        {
            if (_seList.TryGetValue((int) seType, out var clip))
            {
                audioSource.PlayOneShot(clip);
            }
        }
    }
}