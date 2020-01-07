using Zenject;

public sealed class PlayerInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Container
            .Bind<PlayerMover>()
            .AsCached();
    }
}