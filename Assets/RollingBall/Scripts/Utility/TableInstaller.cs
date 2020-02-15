using UnityEngine;
using Zenject;

[CreateAssetMenu(fileName = "TableInstaller", menuName = "Installers/TableInstaller")]
public sealed class TableInstaller : ScriptableObjectInstaller<TableInstaller>
{
    [SerializeField] private StageDataTable stageDataTable = null;
    [SerializeField] private StageObjectTable stageObjectTable = null;
    [SerializeField] private BgmTable bgmTable = null;
    [SerializeField] private SeTable seTable = null;

    public override void InstallBindings()
    {
        Container
            .BindInstance(stageDataTable);

        Container
            .BindInstance(stageObjectTable);

        Container
            .BindInstance(bgmTable);

        Container
            .BindInstance(seTable);
    }
}