using Zenject;

public sealed class StageInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Container
            .Bind<StageInitializer>()
            .AsCached()
            .NonLazy();
    }
}