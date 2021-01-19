using RollingBall.Game.MoveCount;
using RollingBall.Game.Player;
using UnityEngine;
using Zenject;

namespace RollingBall.Game.Installer
{
    public sealed class GameInstaller : MonoInstaller
    {
        [SerializeField] private MoveCountView moveCountView = default;
        [SerializeField] private PlayerController playerController = default;
        [SerializeField] private Rigidbody2D rigidbody2d = default;

        public override void InstallBindings()
        {
            #region MoveCount

            Container
                .BindInterfacesTo<MoveCountModel>()
                .AsCached();

            Container
                .BindInstance(moveCountView)
                .AsCached();

            Container
                .Bind<MoveCountPresenter>()
                .AsCached()
                .NonLazy();

            #endregion

            #region Player

            Container
                .Bind<PlayerController>()
                .FromInstance(playerController)
                .AsCached();

            Container
                .Bind<PlayerMover>()
                .AsCached();

            Container
                .Bind<Rigidbody2D>()
                .FromInstance(rigidbody2d)
                .AsCached();

            #endregion
        }
    }
}