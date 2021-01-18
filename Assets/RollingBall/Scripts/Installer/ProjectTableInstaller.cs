using RollingBall.Common.Sound.UnityAudio;
using UnityEngine;
using Zenject;

namespace RollingBall.Installer
{
    [CreateAssetMenu(fileName = "ProjectTableInstaller", menuName = "Installers/ProjectTableInstaller")]
    public sealed class ProjectTableInstaller : ScriptableObjectInstaller
    {
        [SerializeField] private UnityAudioBgmTable unityAudioBgmTable = default;
        [SerializeField] private UnityAudioSeTable unityAudioSeTable = default;

        public override void InstallBindings()
        {
            Container
                .BindInstance(unityAudioBgmTable)
                .AsCached();

            Container
                .BindInstance(unityAudioSeTable)
                .AsCached();
        }
    }
}