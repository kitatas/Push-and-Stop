using Zenject;

public class MoveCountInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Container
            .Bind<MoveCountModel>()
            .AsSingle();

        Container
            .Bind<MoveCountPresenter>()
            .AsSingle()
            .NonLazy();
    }
}