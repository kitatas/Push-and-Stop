using Zenject;

namespace RollingBall.Player
{
    public sealed class PlayerInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container
                .Bind<PlayerMover>()
                .AsCached();
        }
    }
}