using UnityEngine;
using Zenject;

[CreateAssetMenu(fileName = "TableInstaller", menuName = "Installers/TableInstaller")]
public class TableInstaller : ScriptableObjectInstaller<TableInstaller>
{
    [SerializeField] private StageDataTable stageDataTable = null;

    public override void InstallBindings()
    {
        Container
            .BindInstance(stageDataTable);
    }
}