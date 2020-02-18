using Zenject;

public sealed class StageInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Container
            .Bind<StageLoader>()
            .AsCached()
            .NonLazy();
    }
}