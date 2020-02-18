using System;
using UnityEngine;
using Zenject;
using Object = UnityEngine.Object;

public sealed class StageLoader
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
    private IStageObject _player;
    private IStageObject _goal;

    [Inject]
    private void Construct(StageObjectTable stageObjectTable, PlayerController playerController, Goal goal,
        StageDataTable stageDataTable, MinMoveCountView minMoveCountView)
    {
        _stageObjectTable = stageObjectTable;
        _player = playerController;
        _goal = goal;

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
        IStageObject stageObject;
        switch (squareType)
        {
            case SquareType.None:
                return;
            case SquareType.Player:
                stageObject = _player;
                break;
            case SquareType.Goal:
                stageObject = _goal;
                break;
            case SquareType.Block:
                stageObject = Object.Instantiate(_stageObjectTable.block);
                break;
            case SquareType.MoveBlock:
                stageObject = Object.Instantiate(_stageObjectTable.moveBlock);
                break;
            case SquareType.BallBlock:
                stageObject = Object.Instantiate(_stageObjectTable.ballBlock);
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(squareType), squareType, null);
        }

        stageObject.SetPosition(position);
    }
}