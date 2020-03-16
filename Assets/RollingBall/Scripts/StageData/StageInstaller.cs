using Zenject;

public sealed class StageInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Container
            .Bind<Caretaker>()
            .AsCached();

        Container
            .Bind<ClearAction>()
            .AsCached();

        Container
            .Bind<StageLoader>()
            .AsCached()
            .NonLazy();
    }
}