using Zenject;

public sealed class MoveCountInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Container
            .Bind<MoveCountModel>()
            .AsCached();

        Container
            .Bind<MoveCountPresenter>()
            .AsCached()
            .NonLazy();
    }
}