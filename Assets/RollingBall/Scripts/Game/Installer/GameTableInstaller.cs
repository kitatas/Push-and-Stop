using RollingBall.Game.StageData;
using RollingBall.Game.StageObject;
using UnityEngine;
using Zenject;

namespace RollingBall.Game.Installer
{
    [CreateAssetMenu(fileName = "GameTableInstaller", menuName = "Installers/GameTableInstaller")]
    public sealed class GameTableInstaller : ScriptableObjectInstaller
    {
        [SerializeField] private StageDataTable stageDataTable = default;
        [SerializeField] private StageObjectTable stageObjectTable = default;

        public override void InstallBindings()
        {
            Container
                .Bind<StageDataTable>()
                .FromInstance(stageDataTable)
                .AsCached();

            Container
                .Bind<StageObjectTable>()
                .FromInstance(stageObjectTable)
                .AsCached();
        }
    }
}