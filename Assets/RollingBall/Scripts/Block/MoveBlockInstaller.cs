using UnityEngine;
using Zenject;

public class MoveBlockInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Container
            .Bind<Rigidbody2D>()
            .FromComponentOnRoot()
            .AsSingle();
    }
}