using RollingBall.Common.Sound.UnityAudio;
using RollingBall.Common.Transition;
using UnityEngine;
using Zenject;

namespace RollingBall.Installer
{
    public sealed class ProjectInstaller : MonoInstaller
    {
        [SerializeField] private SpriteMask spriteMask = default;
        [SerializeField] private UnityAudioBgmController bgmController = default;
        [SerializeField] private UnityAudioSeController seController = default;

        public override void InstallBindings()
        {
            Container
                .BindInstance(spriteMask)
                .AsCached();

            Container
                .Bind<TransitionSpriteMask>()
                .AsCached();

            Container
                .Bind<SceneLoader>()
                .AsCached();

            Container
                .BindInterfacesAndSelfTo<UnityAudioBgmController>()
                .FromInstance(bgmController)
                .AsCached();

            Container
                .BindInterfacesAndSelfTo<UnityAudioSeController>()
                .FromInstance(seController)
                .AsCached();
        }
    }
}