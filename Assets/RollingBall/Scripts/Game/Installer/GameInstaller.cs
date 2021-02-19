using RollingBall.Game.Memento;
using RollingBall.Game.MoveCount;
using RollingBall.Game.Player;
using RollingBall.Game.StageData;
using RollingBall.Game.StageObject;
using UnityEngine;
using Zenject;

namespace RollingBall.Game.Installer
{
    public sealed class GameInstaller : MonoInstaller
    {
        [SerializeField] private MoveCountView moveCountView = default;
        [SerializeField] private PlayerController playerController = default;
        [SerializeField] private Rigidbody2D rigidbody2d = default;
        [SerializeField] private Goal goal = default;

        public override void InstallBindings()
        {
            #region Memento

            Container
                .Bind<Caretaker>()
                .AsCached();

            #endregion

            #region MoveCount

            Container
                .BindInterfacesAndSelfTo<MoveCountModel>()
                .AsCached();

            Container
                .Bind<MoveCountView>()
                .FromInstance(moveCountView)
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
                .Bind<MoveUseCase>()
                .AsCached();

            Container
                .Bind<Rigidbody2D>()
                .FromInstance(rigidbody2d)
                .AsCached();

            #endregion

            #region StageData

            Container
                .Bind<StageRepository>()
                .AsCached()
                .NonLazy();

            #endregion

            #region StageObject

            Container
                .Bind<Goal>()
                .FromInstance(goal)
                .AsCached();

            #endregion
        }
    }
}