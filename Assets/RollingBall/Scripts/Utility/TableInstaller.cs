using RollingBall.Sound.UnityAudio.BGM;
using RollingBall.Sound.UnityAudio.SE;
using RollingBall.StageData;
using RollingBall.StageObject;
using UnityEngine;
using Zenject;

namespace RollingBall.Utility
{
    [CreateAssetMenu(fileName = "TableInstaller", menuName = "Installers/TableInstaller")]
    public sealed class TableInstaller : ScriptableObjectInstaller<TableInstaller>
    {
        [SerializeField] private StageDataTable stageDataTable = null;
        [SerializeField] private StageObjectTable stageObjectTable = null;
        [SerializeField] private UnityAudioBgmTable unityAudioBgmTable = null;
        [SerializeField] private UnityAudioSeTable unityAudioSeTable = null;

        public override void InstallBindings()
        {
            Container
                .BindInstance(stageDataTable);

            Container
                .BindInstance(stageObjectTable);

            Container
                .BindInstance(unityAudioBgmTable);

            Container
                .BindInstance(unityAudioSeTable);
        }
    }
}