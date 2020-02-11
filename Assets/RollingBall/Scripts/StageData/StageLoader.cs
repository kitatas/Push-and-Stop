using System;
using UnityEngine;
using Zenject;

public sealed class StageLoader : MonoBehaviour
{
    private enum SquareType
    {
             None = 0,
           Player = 1,
             Goal = 2,
            Block = 3,
        MoveBlock = 4,
        BallBlock = 5,
    }

    private StageObjectTable _stageObjectTable;
    private GoalInfo _goalInfo;

    [Inject]
    private void Construct(StageObjectTable stageObjectTable, GoalInfo goalInfo, StageDataTable stageDataTable,
        MinMoveCountView minMoveCountView)
    {
        _stageObjectTable = stageObjectTable;
        _goalInfo = goalInfo;

        var stageData = stageDataTable.StageDataInfo();
        minMoveCountView.Display(stageData.minMoveCount);
        LoadStageData(stageData.stageFile);
    }

    private void LoadStageData(TextAsset stage)
    {
        var lines = stage.text.Split(new[] {'\n', '\r'}, StringSplitOptions.RemoveEmptyEntries);

        var col = lines[0].Split(',').Length;
        var row = lines.Length;

        for (int y = 0; y < row; y++)
        {
            var value = lines[y].Split(',');
            for (int x = 0; x < col; x++)
            {
                var type = (SquareType) int.Parse(value[x]);
                var pos = new Vector2(x - 2, 2 - y);
                Create(type, pos);
            }
        }
    }

    private void Create(SquareType squareType, Vector2 position)
    {
        switch (squareType)
        {
            case SquareType.None:
                break;
            case SquareType.Player:
                break;
            case SquareType.Goal:
                _goalInfo.SetPosition(position);
                break;
            case SquareType.Block:
                var block = Instantiate(_stageObjectTable.block);
                block.transform.position = position;
                break;
            case SquareType.MoveBlock:
                var moveBlock = Instantiate(_stageObjectTable.moveBlock);
                moveBlock.transform.position = position;
                break;
            case SquareType.BallBlock:
                var ballBlock = Instantiate(_stageObjectTable.ballBlock);
                ballBlock.transform.position = position;
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(squareType), squareType, null);
        }
    }
}