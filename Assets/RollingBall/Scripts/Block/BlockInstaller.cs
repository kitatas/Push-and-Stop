using UnityEngine;
using Zenject;

public class BlockInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Container
            .Bind<Rigidbody2D>()
            .FromComponentOnRoot()
            .AsSingle();
    }
}