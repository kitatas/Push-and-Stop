using System;
using UnityEngine;
using Zenject;

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

    private readonly StageObjectTable _stageObjectTable;
    private readonly GoalInfo _goalInfo;
    private readonly DiContainer _diContainer;

    public StageLoader(StageObjectTable stageObjectTable, GoalInfo goalInfo, DiContainer diContainer)
    {
        _stageObjectTable = stageObjectTable;
        _goalInfo = goalInfo;
        _diContainer = diContainer;
    }

    public void LoadStageData(TextAsset stage)
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
                // skip
                break;
            case SquareType.Player:
                // set position
                break;
            case SquareType.Goal:
                // set position
                _goalInfo.SetPosition(position);
                break;
            case SquareType.Block:
                // create
                var block = _diContainer.InstantiatePrefab(_stageObjectTable.Block);
                block.transform.position = position;
                break;
            case SquareType.MoveBlock:
                // create
                var moveBlock = _diContainer.InstantiatePrefab(_stageObjectTable.MoveBlock);
                moveBlock.transform.position = position;
                break;
            case SquareType.BallBlock:
                // create
                var ballBlock = _diContainer.InstantiatePrefab(_stageObjectTable.BallBlock);
                ballBlock.transform.position = position;
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(squareType), squareType, null);
        }
    }
}