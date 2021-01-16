using Zenject;

namespace RollingBall.Transition
{
    public sealed class TransitionInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container
                .Bind<TransitionSpriteMask>()
                .AsCached();

            Container
                .Bind<SceneLoader>()
                .AsCached();
        }
    }
}