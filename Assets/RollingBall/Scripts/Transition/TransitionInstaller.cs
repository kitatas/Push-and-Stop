using Zenject;

public sealed class TransitionInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Container
            .Bind<TransitionSpriteMask>()
            .AsCached();

        Container
            .Bind<Transition>()
            .AsCached();
    }
}