using Zenject;

namespace RollingBall.MoveCounter
{
    public sealed class MoveCountInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container
                .BindInterfacesTo<MoveCountModel>()
                .AsCached();

            Container
                .Bind<MoveCountPresenter>()
                .AsCached()
                .NonLazy();
        }
    }
}