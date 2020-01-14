using Zenject;

public sealed class StageInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Container
            .Bind<StageLoader>()
            .AsCached();

        Container
            .Bind<StageInitializer>()
            .AsCached()
            .NonLazy();
    }
}